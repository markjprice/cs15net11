# Keeping Blazor WebAssembly responsive with a Web Worker

- [Keeping Blazor WebAssembly responsive with a Web Worker](#keeping-blazor-webassembly-responsive-with-a-web-worker)
- [CPU-bound work versus I/O-bound work](#cpu-bound-work-versus-io-bound-work)
- [Creating a Blazor Web Worker project](#creating-a-blazor-web-worker-project)
- [Exporting a worker method](#exporting-a-worker-method)
- [Creating and invoking the worker](#creating-and-invoking-the-worker)
- [Invoking a method that does not return a result](#invoking-a-method-that-does-not-return-a-result)
- [Handling cancellation and timeouts](#handling-cancellation-and-timeouts)
- [Disposing the worker](#disposing-the-worker)
- [Understanding the limitations](#understanding-the-limitations)

Blazor WebAssembly normally executes application code on the browser thread responsible for responding to user input and updating the page. A lengthy calculation on that thread can prevent buttons, animations, scrolling, and rendering from responding until the calculation finishes.

A browser **Web Worker** provides a separate execution context in which code can run without blocking the page’s main thread. The page and the worker communicate by sending messages. They do not share ordinary object instances, and a worker cannot directly read or modify the page’s **Document Object Model (DOM)**. The worker performs the calculation and returns data to the Blazor component, which then updates the UI on the main thread.

The **Blazor Web Worker** project template generates the JavaScript messaging infrastructure, a `WebWorkerClient` class, and an example C# method marked with `[JSExport]`.

# CPU-bound work versus I/O-bound work

A Web Worker is most useful for **CPU-bound work**, meaning work that spends most of its time using the processor. Examples include:

* Analyzing a large collection of customers.
* Parsing or transforming a large document.
* Performing image processing.
* Running a simulation.
* Sorting, grouping, or aggregating a large amount of data.
* Compressing or decompressing data.

Making a method asynchronous does not automatically move its work onto another thread. An `async` method that performs a long synchronous calculation still occupies the browser thread until it reaches an incomplete awaited operation.

**I/O-bound work** spends most of its time waiting for an external operation, such as an HTTP request, to finish. When an HTTP call is awaited correctly, Blazor can return control to the browser while it waits for the response. Moving the HTTP call into a Web Worker usually provides no responsiveness benefit because the network request is already non-blocking. CPU-bound and I/O-bound asynchronous operations therefore require different approaches.

> **Good practice:** Use a Blazor Web Worker for substantial CPU-bound browser work, not merely because a method is asynchronous. HTTP calls are already non-blocking when awaited correctly. Creating a worker adds startup, messaging, serialization, memory, and disposal overhead, so it is usually unnecessary for small calculations.

# Creating a Blazor Web Worker project

From the solution folder, enter the following command to create a worker project named `Northwind.WebApi.WasmWorker`:

```bash
dotnet new blazorwebworker -o Northwind.WebApi.WasmWorker
```

The template creates a Razor class library containing files similar to the following:

```text
Northwind.WebApi.WasmWorker/
├── Northwind.WebApi.WasmWorker.csproj
├── WebWorkerClient.cs
├── WorkerMethods.cs
└── wwwroot/
    ├── dotnet-web-worker-client.js
    └── dotnet-web-worker.js
```

`WebWorkerClient.cs` manages the worker’s creation, invocation, and disposal. `WorkerMethods.cs` contains an example exported method. The JavaScript files start the .NET WebAssembly runtime inside the worker and pass invocation messages and results between the worker and the Blazor application.

Add the worker project to the solution, and then add a project reference from the Blazor WebAssembly client. Assuming that the client project is named `Northwind.WebApi.WasmClient`, enter the following commands:

```bash
dotnet sln add Northwind.WebApi.WasmWorker

dotnet add Northwind.WebApi.WasmClient reference \
  Northwind.WebApi.WasmWorker
```

The project reference makes the worker assembly and its static web assets available to the Blazor client.

# Exporting a worker method

Worker methods must currently be static methods marked with `[JSExport]` and declared in a static partial class. The class should also be marked as supported in the browser.

Replace the generated `WorkerMethods.cs` file with a file named `CustomerWorker.cs`, as shown in the following code:

```csharp
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json;

namespace Northwind.WebApi.WasmWorker;

[SupportedOSPlatform("browser")]
public static partial class CustomerWorker
{
    private static CustomerForAnalysis[] cachedCustomers = [];

    [JSExport]
    public static string Analyze(string customers)
    {
        CustomerForAnalysis[] items =
            JsonSerializer.Deserialize<CustomerForAnalysis[]>(customers)
            ?? [];

        int countryCount = items
            .Where(customer =>
                !string.IsNullOrWhiteSpace(customer.Country))
            .Select(customer => customer.Country)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Count();

        string mostCommonCountry = items
            .Where(customer =>
                !string.IsNullOrWhiteSpace(customer.Country))
            .GroupBy(
                customer => customer.Country!,
                StringComparer.OrdinalIgnoreCase)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key)
            .Select(group => group.Key)
            .FirstOrDefault()
            ?? "Unknown";

        return $"{items.Length:N0} customers are located in " +
               $"{countryCount:N0} countries. The country with the " +
               $"most customers is {mostCommonCountry}.";
    }

    [JSExport]
    public static void Cache(string customers)
    {
        cachedCustomers =
            JsonSerializer.Deserialize<CustomerForAnalysis[]>(customers)
            ?? [];
    }

    [JSExport]
    public static void ClearCache()
    {
        cachedCustomers = [];
    }
}

public sealed record CustomerForAnalysis(
    string CustomerId,
    string CompanyName,
    string? Country);
```

The current `[JSExport]` guidance recommends using JavaScript-interoperable primitive values and strings at the worker boundary. Complex application objects should therefore be serialized before invocation and deserialized inside the worker.

In this example, the parameter named `customers` is a JSON string rather than a `List<Customer>`. This makes the serialization boundary explicit and avoids depending on preview behavior for complex exported method parameters.

# Creating and invoking the worker

Inject `IJSRuntime` into the component that will use the worker:

```razor
@using System.Text.Json
@using Northwind.WebApi.WasmWorker
@inject IJSRuntime JSRuntime
```

Before invoking the worker, map the application’s customer objects to the smaller record required by the calculation and serialize them:

```csharp
CustomerForAnalysis[] customersForAnalysis = customerDtos
    .Select(customer => new CustomerForAnalysis(
        CustomerId: customer.CustomerId,
        CompanyName: customer.CompanyName,
        Country: customer.Country))
    .ToArray();

string customers =
    JsonSerializer.Serialize(customersForAnalysis);
```

Create the worker by calling `WebWorkerClient.CreateAsync`:

```csharp
await using WebWorkerClient worker =
    await WebWorkerClient.CreateAsync(JSRuntime);
```

`CreateAsync` loads the worker scripts, creates a browser worker, starts the .NET runtime inside it, and waits until it is ready to accept calls.

Invoke the result-returning `Analyze` method as follows:

```csharp
string result = await worker.InvokeAsync<string>(
    "Northwind.WebApi.WasmWorker.CustomerWorker.Analyze",
    [customers]);
```

The method name identifies the exported class and method within the loaded worker assembly. Because the namespace matches the assembly name in this project, the complete exported path is:

```text
Northwind.WebApi.WasmWorker.CustomerWorker.Analyze
```

The returned string can then be assigned to component state and displayed in the UI:

```csharp
analysisResult = result;
```

# Invoking a method that does not return a result

Use `InvokeVoidAsync` when the exported method does not return a value. For example, the following call sends the serialized customers to the `Cache` method:

```csharp
await worker.InvokeVoidAsync(
    "Northwind.WebApi.WasmWorker.CustomerWorker.Cache",
    [customers]);
```

The cached data can later be cleared:

```csharp
await worker.InvokeVoidAsync(
    "Northwind.WebApi.WasmWorker.CustomerWorker.ClearCache",
    []);
```

Despite its name, `InvokeVoidAsync` should still be awaited. It means that the exported method does not produce a result value, not that the caller should discard the returned task or ignore errors.

# Handling cancellation and timeouts

The generated `WebWorkerClient` supports cancellation tokens and timeout values for both worker creation and method invocation. The default timeout in the Preview 4 implementation is 60 seconds.

The following component code allows an operation to be canceled and sets separate startup and invocation timeouts:

```csharp
private CancellationTokenSource? analysisCancellation;
private string analysisResult = string.Empty;
private string statusMessage = string.Empty;

private async Task AnalyzeCustomersAsync()
{
    analysisCancellation?.Cancel();
    analysisCancellation?.Dispose();

    analysisCancellation = new CancellationTokenSource();

    try
    {
        statusMessage = "Starting worker...";

        CustomerForAnalysis[] customersForAnalysis = customerDtos
            .Select(customer => new CustomerForAnalysis(
                CustomerId: customer.CustomerId,
                CompanyName: customer.CompanyName,
                Country: customer.Country))
            .ToArray();

        string customers =
            JsonSerializer.Serialize(customersForAnalysis);

        await using WebWorkerClient worker =
            await WebWorkerClient.CreateAsync(
                JSRuntime,
                timeoutMs: 10_000,
                cancellationToken: analysisCancellation.Token);

        statusMessage = "Analyzing customers...";

        string result = await worker.InvokeAsync<string>(
            "Northwind.WebApi.WasmWorker.CustomerWorker.Analyze",
            [customers],
            timeoutMs: 30_000,
            cancellationToken: analysisCancellation.Token);

        analysisResult = result;
        statusMessage = "Analysis complete.";
    }
    catch (OperationCanceledException)
    {
        statusMessage = "Analysis canceled.";
    }
    catch (JSException ex) when (
        ex.Message.Contains(
            "timed out",
            StringComparison.OrdinalIgnoreCase))
    {
        statusMessage =
            "The worker did not finish within the allowed time.";
    }
    catch (JSException ex)
    {
        statusMessage = $"Worker error: {ex.Message}";
    }
}

private void CancelAnalysis()
{
    analysisCancellation?.Cancel();
}
```

A button can invoke the cancellation method:

```razor
<button class="btn btn-secondary"
        @onclick="CancelAnalysis">
  Cancel
</button>
```

In the current preview implementation, the cancellation token cancels the caller’s JavaScript interop wait. The JavaScript timeout similarly rejects the pending invocation when its time limit expires. Neither mechanism should be treated as cooperative cancellation inside the exported C# method because the method does not automatically receive the caller’s `CancellationToken`. Disposing the client terminates the browser worker and is the reliable way to stop the worker instance. This behavior follows from the generated worker client’s current JavaScript and C# implementations and could change before release.

For an algorithm that can be divided into smaller units, another option is to pass progress or cancellation state through additional messages. That requires custom worker communication beyond the basic generated client.

# Disposing the worker

The `WebWorkerClient` implements `IAsyncDisposable`. For a one-off operation, declare it with `await using`:

```cs
await using WebWorkerClient worker =
    await WebWorkerClient.CreateAsync(JSRuntime);
```

The compiler generates a call to `DisposeAsync` when execution leaves the scope, including when an exception is thrown. Disposal terminates the underlying browser worker and releases its JavaScript object reference.

Starting a .NET runtime for every small calculation is wasteful. When the same component performs several substantial operations, store the worker in a field and reuse it:

```cs
private WebWorkerClient? worker;

protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if (firstRender)
    {
        worker = await WebWorkerClient.CreateAsync(JSRuntime);
    }
}
```

The component should then implement `IAsyncDisposable` and dispose of the worker when the component is removed:

```cs
public async ValueTask DisposeAsync()
{
    analysisCancellation?.Cancel();
    analysisCancellation?.Dispose();

    if (worker is not null)
    {
        await worker.DisposeAsync();
    }
}
```

Reusing a worker avoids repeatedly paying its startup cost, but it also keeps the worker’s .NET runtime and memory alive. Choose the lifetime according to how often the feature is used.

# Understanding the limitations

A Blazor Web Worker has several limitations and costs:

* **No direct DOM access:** Worker code cannot manipulate elements, call component rendering methods, or directly update the page. It must return data to the Blazor component, which updates the UI.
* **Serialization boundaries:** Values must pass through .NET-to-JavaScript interop and browser worker messaging. Complex objects should currently be serialized explicitly, usually as JSON.
* **Data-copying costs:** Worker messages generally copy or serialize data instead of sharing the original object instances. Large customer collections can therefore take noticeable time and memory to transfer.
* **Startup overhead:** Creating a worker starts another .NET WebAssembly runtime. This consumes time and memory before the calculation begins.
* **Separate state:** Static fields in the worker belong to that worker instance. Their values are lost when the worker is disposed.
* **No automatic cancellation inside the method:** Canceling the invocation does not inject a cancellation token into the exported method. Terminate the worker when execution must be stopped.
* **Error boundaries:** Exceptions must cross the JavaScript messaging boundary and are normally surfaced to the client as `JSException`.
* **Unsuitable for trivial work:** For small calculations, worker startup and serialization may cost more than running the calculation directly.

Browsers use message passing and structured cloning to move supported data between a page and its workers. DOM nodes and functions cannot be cloned, and worker code does not have access to the page’s DOM.
