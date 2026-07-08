# Benchmarking performance and resource usage

- [Benchmarking performance and resource usage](#benchmarking-performance-and-resource-usage)
  - [Evaluating the efficiency of types](#evaluating-the-efficiency-of-types)
  - [Monitoring performance and memory using Benchmark.NET](#monitoring-performance-and-memory-using-benchmarknet)

## Evaluating the efficiency of types

What is the best type to use for a scenario? To answer this question, we need to carefully consider what we mean by *best*, and through this, we should consider the following factors:

- **Functionality**: This can be decided by checking whether the type provides the features you need.
- **Memory size**: This can be decided by the number of bytes of memory the type takes up.
- **Performance**: This can be decided by how fast the type is.
- **Future needs**: This depends on the changes in requirements and maintainability.

There will be scenarios, such as when storing numbers, where multiple types have the same functionality, so we will need to consider memory and performance to make a choice.

If we need to store millions of numbers, then the best type to use would be the one that requires the fewest bytes of memory. But if we only need to store a few numbers, yet we need to perform lots of calculations on them, then the best type to use would be the one that runs fastest on a specific CPU.

The `sizeof()` function shows the number of bytes that a single instance of a type uses in memory. When we are storing many values in more complex data structures, such as arrays and lists, then we need a better way of measuring memory usage.

You can read lots of advice online and in books, but the only way to know for sure what the best type would be for your code is to compare the types yourself.

In the next section, you will learn how to write code to monitor the actual memory requirements and performance when using different types.

Today a `short` variable might be the best choice, but it might be an even better choice to use an `int` variable, even though it takes twice as much space in the memory. This is because we might need a wider range of values to be stored in the future.

As listed above, there is an important metric that developers often forget: maintenance. This is a measure of how much effort another programmer would have to put in to understand and modify your code. If you make a nonobvious choice of type without explaining that choice with a helpful comment, then it might confuse the programmer who comes along later and needs to fix a bug or add a feature.

## Monitoring performance and memory using Benchmark.NET

There is a popular benchmarking NuGet package for .NET that Microsoft uses in its blog posts about performance improvements, so it is good for .NET developers to know how it works and use it for their own performance testing. 

Let's see how we could use it to compare performance between `string` concatenation and `StringBuilder`:

1.	Use your preferred code editor to add a new console app to the `Chapter08` solution named `Benchmarking`.
2.	In the `Benchmarking` project, add a package reference to Benchmark.NET, remembering that you can find out the latest version and use that instead of the version I used, as shown in the following markup:
```xml
<ItemGroup>
  <PackageReference Include="BenchmarkDotNet" Version="0.15.8" />
</ItemGroup>
```
3.	Build the project to restore packages.
4.	Add a new class file named `StringBenchmarks.cs`.
5.	In `StringBenchmarks.cs`, add statements to define a class with methods for each benchmark you want to run, in this case, two methods that both combine twenty numbers comma-separated using either `string` concatenation or `StringBuilder`, as shown in the following code:
```cs
using BenchmarkDotNet.Attributes; // [Benchmark]

public class StringBenchmarks
{
  int[] numbers;

  public StringBenchmarks()
  {
    numbers = Enumerable.Range(
      start: 1, count: 20).ToArray();
  }

  [Benchmark(Baseline = true)]
  public string StringConcatenationTest()
  {
    string s = string.Empty; // e.g. ""

    for (int i = 0; i < numbers.Length; i++)
    {
      s += numbers[i] + ", ";
    }

    return s;
  }

  [Benchmark]
  public string StringBuilderTest()
  {
    System.Text.StringBuilder builder = new();

    for (int i = 0; i < numbers.Length; i++)
    {
      builder.Append(numbers[i]);
      builder.Append(", ");
    }

    return builder.ToString();
  }
}
```

6.	`In Program.cs`, delete the existing statements and then import the namespace for running benchmarks and add a statement to run the benchmarks class, as shown in the following code:
```cs
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<StringBenchmarks>();
```

7.	Use your preferred coding tool to run the console app with its release configuration:
    - In Visual Studio, in the toolbar, set **Solution Configurations** to **Release**, and then navigate to **Debug** | **Start Without Debugging**.
    - In VS Code, in a terminal, enter the `dotnet run --configuration Release` command.

> **Important!** You must build the benchmarks in a **Release** build. This is important for performance testing, as most optimizations are disabled in **Debug** builds, in both the C# compiler and the JIT compiler.

8.	Note the results, including some artifacts like report files, and the most important, a summary table that shows that `string` concatenation took a mean of 412.990 ns and `StringBuilder` took a mean of 275.082 ns, as shown in the following partial output:
```
// ***** BenchmarkRunner: Finish  *****

// * Export *
  BenchmarkDotNet.Artifacts\results\StringBenchmarks-report.csv
  BenchmarkDotNet.Artifacts\results\StringBenchmarks-report-github.md
  BenchmarkDotNet.Artifacts\results\StringBenchmarks-report.html

// * Detailed results *
StringBenchmarks.StringConcatenationTest: DefaultJob
Runtime = .NET 11.0.0 (11.0.22.22904), X64 RyuJIT; GC = Concurrent Workstation
Mean = 412.990 ns, StdErr = 2.353 ns (0.57%), N = 46, StdDev = 15.957 ns
Min = 373.636 ns, Q1 = 413.341 ns, Median = 417.665 ns, Q3 = 420.775 ns, Max = 434.504 ns
IQR = 7.433 ns, LowerFence = 402.191 ns, UpperFence = 431.925 ns
ConfidenceInterval = [404.708 ns; 421.273 ns] (CI 99.9%), Margin = 8.282 ns (2.01% of Mean)
Skewness = -1.51, Kurtosis = 4.09, MValue = 2
-------------------- Histogram --------------------
[370.520 ns ; 382.211 ns) | @@@@@@
[382.211 ns ; 394.583 ns) | @
[394.583 ns ; 411.300 ns) | @@
[411.300 ns ; 422.990 ns) | @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
[422.990 ns ; 436.095 ns) | @@@@@
---------------------------------------------------

StringBenchmarks.StringBuilderTest: DefaultJob
Runtime = .NET 11.0.0 (11.0.22.22904), X64 RyuJIT; GC = Concurrent Workstation
Mean = 275.082 ns, StdErr = 0.558 ns (0.20%), N = 15, StdDev = 2.163 ns
Min = 271.059 ns, Q1 = 274.495 ns, Median = 275.403 ns, Q3 = 276.553 ns, Max = 278.030 ns
IQR = 2.058 ns, LowerFence = 271.409 ns, UpperFence = 279.639 ns
ConfidenceInterval = [272.770 ns; 277.394 ns] (CI 99.9%), Margin = 2.312 ns (0.84% of Mean)
Skewness = -0.69, Kurtosis = 2.2, MValue = 2
-------------------- Histogram --------------------
[269.908 ns ; 278.682 ns) | @@@@@@@@@@@@@@@
---------------------------------------------------

// * Summary *

BenchmarkDotNet=v0.15.8, OS=Windows 10.0.22000
11th Gen Intel Core i7-1165G7 2.80GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=11.0.100
  [Host]     : .NET 11.0.0 (11.0.22.22904), X64 RyuJIT
  DefaultJob : .NET 11.0.0 (11.0.22.22904), X64 RyuJIT

|                  Method |     Mean |   Error |   StdDev | Ratio | RatioSD |
|------------------------ |---------:|--------:|---------:|------:|--------:|
| StringConcatenationTest | 413.0 ns | 8.28 ns | 15.96 ns |  1.00 |    0.00 |
|       StringBuilderTest | 275.1 ns | 2.31 ns |  2.16 ns |  0.69 |    0.04 |

// * Hints *
Outliers
  StringBenchmarks.StringConcatenationTest: Default -> 7 outliers were removed, 14 outliers were detected (376.78 ns..391.88 ns, 440.79 ns..506.41 ns)
  StringBenchmarks.StringBuilderTest: Default       -> 2 outliers were detected (274.68 ns, 274.69 ns)

// * Legends *
  Mean    : Arithmetic mean of all measurements
  Error   : Half of 99.9% confidence interval
  StdDev  : Standard deviation of all measurements
  Ratio   : Mean of the ratio distribution ([Current]/[Baseline])
  RatioSD : Standard deviation of the ratio distribution ([Current]/[Baseline])
  1 ns    : 1 Nanosecond (0.000000001 sec)

// ***** BenchmarkRunner: End *****
// ** Remained 0 benchmark(s) to run **

Run time: 00:01:13 (73.35 sec), executed benchmarks: 2
Global total time: 00:01:29 (89.71 sec), executed benchmarks: 2
// * Artifacts cleanup *
```

The `Outliers` section is especially interesting because it shows that not only is `string` concatenation slower than `StringBuilder`, but it is also more inconsistent in how long it takes. Your results will vary, of course. Note there might not be a `Hints` and an `Outliers` section if there are no outliers when you run your benchmarks!
