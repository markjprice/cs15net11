# History of Blazor

Blazor lets you build interactive web UI components using C# instead of JavaScript. Blazor is supported on all modern browsers.

## JavaScript and friends

Traditionally, any code that needs to be executed in a web browser must be written using the JavaScript programming language or a higher-level technology that transpiles (transforms or compiles) into JavaScript. This is because all browsers have supported JavaScript for over two decades, so it is the lowest common denominator for implementing business logic in the client.
JavaScript does have some issues, however. Although it has superficial similarities to C-style languages like C# and Java, it is actually very different once you dig beneath the surface. It is a dynamically typed pseudo-functional language that uses prototypes instead of class inheritance for object reuse. It might look human, but you will get a surprise when it’s revealed to be a Skrull.
It’d be great if we could use the same language and libraries in a browser as we do on the server.
Even Blazor cannot replace JavaScript completely. For example, some parts of the browser are only accessible to JavaScript. Blazor provides an interop service so that your C# code can call JavaScript code, and vice versa. You will see this in the online-only Interop with JavaScript section in Chapter 14, Building Interactive Web Components Using Blazor.
Silverlight – C# and .NET using a plugin
Microsoft made a previous attempt at achieving this goal with a technology named Silverlight. When Silverlight 2 was released in 2008, a C# and .NET developer could use their skills to build libraries and visual components that were executed in the web browser by the Silverlight plugin.
By 2011 and Silverlight 5, Apple’s success with the iPhone and Steve Jobs’ hatred of browser plugins like Flash eventually led to Microsoft abandoning Silverlight since, like Flash, Silverlight is banned from iPhones and iPads.
WebAssembly – a target for Blazor
Another development in web browsers has given Microsoft the opportunity to make another attempt. In 2017, the W3C WebAssembly Community Group reached a consensus, and all major browsers now support it: Chromium (Chrome, Edge, Opera, and Brave), Firefox, and WebKit (Safari).
WebAssembly (Wasm) is a binary instruction format for a virtual machine that provides a way to run code written in multiple languages on the web at near-native speed. Wasm is designed as a portable target for the compilation of high-level languages like C#.
Blazor hosting models in .NET 7 and earlier
Blazor is a single programming or app model. For .NET 7 and earlier, a developer had to choose one hosting model for each project:
•	A Blazor Server project runs on the server side, so the C# code has full access to all resources that your business logic might need without needing to supply credentials to authenticate. It uses SignalR to communicate UI updates to the client side. The server must keep a live SignalR connection to each client and track the current state of every client. This means that Blazor Server does not scale well if you need to support lots of clients. It first shipped as part of ASP.NET Core 3 in September 2019.
•	A Blazor Wasm project runs on the client side, so the C# code only has access to resources in the browser. It must make HTTP calls (which might require authentication) before it can access resources on the server. It first shipped as an extension to ASP.NET Core 3.1 in May 2020 and was versioned 3.2 because it was a current release and therefore not covered by ASP.NET Core 3.1’s long-term support. The Blazor Wasm 3.2 version used the Mono runtime and Mono libraries. .NET 5 and later use the Mono runtime and the .NET libraries.
•	A .NET MAUI Blazor app, aka Blazor Hybrid, project renders its web UI to a web view control using a local interop channel and is hosted in a .NET MAUI app. It is conceptually like an Electron app.
Unification of Blazor hosting models in .NET 8 and later
With .NET 8 and later, the Blazor team created a unified hosting model where each individual component can be set to execute using a different rendering model:
•	SSR: Executes code on the server side like Razor Pages and MVC do. The complete response is then sent to the browser to display to the visitor and there is no further interaction between the server and client until the browser makes a new HTTP request. As far as the browser is concerned, the web page is static just like any other HTML file.
•	Streaming rendering: Executes code on the server side. HTML markup can be returned and displayed in the browser, and while the connection is still open, any asynchronous operations can continue to execute. When all asynchronous operations are complete, the final markup is sent by the server to update the contents of the page. This improves the experience for the visitor because they see some content like a “Loading…” message while waiting for the rest.
•	Interactive server rendering: Executes code on the server side during live interactions, which means the code has full and easy access to server-side resources like databases. This can simplify implementing functionality. Interactive requests are made using SignalR, which is more efficient than a full request. A permanent connection is needed between the browser and server, which limits scalability. This is a good choice for intranet websites where there is a limited number of clients and high-bandwidth networking.
•	Interactive Wasm rendering: Executes code on the client side, which means the code only has access to resources within the browser. This can complicate the implementation because a callback to the server must be made whenever new data is required. It is a good choice for public websites where there is potentially a large number of clients and low-bandwidth connections for some of them.
•	Interactive automatic rendering: Starts by rendering on the server for faster initial display, downloads Wasm components in the background, and then switches to Wasm for subsequent interactivity.
This unified model means that, with careful planning, a developer can write Blazor components once and then choose to run them on the web server side, or the web client side, or dynamically switch. This gives the best of all worlds.
Understanding Blazor components
It is important to understand that Blazor is used to create UI components. Components define how to render the UI and react to user events, and can be composed, nested, and compiled into a Razor class library for packaging and distribution.
For example, to provide a UI for star ratings of products on a commerce site, you might create a component named Rating.razor, as shown in the following markup:
<div>
@for (int i = 0; i < Maximum; i++)
{
  if (i < Value)
  {
    <span class="oi oi-star-filled" />
  }
  else
  {
    <span class="oi oi-star-empty" />
  }
}
</div>
@code {
  [Parameter]
  public byte Maximum { get; set; }

  [Parameter]
  public byte Value { get; set; }
}
You could then use the component on a web page, as shown in the following markup:
<h1>Review</h1>
<Rating id="rating" Maximum="5" Value="3" />
<textarea id="comment" />
The markup for creating an instance of a component looks like an HTML tag, where the name of the tag is the component type. Components can be embedded in a web page using an element, for example, <Rating Value="5" />, or they can be routed to, like a mapped endpoint.
Instead of a single file with both markup and an @code block, the code can be stored in a separate code-behind file named Rating.razor.cs. The class in this file must be partial and have the same name as the component.
There are many built-in Blazor components, including ones to set elements like <title> in the <head> section of a web page, and plenty of third parties who will sell you components for common purposes.
What is the difference between Blazor and Razor?
You might wonder why Blazor components use .razor as their file extension. Razor is a template markup syntax that allows the mixing of HTML and C#. Older technologies that support Razor syntax use the .cshtml file extension to indicate the mix of C# and HTML.
Razor syntax is used for:
•	ASP.NET Core MVC views and partial views that use the .cshtml file extension. The business logic is separated into a controller class that treats the view as a template to push the view model to, which then outputs it to a web page.
•	Razor Pages that use the .cshtml file extension. The business logic can be embedded or separated into a file that uses the .cshtml.cs file extension. The output is a web page.
•	Blazor components that use the .razor file extension. The output is rendered as part of a web page, although layouts can be used to wrap a component so it outputs as a web page, and the @page directive can be used to assign a route that defines the URL path to retrieve the component as a page.
Now that you understand the background of Blazor, let’s see something more practical: how to add Blazor support to an existing ASP.NET Core project.
