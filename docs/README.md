# Docs

This documentation section of the repository contains links to all the optional online-only sections for the book as well as other content that readers will find useful.

- [Docs](#docs)
- [Overview](#overview)
- [Chapter 1 Hello C#, Welcome .NET!](#chapter-1-hello-c-welcome-net)
- [Chapter 2 Speaking C#](#chapter-2-speaking-c)
- [Chapter 3 Controlling Flow, Converting Types, and Handling Exceptions](#chapter-3-controlling-flow-converting-types-and-handling-exceptions)
- [Chapter 4 Writing, Debugging, and Testing Functions](#chapter-4-writing-debugging-and-testing-functions)
- [Chapter 5 Building Your Own Types with Object-Oriented Programming](#chapter-5-building-your-own-types-with-object-oriented-programming)
- [Chapter 6 Implementing Interfaces and Inheriting Classes](#chapter-6-implementing-interfaces-and-inheriting-classes)
- [Chapter 7 Packaging and Distributing .NET Types](#chapter-7-packaging-and-distributing-net-types)
- [Chapter 8 Working with Common .NET Types](#chapter-8-working-with-common-net-types)
- [Chapter 9 Processes, Files, Streams, and Serialization](#chapter-9-processes-files-streams-and-serialization)
- [Chapter 10 Working with Data Using Entity Framework Core](#chapter-10-working-with-data-using-entity-framework-core)
- [Chapter 11 Querying and Manipulating Data Using LINQ](#chapter-11-querying-and-manipulating-data-using-linq)
- [Chapter 12 Building Web Apps Using Blazor](#chapter-12-building-web-apps-using-blazor)
- [Chapter 13 Building and Consuming Web Services](#chapter-13-building-and-consuming-web-services)
- [Errata, Improvements, and Common Mistakes](#errata-improvements-and-common-mistakes)

# Overview
- [**Book Links**](book-links.md): All links in the published book.
- [**Support for .NET 12**](https://github.com/markjprice/markjprice/blob/main/articles/dotnet12.md): How to use the .NET 11 edition of this book with .NET 12 previews starting in February 2027.
- [**What's New in the 11th Edition**](whats-new-in-book.md): There are hundreds of minor fixes and improvements throughout the 11th edition.

# Chapter 1 Hello C#, Welcome .NET!
- [.NET History](ch01-dotnet-history.md)
- [Project Options](ch01-project-options.md)
- [Solution Evolution](ch01-solution-evolution.md)
- [Microsoft Learn documentation MCP server](ch01-ms-learn-mcp-server.md)
- [Source Code](ch01-source-code.md)

# Chapter 2 Speaking C#
- [C# language versions and features](ch02-features.md)
- [Revealing the extent of the C# vocabulary](ch02-vocabulary.md)
- [`decimal` vs. `double`](ch02-decimal-vs-double.md)
- [Special real number values](ch02-special-real-numbers.md)
- [Getting and setting the default values for types](ch02-defaults.md)
- [Dynamic types](ch02-dynamic.md)
- [What does `new` do?](ch02-what-does-new-do.md)
- [Custom number formatting](ch02-custom-formatting.md)
- [Passing arguments to a console app](ch02-arguments.md)
- [Handling platforms that do not support an API](ch02-api-unsupported-platforms.md)

# Chapter 3 Controlling Flow, Converting Types, and Handling Exceptions
- [Adding a new item to a project using Visual Studio](ch03-adding-a-new-item.md)
- [Why you should always use braces with `if` statements](ch03-always-use-braces.md)
- [Understanding how `foreach` works internally](ch03-foreach-internals.md)
- [Rounding numbers and the default rounding rules](ch03-rounding-numbers.md)
- [Converting from a binary object to a string using Base64 encoding](ch03-binary-to-string.md)
- [List pattern matching with arrays](ch03-list-patterns.md)
- [Checking for overflow](ch03-overflow.md)
- [Returning result types versus throwing exceptions](ch03-result-types.md)

# Chapter 4 Writing, Debugging, and Testing Functions
- [Using lambdas in function implementations](ch04-lambdas.md)
- [Implementing the tester-doer and try patterns](ch04-try-pattern.md)
- [Logging during development and runtime](ch04-logging.md)
- [Understanding `async` and `await`](ch04-async-await.md)

# Chapter 5 Building Your Own Types with Object-Oriented Programming
- [Changing an enum base type for performance](ch05-enum-base-type.md)
- [Implementing functionality using local functions](ch05-local-functions.md)
- [Defining a primary constructor for a class](ch05-primary-constructor.md)
- [Type aliases](ch05-type-alias.md)
- [Limiting flags enum values](ch05-validating-properties.md)
- [Equality of other types](ch05-equality.md)
- [Pattern matching with objects](ch05-pattern-matching.md)

# Chapter 6 Implementing Interfaces and Inheriting Classes
- [Raising and handling events](ch06-events.md)
- [Comparing objects when sorting](ch06-comparing-objects.md)
- [Managing memory with reference and value types](ch06-memory.md)
- [Summarizing custom type choices](ch06-summarizing-custom-types.md)
- [Writing Better Code](ch06-writing-better-code.md)

# Chapter 7 Packaging and Distributing .NET Types
- [Controlling the .NET SDK](ch07-control-sdk.md)
- [New features in modern .NET](ch07-features.md)
- [Working with preview features](ch07-preview-features.md)
- [Decompiling .NET assemblies](ch07-decompiling.md)
- [Porting from .NET Framework to modern .NET](ch07-porting.md)
- [Introducing source generators](ch07-source-generators.md)
- [Improving performance in .NET](ch07-performance.md)

# Chapter 8 Working with Common .NET Types
- [Specialized numbers](ch08-numbers.md)
- [Benchmarking performance and resource usage](ch08-benchmarking.md)
- [.NET Collections Overview](ch08-collections.md)
- [Sorting collections](ch08-sorting-collections.md)
- [Read-only, immutable, and frozen collections](ch08-readonly-frozen.md)
- [Good practice with collections](ch08-good-practice.md)
- [Working with spans, indexes, and ranges](ch08-spans-indexes-ranges.md)
- [Working with Network Resources](ch08-network-resources.md)

# Chapter 9 Processes, Files, Streams, and Serialization
- [Compressing streams](ch09-compression.md)
- [Working with Tar archives](ch09-tar-archives.md)
- [Reading and writing with random access handles](ch09-random-access-handles.md)
- [Handling ambiguous union cases](ch09-ambiguous-unions.md)
- [Serializing object graphs as XML](ch09-serializing-xml.md)
- [JSON Patch implementation improvements](ch09-json-patch.md)
- [Parsing structured text files with `TextFieldParser`](ch09-textfieldparser.md)
- [Working with environment variables](ch09-environment-variables.md)

# Chapter 10 Working with Data Using Entity Framework Core
- [Database Primer](ch11-database-primer.md)
- [Understanding legacy Entity Framework](ch10-legacy-ef.md)
- [Structuring Projects](ch10-structuring-projects.md)
- [Entity-Relationship Diagram for Northwind](ch11-er-diagram.md)
- [Validating asynchronously with DataAnnotations](ch10-async-validation.md)
- [Why the EF Core CLI cannot use data annotations for everything](ch10-data-annotations.md)
- [Loading and tracking patterns with EF Core](ch11-loading-tracking.md)
- [Modifying data with EF Core](ch11-modifying.md)
- [Working with transactions](ch11-transactions.md)
- [Implementing asynchronous methods with EF Core](ch11-ef-core-async.md)
- [Code First EF Core models](ch11-code-first.md)
- [Avoiding EF Core performance traps](ch10-ef-core-performance.md)
- [App Secrets](ch11-app-secrets.md)

# Chapter 11 Querying and Manipulating Data Using LINQ
- [LINQ extension methods](ch12-linq-methods.md)
- [Aggregating and paging sequences](ch12-aggregating.md)
- [Using multiple threads with parallel LINQ](ch12-plinq.md)
- [Working with LINQ to XML](ch12-linq-to-xml.md)
- [Creating your own LINQ extension methods](ch12-custom-linq-methods.md)

# Chapter 12 Building Web Apps Using Blazor
- [Understanding web development](ch11-http-web-techs.md)
- [History of Blazor](ch11-blazor-history.md)
- [Prototyping with Bootstrap](ch11-bootstrap.md)
- [New features in ASP.NET Core](ch11-features.md)
- [ASP.NET Core common classes and methods](ch11-common-classes.md)
- [Configuring services and the HTTP request pipeline](ch11-http-pipeline.md)
- [Understanding automatic cross-origin CSRF protection](ch12-csrf-protection.md)
- [Enabling HTTP/3 and request decompression support](ch11-enabling-http3.md)
- [Understanding `MapStaticAssets`](ch11-mapstaticassets.md)
- [Enabling client-side execution using WebAssembly](ch11-blazor-wasm.md)
- [Enhancing Blazor apps](ch11-enhanced-blazor.md)

# Chapter 13 Building and Consuming Web Services
- [Common responses to a `GET` request](ch13-get-responses.md)
- [Designing web services for case sensitivity](ch13-case-sensitivity.md)
- [Validating Minimal API requests asynchronously](ch13-async-validation.md)
- [Implementing asynchronous operations](ch13-async-endpoints.md)
- [In-memory, distributed, and hybrid caches](ch13-caching.md)
- [Implementing advanced features for web services](ch13-advanced.md)
- [Exercise 13.2 – Practice creating and deleting customers with HttpClient](ch13-exercise-2.md)

# [Errata, Improvements, and Common Mistakes](errata/README.md)

If you find any mistakes in the tenth edition, *C# 15 and .NET 11 - Modern Cross-Platform Development Fundamentals*, or if you have suggestions for improvements, then please [raise an issue in this repository](https://github.com/markjprice/cs15net11/issues) or email me at markjprice (at) gmail.com.
