# Using the PDF edition with Google NotebookLM

> **NotebookLM turns the book PDF into an interactive study companion, not a replacement for reading or coding.**

- [Using the PDF edition with Google NotebookLM](#using-the-pdf-edition-with-google-notebooklm)
  - [Why use NotebookLM rather than a chatbot?](#why-use-notebooklm-rather-than-a-chatbot)
  - [The workflow](#the-workflow)
  - [Example prompts](#example-prompts)
  - [Generating study aids](#generating-study-aids)
  - [Three pass process](#three-pass-process)
  - [Reviewing your own code](#reviewing-your-own-code)
  - [Warnings](#warnings)
  - [Demonstration: using NotebookLM to revise generics](#demonstration-using-notebooklm-to-revise-generics)


## Why use NotebookLM rather than a chatbot?

NotebookLM is especially useful for a large programming book because readers often do not need “the whole book” at once. They need to ask, “Where did Mark explain this?”, “How does this chapter connect to that earlier chapter?”, “Make me quiz questions on this topic”, or “Summarize the parts I should reread before attempting this exercise.”

Google’s own help currently says NotebookLM supports PDF files, Markdown, DOCX, TXT, CSV, PPTX, EPUB, web URLs, Google Docs/Slides/Sheets, images, audio files, and public YouTube videos with captions. It also says each source can contain up to 500,000 words or up to 200 MB, and free users can include up to 50 sources. That makes it a good fit for a long technical book PDF, assuming the reader has legal access to that PDF. 

You can use the PDF edition of this book with Google’s NotebookLM to create a source-grounded study assistant for the book. NotebookLM works differently from a general chatbot. Instead of asking it broad questions from memory, you add sources, such as this book’s PDF, and then ask questions about those sources. This is useful when you want to review a topic, compare explanations across chapters, generate study questions, or find where a concept is introduced.

Use this only with a PDF copy that you are allowed to use. Do not upload books, course material, or private documents unless you have the right to use them in that way. Google says NotebookLM supports uploaded PDF files, among other source types, with per-source limits of up to 500,000 words or 200 MB. Free users can include up to 50 sources in a notebook.

> Add or discover new sources for your notebook - Computer - NotebookLM Help: https://support.google.com/notebooklm/answer/16215270?co=GENIE.Platform%3DDesktop&hl=en

## The workflow

A good workflow is:

1. Create a new notebook called something like **C# and .NET Study Notebook**.
2. Add the PDF edition of this book as a source.
3. Optionally add related sources, such as your own notes, exercise solutions, official documentation pages, or project requirements.
4. Ask targeted questions rather than vague ones.
5. Follow the citations back into the book before trusting or using an answer.
6. Write and run code yourself. NotebookLM can help you study, but it cannot build your understanding for you.

> Google Workspace Updates: New ways to customize and interact with your content in NotebookLM: https://workspaceupdates.googleblog.com/2026/03/new-ways-to-customize-and-interact-with-your-content-in-NotebookLM.html

## Example prompts

NotebookLM is best when you ask it to work from the book. For example:

```text
Using only the book PDF, explain the difference between value types and reference types. Give me a beginner-friendly explanation, then list the chapters or sections I should reread.
```

```text
Create ten short quiz questions about nullable reference types based on the book. Ask one question at a time. Wait for my answer before showing the explanation.
```

```text
Find where the book explains async and await. Summarize the explanation, then list the most common mistakes a beginner might make.
```

```text
I have just finished the chapter on object-oriented programming. Create a revision plan for the next 45 minutes using only material from the book.
```

```text
Compare the book’s explanations of interfaces, abstract classes, and inheritance. Create a table showing when to use each one.
```

```text
Generate a glossary of the twenty most important terms from the chapter on ASP.NET Core, with short definitions and page references or citations where available.
```

```text
I am stuck on the exercise at the end of Chapter 12. Do not solve it for me. Ask me questions that help me reason through the problem.
```

The most important habit is to ask for help that builds understanding. Avoid prompts like this:

```text
Do the exercise for me.
```

Prefer prompts like this:

```text
Give me three hints for the exercise, from least revealing to most revealing. Do not show the final code unless I ask.
```

## Generating study aids

NotebookLM can also generate study aids from the book, such as summaries, quizzes, flashcards, mind maps, audio overviews, video overviews, infographics, reports, and slide decks. Google’s help pages list these as NotebookLM features, and Google has also announced saved conversation history, artifact creation in chat, EPUB support, slide revision, quiz and flashcard improvements, infographic styles, and PPTX export for generated slide decks. Availability may vary by account type, age, language, and region. 

## Three pass process

A useful way to revise a chapter is to use NotebookLM in three passes:

First, ask for orientation:

```text
Summarize this chapter in five paragraphs. Then list the five ideas that a beginner is most likely to misunderstand.
```

Second, ask for active recall:

```text
Create a quiz for this chapter. Ask me one question at a time. After I answer, tell me whether I am correct and quote or cite the part of the source that supports the explanation.
```

Third, ask for practice:

```text
Create three small coding challenges based on this chapter. Make the first easy, the second moderate, and the third a stretch. Do not provide the solutions until I ask.
```

## Reviewing your own code

You can also use NotebookLM after you have written code:

```text
Based on the book’s guidance, review this code for style, correctness, naming, exception handling, and common beginner mistakes. Do not invent rules. Refer back to the book where possible.
```

Then paste your code as a note or add it as a separate source. This works well because NotebookLM can compare your code against the book’s explanations. It should not replace the compiler, tests, debugger, or documentation. Treat it as a study partner that helps you ask better questions.

> **Good practice**: Treat AI output as a study aid, not as authority.

## Warnings

NotebookLM is useful because it works from sources you provide, but it can still summarize incorrectly, miss context, or produce advice that sounds more certain than it should. For programming, always verify important details by reading the cited passage, checking the official documentation when needed, compiling the code, running the tests, and debugging the result.

Before uploading anything, check whether it contains private, confidential, employer-owned, or client-owned information. Google says NotebookLM uses your files, generated outputs, and chat history to build your knowledge base and assist with tasks, and that content is not used to directly train its foundation models unless you choose to provide feedback. It also says feedback content may be reviewed and retained, while Workspace or Education users have different handling for feedback and model training. ([Google Help][2])

## Demonstration: using NotebookLM to revise generics

1. Add the book PDF as a source.
2. Ask:

```text
Using only this book, explain C# generics to a beginner who understands methods and classes but has not used type parameters before.
```

3. Ask:

```text
Find the most important examples of generics in the book. Group them by collections, methods, classes, constraints, and nullable reference types.
```

4. Ask:

```text
Create a five-question quiz about generics. Ask one question at a time and wait for my answer.
```

5. Ask:

```text
Create a small coding exercise that uses `List<T>`, `Dictionary<TKey,TValue>`, and a generic method. Give me requirements first, not the solution.
```

6. After attempting the exercise, paste the code and ask:

```text
Review my solution against the book’s guidance. Identify mistakes, explain why they matter, and give hints before showing corrected code.
```

> Privacy and Terms of Use in NotebookLM - NotebookLM Help: https://support.google.com/notebooklm/answer/17004255?hl=en
