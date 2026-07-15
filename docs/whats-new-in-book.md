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
- Added about a dozen suggested prompts to each chapter to encourage readers to use their preferred AI chatbot to coach them to learn deeper in a responsible way.
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
- 

## Chapter 6

## Chapter 7

## Chapter 8

## Chapter 9

## Chapter 10

## Chapter 11

## Chapter 12

## Chapter 13
