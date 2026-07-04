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

> A major goal for the 11th edition is to reduce the page count of the print book so that we can (1) reduce the price of the book, and (2) reduce the physical size and weight of the book. 

# What's New in the 11th Edition
- There are hundreds of minor fixes and improvements throughout the 11th edition; too many to list individually. 
- All [errata](https://github.com/markjprice/cs14net10/blob/main/docs/errata/errata.md) and [improvements](https://github.com/markjprice/cs14net10/blob/main/docs/errata/improvements.md) for the 10th edition (up to mid-September 2026) have been made to the 11th edition. 
- The *Test your knowledge* questions at the end of each chapter have stayed the same for many editions. I have now reviewed, updated, and expanded them all.
- Removed "version-to-version" commentary. In previous editions, I would often say which version of C# or .NET a feature was introduced. I now assume the reader will be using the version the book was written for and does not care when a feature was introduced. I limit historic discussions and version-specific commentary to optional online-only sections.
- Removed `, as shown in the following code/markup:`. Earlier Packt editors insisted that I always lead-in to code and commands, but this just adds about 1-2 pages of verbosity to every chapter so I've trimmed that out, unless I need to point out highlighted parts.
- Moved subsections that cover topics that are never used again in the book to become optional online-only in the book's GitHub repository (see details of which subsections below).

> After publishing the 11th edition, any errata and improvements for the 10th edition will be duplicated in both the 10th and 11th edition [errata and improvements](https://github.com/markjprice/cs15net11/blob/main/docs/errata/README.md).

## Chapter 1
- Removed the marketing sections about my other .NET books. 
- Added a new section, *Getting the most from the book’s suggested prompts*, to explain that the book now has dozens of suggested prompts in each chapter. The reader can enter them into their preferred AI chatbot to learn more details about a topic. This is useful because beginners especially struggle to know what to ask, because they don't know what they don't know and don't know the terminology. These prompts are therefore a useful starting point to go deeper into topic areas, and they model how to write good prompts.
- Added a box about resetting development settings including keyboard shortcuts. This is important because **Visual C#** and **General** development settings have different keyboard shortcuts.
- Replaced the Polyglot Notebooks extension for VS Code which is now deprecated with one for PowerShell to make it easier to use the scripts I supply to manage VS Code extensions.
- Updated the figures for .NET lifetime to emphasize the shared end-of-life dates of both .NET 10 and .NET 11. The most important reason is to counter the assumption that enterprise organizations should only target LTS releases. But .NET 10 projects could retarget to .NET 11 despite it being STS instead of LTS and still have the same EOL date.
- Moved the sections about top-level programs to *Chapter 2* to avoid overwhelming beginners in their first chapter and avoid disrupting the flow of *Chapter 1*.
- Moved the *Running a C# code file without a project file* to immediately after building the first two projects, and added *Splitting file-based apps across multiple files* and *Referencing another C# file as a library* subsections because these are new features in .NET 11.
- Moved the *Getting help from documentation, communities, and AI tools* section from a PDF appendix back into the print book to encourage more readers to actually read it, and added *Using Google NotebookLM with the book’s PDF* and *Coding assistants* subsections.

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
- Moved the *Why you should always use braces with if statements* subsection to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch03-always-use-braces.md
- Moved the *Adding a new item to a project using Visual Studio* subsection to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch03-adding-a-new-item.md
- Moved the *Understanding how foreach works internally* subsection to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch03-foreach-internals.md
- Moved the *List pattern matching with arrays* subsection to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch03-list-patterns.md
- Moved the *Rounding numbers and the default rounding rules* subsection to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch03-rounding-numbers.md
- Moved the *Checking for overflow* section to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch03-overflow.md

## Chapter 4


## Chapter 5

## Chapter 6

## Chapter 7

## Chapter 8

## Chapter 9

## Chapter 10

## Chapter 11

## Chapter 12

## Chapter 13
