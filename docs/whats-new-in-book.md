- [What's New in the 11th Edition](#whats-new-in-the-11th-edition)
  - [Chapter 1](#chapter-1)
  - [Chapter 2](#chapter-2)
  - [Chapter 3](#chapter-3)
  - [Chapter 4](#chapter-4)
  - [Chapter 5](#chapter-5)
  - [Chapter 6](#chapter-6)
  - [Chapter 7](#chapter-7)
  - [Chapter 8](#chapter-8)
  - [Chapter 9](#chapter-9)
  - [Chapter 10](#chapter-10)
  - [Chapter 11](#chapter-11)
  - [Chapter 12](#chapter-12)
  - [Chapter 13](#chapter-13)

> A major goal for the 11th edition is to reduce the page count of the print book so that we can (1) reduce the price and (2) reduce the physical size and weight. 

# What's New in the 11th Edition
- There are hundreds of minor fixes and improvements throughout the 11th edition; too many to list individually. 
- All [errata](https://github.com/markjprice/cs14net10/blob/main/docs/errata/errata.md) and [improvements](https://github.com/markjprice/cs14net10/blob/main/docs/errata/improvements.md) for the 10th edition (up to mid-September 2026) have been made to the 11th edition.
- Added about a dozen suggested [prompts](https://github.com/markjprice/cs15net11/blob/main/prompts/readme.md) to each chapter to encourage readers to use their preferred AI chatbot to coach them to learn deeper in a responsible way.
- The *Test your knowledge* questions at the end of each chapter have stayed the same for many editions. I have now reviewed, updated, and expanded them all, so there are now almost twice as many questions in the book.
- Removed "version-to-version" commentary. In previous editions, I would often say which version of C# or .NET a feature was introduced. I now assume the reader will be using the version the book was written for and they do not care when a feature was introduced. I limit historic discussions and version-specific commentary to optional online-only sections.
- Removed `, as shown in the following code:`. Earlier Packt editors insisted that I always lead-in to code and commands, but this just adds about 1-2 pages of verbosity to every chapter so I've trimmed that out, unless I need to point out highlighted parts.
- Moved some subsections that cover topics that are never used again in the book to become optional online-only in the book's GitHub repository (see details of which subsections below).

> After publishing the 11th edition, any errata and improvements for the 10th edition will be duplicated in both the 10th and 11th edition [errata and improvements](https://github.com/markjprice/cs15net11/blob/main/docs/errata/README.md).

## Chapter 1
- Removed the marketing sections about my other .NET books. 
- Added a new section, *Getting the most from the book’s suggested prompts*, to explain that the book now has dozens of suggested prompts in each chapter. The reader can enter them into their preferred AI chatbot to learn more details about a topic. This is useful because beginners especially struggle to know what to ask, because they don't know what they don't know and don't know the terminology. These prompts are therefore a useful starting point to go deeper into topic areas, and they model how to write good prompts.
- Added a new section, *Using Google NotebookLM with the book’s PDF*, with two screenshots, about loading the book's PDF into NotebookLM so you can ask questions about it interactively with it knowing the full context of your questions.
- Added a box about resetting development settings including keyboard shortcuts. This is important because **Visual C#** and **General** development settings have different keyboard shortcuts.
- Replaced the Polyglot Notebooks extension for VS Code which is now deprecated with one for PowerShell to make it easier to use the scripts I supply to manage VS Code extensions.
- Updated the figures for .NET lifetime to emphasize the shared end-of-life dates of both .NET 10 and .NET 11. The most important reason is to counter the assumption that enterprise organizations should only target LTS releases. But .NET 10 projects could retarget to .NET 11 despite it being STS instead of LTS and still have the same EOL date.
- Moved the sections about top-level programs to *Chapter 2* to avoid overwhelming beginners in their first chapter and avoid disrupting the flow of *Chapter 1*.
- Moved the *Running a C# code file without a project file* to immediately after building the first two projects, and added *Splitting file-based apps across multiple files* and *Referencing another C# file as a library* subsections because these are new features in .NET 11.
- Moved the *Getting help from documentation, communities, and AI* section from a PDF appendix in the 10th edition back into the print book to encourage more readers to actually read it.
- Added a *Coding assistants* subsection to introduce products like **Claude Code**, **ChatGPT Codex**, and **GitHub Copilot**, and how they are better than chatbots for programmers.

## Chapter 2
- In the *Introducing C#, its compiler, and language versions* section, I removed rows from the tables for older now-irrelevant versions.
- In the *Showing the compiler version* section, I created a new project called `Compiler` instead of using the old `Vocabulary` project that was used later in this chapter and has now been made optional. 
- Moved the *Revealing the extent of the C# vocabulary* section online-only with a box to explain it's optional, to avoid overwhelming beginners with example code that uses reflection which is not a beginner feature.
- Added a *Importing namespaces* section that contains some of the implicit namespace discussion that used to be in *Chapter 1* as well as subsections that were already in *Chapter 2*, and updated the code and screenshots to show that the feature now also includes the `System.Net.Http.Json` namespace in console apps in .NET 11.
- Added a new *Declaration, assignment, initialization, and instantiation* subsection to explain those terms.
- Moved the *Special real number values* subsection to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch02-special-real-numbers.md
- Moved the *Storing dynamic types* and *Dynamic types with ExpandoObject* subsections to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch02-dynamic.md
- Moved the *Getting and setting the default values for types* subsection to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch02-defaults.md
- Moved the *Custom number formatting* subsection and its large tables of format codes to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch02-custom-formatting.md
- Moved the *Passing arguments to a console app* subsection to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch02-arguments.md

## Chapter 3
- Added a *Comparison and equality operators* subsection.
- Added a *Understanding operator precedence and associativity* subsection.
- Moved the *Why you should always use braces with if statements* subsection to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch03-always-use-braces.md
- Moved the *Adding a new item to a project using Visual Studio* subsection to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch03-adding-a-new-item.md
- Moved the *Understanding how foreach works internally* subsection to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch03-foreach-internals.md
- Moved the *List pattern matching with arrays* subsection to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch03-list-patterns.md
- Moved the *Rounding numbers and the default rounding rules* subsection to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch03-rounding-numbers.md
- Moved the *Converting from a binary object to a string using Base64 encoding* subsection to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch03-binary-to-string.md
- Moved the *Checking for overflow* section to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch03-overflow.md

## Chapter 4
- Added more explanation about controlling cultures so the output of the coding tasks looks consistent for all readers.
- Added more explanation about recursion to the *Calculating factorials with recursion* subsection to make it clearer how it works.
- Added steps to add XML documentation to the `Factorial` function to show how to document the exceptions that a function might throw, and how to hover over the function call to see that.
- Updated the screenshots of the debugging toolbar and windows to show the latest Visual Studio user interface changes.
- Changed the variables names from `a` and `b` to `firstNumber` and `secondNumber` to follow good practice.
- Update the *Finding bugs with xUnit unit testing* subsection to use **xUnit v3** instead of the now deprecated xUnit v2.
- Added a subsection, *Testing frameworks, Microsoft Testing Platform, and xUnit*, to explain how Microsoft Testing Platform relates to xUnit.net.
- Added a subsection, *xUnit versions and project templates*, to explain how to use xUnit.net v3.
- Added a screenshot of the Visual Studio UI for the **xUnit Test Project** template to the *Creating an xUnit v3 test project* subsection to show how the reader can select the version and test runner.
- Rewrote the *Rethrowing exceptions* subsection to make it clearer, for example, by separating the code blocks for the three ways to catch and throw an exception.

## Chapter 5
- Reorganized the subsection, *Categorizing members by technical type*, and added a new subsection, *Categorizing members by behavior*
- In the *Passing optional parameters* subsection, I renamed the `OptionalParameters` method to `DescribeJounrny` to make it more reaistic and fit with the `Person` class.
- Added a new table to summarize options for passing parameters.
- Added a new major section, *Modeling closed alternatives with union types and pattern matching*.

## Chapter 6
- Moved the *Working with nullable values and references* section to be the first section after setting up the projects for this chapter because all the other sections benefit from the reader knowing how to handle `null` values.
- Moved the *Understanding polymorphism* subsection earlier within its parent section to improve flow and reader understanding.
- Added a new subsection, *Defining closed class hierarchies*.

## Chapter 7
- Added a new figure to explain what each .NET layer does.
- Moved the Central Package Management (CPM) and Package Source Mapping (PSM) theory sections from Chapter 11 to this chapter.

## Chapter 8
- Added information about `System.Random` and its new generic numeric methods.
- Added new subsections in the *Manipulating, comparing, and searching Unicode text* section about runes, *Working with Unicode characters using Rune*, *Searching and modifying strings using runes*, and *Working with user-perceived characters*.
- Added a new subsection in the *Pattern matching with regular expressions* section titled *Avoiding excessive backtracking*.
- Added new subsections in the *Storing multiple objects in collections* section, *Passing arguments to collection expressions* and *Using dictionary expressions[*.

## Chapter 9
- Changed the chapter title to *Processes, Files, Streams, and Serialization*.
- Added a major new section, *Working with processes and tasks*, with subsections like *Starting another program from C#*, *Avoiding blocked apps with Task, async, and await*, *Reading output safely as text, lines, or bytes*, *Timeouts, cancellation, and killing processes*, *Shells, arguments, security, and cross-platform behavior*, and *Understanding runtime async*.
- Added new subsections to the *Reading and writing with streams* section, *Adapting memory and text to streams*, *Reading a string as an encoded stream*, and *Compressing data with Zstandard*.
- Other APIs for compressing streams and archiving files have moved online only: https://github.com/markjprice/cs15net11/blob/main/docs/ch09-compression.md
- Added a new subsection to the *Serializing object graphs* section, *Serializing C# union types with System.Text.Json*.
- Moved the XML serialization section online only: https://github.com/markjprice/cs15net11/blob/main/docs/ch09-serializing-xml.md
- Added an online only optional section, *Parsing structured text files with TextFieldParser*: https://github.com/markjprice/cs15net11/blob/main/docs/ch09-textfieldparser.md
- Moved the *JSON Patch implementation improvements* subsection online only: https://github.com/markjprice/cs15net11/blob/main/docs/ch09-json-patch.md
- Moved the warning about binary serialization using `BinaryFormatter` online only: https://github.com/markjprice/cs15net11/blob/main/docs/ch09-binary-formatter.md

## Chapter 10
- Moved the *Understanding legacy Entity Framework* subsection online only: https://github.com/markjprice/cs15net11/blob/main/docs/ch10-legacy-ef.md
- Moved the *Structuring and configuring .NET projects* section here from the previous edition's first web development chapter so that this data chapter can start building a shared solution named `WebData` for all projects used in Chapter 10 to Chapter 13. This section has subsections for setting up the solution, *Setting up Central Package Management* and *Enabling Package Source Mapping*.
- The subsections in the previous edition that walked through manually creating entity models have been removed. The theory is still covered in the major section, *Defining EF Core models with conventions, annotations, and Fluent API*. Instead the chapter immediately covers *Scaffolding models from an existing database*. This saves a lot of unncecessary repeated reader activity and pages in the book.
- In the database context logger class, I changed the filename to use dashes between the time parts (`"yyyy-MM-dd_HH-mm-ss"`) as well as between the date parts to make it easier to note when exactly the file was created. This helps solve issues with the database.
- In the *If any of the tests fail* subsection I expanded the instructions to help readers diagnose issues if their unit tests for the EF Core model fail.
- Added a figure to show the process of how C# LINQ builds an expression tree, EF Core translates it to provider-specific SQL, the database executes that SQL, and EF Core materializes rows back into objects.
- Added a new subsection to the *Querying EF Core models with LINQ and raw SQL queries* section, *Using no-tracking queries for read-only data* to explain how to use the `AsNoTracking()` method to improve query performance.
- Added a new online only section, *Top ten EF Core performance mistakes beginners make*: https://github.com/markjprice/cs15net11/blob/main/docs/ch10-ef-core-performance.md

## Chapter 11
- Added a new subsection *Query comprehension syntax versus extension methods*.
- Added a new subsection to the *Querying EF Core* titled *Understanding the LINQ provider boundary*.
- Added a major new section titled *Exploring queries with LINQPad*, with subsections like *Downloading and installing LINQPad*, *Filtering and sorting sequences*, and *Projecting sequences into new types* that repeat coding tasks that the reader will have previously completed in a console app, but now in LINQPad. This shows them how much easier using LINQPad can be. In later sections, the reader is then encouraged to try all the LINQ they are taught in both a console app and in LINQPad. For topics like grouping and joining this is especially helpful because LINQPad shows the results visually, but the reader also sees how results would need to be process in an actual app.

## Chapter 12
- Previous editions started the web development chapters with an empty ASP.NET Core project and manually added to it, feature by feature. Although there are good pedagogical reasons for presenting it that way, it's not how real-world web projects with .NET are created. In this edition, the reader starts with a working Blazor Web App project with all needed features enabled.
- In the *Accessing EF Core through dependency injection*, I have switched from registering a transient-scoped `DbContext` to registering a `DbContextFactory` because that is better practice. In a traditional ASP.NET Core app as covered in the previous edition, a scoped database context usually works well because the scope lasts for one HTTP request. An interactive server-side Blazor component is different. After its initial HTTP request, the component can remain active in a server-side circuit while the user triggers many events.
- In the Northwind database context extension method, I changed the algorithm to search upward through parent folders recursively to find the database file instead of specifying one path to look in. This makes connecting to the database more reliable and avoids common errors.
- Added an online only optional section, *Keeping Blazor WebAssembly responsive with a Web Worker*: https://github.com/markjprice/cs15net11/blob/main/docs/ch12-web-worker.md

## Chapter 13
- In the *Understanding HTTP requests and responses* section, I simplified the table and moved the full version online only: https://github.com/markjprice/cs15net11/blob/main/docs/ch13-get-responses.md
- Added a major new section, *Modeling business outcomes with a union*, with subsections including *Defining order-fulfillment logic using union types*, *Using the union class library in the web service*, and *Trying out the web service using HTTP/REST tools*.
- Added an online only optional section about asynchronous validation with Minimal API web services: https://github.com/markjprice/cs15net11/blob/main/docs/ch13-async-validation.md
- Added a new subsection, *Integration testing a web service*.
- Added a new subsection, *Compressing HTTP content with Zstandard*.
- Added a major new section, *Exposing application capabilities to AI clients with MCP*, with subsections like *MCP Server project template* and *Creating an MCP server for Northwind customers*.

