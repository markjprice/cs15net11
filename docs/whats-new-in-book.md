**What's New in the 11th Edition**

There are hundreds of minor fixes and improvements throughout the 11th edition; too many to list individually. 

All [errata](https://github.com/markjprice/cs14net10/blob/main/docs/errata/errata.md) and [improvements](https://github.com/markjprice/cs14net10/blob/main/docs/errata/improvements.md) for the 10th edition (up to mid-September 2026) have been made to the 11th edition. After publishing the 11th edition, any errata and improvements for the 10th edition have been duplicated in both the 10th and 11th edition [errata and improvements](https://github.com/markjprice/cs15net11/blob/main/docs/errata/README.md).

The main changed sections in *C# 15 and .NET 11 - Modern Cross-Platform Development*, 11th edition compared to the 10th edition are listed below.

# All Chapters
- The *Test your knowledge* questions at the end of each chapter have stayed the same for many editions. I have now reviewed, updated, and expanded them all.
- Removed "version-to-version" commentary. In previous editions, I would often say which version of C# or .NET a feature was introduced. I now assume the reader will be using the version the book was written for. I limit historic discussions and version-specific commentary to optional online-only sections.
- Removed ", as shown in the following code/markup:". Earlier Packt editors insisted that I always lead-in to code and commands, but this just adds about 1-2 pages of verbosity to every chapter so I've trimmed that out, unless I need to include highlighted code.

# Chapter 1
- Removed the marketing sections about my other .NET books. 
- Added a new section, *Getting the most from the book’s suggested prompts*, to explain that the book now has dozens of suggested prompts in each chapter for AI chatbots to learn more details about a topic. Beginners often struggle to know what to ask. These prompts are a useful starting point to go deeper into topic areas, and model how to write good prompts.
- Added a box about resetting development settings including keyboard shortcuts.
- Replaced the Polyglot Notebooks extension for VS Code which is now deprecated with one for PowerShell to make it easier to use the scripts I supply to manage VS Code extensions.
- Updated the figures for .NET lifetime to emphasize the shared end-of-life dates of both .NET 10 and .NET 11 and therefore .NET 10 projects should retarget to .NET 11 despite it being STS instead of LTS.
- Moved the sections about top-level programs to *Chapter 2* and *Chapter 4* to avoid overwhelming beginners in their first chapter.
- Moved the *Running a C# code file without a project file* to immediately after building the first two projects and added *Splitting file-based apps across multiple files* and *Referencing another C# file as a library* subsections.
- Moved the *Getting help from documentation, communities, and AI tools* section from an appendix back into the print book, and added *Using Google NotebookLM with the book’s PDF* and *Coding assistants* subsections.

# Chapter 2
- In the *Introducing C#, its compiler, and language versions* section, I removed rows from the tables for older now-irrelevant versions.
- In the *Showing the compiler version* section, I created a new project called `Compiler` instead of using the old `Vocabulary` project that was used later in this chapter. 
- Moved the *Revealing the extent of the C# vocabulary* section online-only with a box to explain it's optional to avoid overwhelming beginners with example code that uses reflection.
- Added a *Importing namespaces* section that contains some of the implicit namespace discussion that used to be in *Chapter 1* as well as subsections that were already in *Chapter 2*, and updated the code and screenshots to show that the feature now also includes the `System.Net.Http.Json` namespace in console apps.
- Added a new *Declaration, assignment, initialization, and instantiation* subsection to explain those terms.
- Moved the *Special real number values* subsection to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch02-special-real-numbers.md
- Moved the *Storing dynamic types* and *Dynamic types with ExpandoObject* subsections to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch02-dynamic.md
- Moved the *Getting and setting the default values for types* subsection to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch02-defaults.md
- Moved the *Custom number formatting* subsection and its large tables of codes to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch02-custom-formatting.md
- Moved the *Passing arguments to a console app* subsection to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch02-arguments.md

# Chapter 3
- Moved the *Why you should always use braces with if statements* subsection to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch03-always-use-braces.md
- Moved the *Adding a new item to a project using Visual Studio* subsection to be optional online-only: https://github.com/markjprice/cs15net11/blob/main/docs/ch03-adding-a-new-item.md

# Chapter 4

# Chapter 5

# Chapter 6

# Chapter 7

# Chapter 8

# Chapter 9

# Chapter 10

# Chapter 11

# Chapter 12

# Chapter 13

# Chapter 14
