- [Parsing structured text files with `TextFieldParser`](#parsing-structured-text-files-with-textfieldparser)
  - [Why C# developers should know about it](#why-c-developers-should-know-about-it)
  - [Reading a comma-delimited file](#reading-a-comma-delimited-file)
  - [Configuring delimited parsing](#configuring-delimited-parsing)
  - [Handling malformed lines](#handling-malformed-lines)
  - [Reading tab-separated or pipe-separated files](#reading-tab-separated-or-pipe-separated-files)
  - [Reading fixed-width files](#reading-fixed-width-files)
  - [Constructing a parser from a file, stream, or reader](#constructing-a-parser-from-a-file-stream-or-reader)
  - [`ReadFields` compared to `ReadLine`](#readfields-compared-to-readline)
  - [Limitations](#limitations)
  - [Summary](#summary)


# Parsing structured text files with `TextFieldParser`

C# developers sometimes overlook useful .NET APIs because they live in namespaces that appear to belong to another language. A good example is `Microsoft.VisualBasic.FileIO.TextFieldParser`.

Despite the namespace name, `TextFieldParser` is not limited to Visual Basic. It is a .NET type that C# code can use too. It provides a simple way to parse structured text files, especially delimited files such as comma-separated values, tab-separated values, and pipe-separated values, as well as fixed-width files.

Microsoft describes `TextFieldParser` as a type that provides methods and properties for parsing structured text files. Its `ReadFields` method reads the current line, returns the field values as a string array, and advances to the next line containing data. The parser can handle both delimited and fixed-width files. Some properties, such as `Delimiters` and `HasFieldsEnclosedInQuotes`, apply to delimited files, while `FieldWidths` applies to fixed-width files.

## Why C# developers should know about it

Many beginners try to parse CSV files by calling `Split(',')`. That works only for the simplest possible data. Real CSV-like files often contain quoted fields, commas inside quoted text, blank fields, and sometimes line breaks inside quoted fields.

For example, this line has three fields, not four:

```text
42,"Bond, James",London
```

A simple `Split(',')` call would incorrectly split `Bond, James` into two fields. A parser that understands quoted fields can read the line correctly.

`TextFieldParser` is useful when:
- You need a built-in parser for small or medium-sized structured text files.
- You want to read CSV-like data without adding a NuGet package.
- You need to handle quoted fields.
- You need to parse fixed-width records.
- You want beginner-friendly code that shows the mechanics of reading records from a file.

For large, complex, high-performance, or production-grade CSV processing, a dedicated library such as `CsvHelper` may still be a better choice. But `TextFieldParser` is handy, available, and easy to teach.

## Reading a comma-delimited file

First, create a sample file named `people.csv`:

```csv
Id,Name,City
1,"Bond, James",London
2,"Moneypenny, Eve",London
3,Q,Unknown
```

The following C# code reads the file:

```cs
using Microsoft.VisualBasic.FileIO;

using TextFieldParser parser = new("people.csv");

parser.TextFieldType = FieldType.Delimited;
parser.SetDelimiters(",");
parser.HasFieldsEnclosedInQuotes = true;

// Read the header row.
string[]? headers = parser.ReadFields();

while (!parser.EndOfData)
{
    string[]? fields = parser.ReadFields();

    if (fields is null)
    {
        continue;
    }

    string id = fields[0];
    string name = fields[1];
    string city = fields[2];

    Console.WriteLine($"{id}: {name} lives in {city}.");
}
```

The output is:

```text
1: Bond, James lives in London.
2: Moneypenny, Eve lives in London.
3: Q lives in Unknown.
```

The important point is that `"Bond, James"` is read as one field because the comma is inside a quoted field.

The `EndOfData` property tells you whether there is more data to read. Microsoft documents it as the property to use when reading from files to determine the end of the data being read.

## Configuring delimited parsing

The most useful delimited-file members are shown in the following table:

| Member                      | Purpose                                                         |     |
| --------------------------- | --------------------------------------------------------------- | --- |
| `TextFieldType`             | Set to `FieldType.Delimited` or `FieldType.FixedWidth`.         |     |
| `SetDelimiters`             | Specifies one or more delimiters, such as `","`, `"\t"`, or `"  | "`. |
| `HasFieldsEnclosedInQuotes` | Specifies whether fields can be enclosed in quotation marks.    |     |
| `ReadFields`                | Reads the next record and returns its fields as a string array. |     |
| `EndOfData`                 | Indicates whether the parser has reached the end of the data.   |     |
| `LineNumber`                | Reports the current line number, useful for diagnostics.        |     |
| `ErrorLine`                 | Returns the text of the malformed line after a parsing error.   |     |
| `ErrorLineNumber`           | Returns the line number of the malformed line.                  |     |

The default `TextFieldType` is delimited, but setting it explicitly makes your intent clearer. Microsoft’s documentation shows `TextFieldType` being used to choose between delimited and fixed-width parsing.

## Handling malformed lines

Text files are often messy. A row might contain an unmatched quote, too many fields, too few fields, or an unexpected format. `ReadFields` can throw a `MalformedLineException` when a field cannot be parsed using the configured format.

The following example reports malformed lines and continues reading:

```csharp id="yot8du"
using Microsoft.VisualBasic.FileIO;

using TextFieldParser parser = new("people.csv");

parser.TextFieldType = FieldType.Delimited;
parser.SetDelimiters(",");
parser.HasFieldsEnclosedInQuotes = true;

while (!parser.EndOfData)
{
    try
    {
        string[]? fields = parser.ReadFields();

        if (fields is null)
        {
            continue;
        }

        Console.WriteLine(string.Join(" | ", fields));
    }
    catch (MalformedLineException ex)
    {
        Console.WriteLine(
            $"Line {parser.ErrorLineNumber} could not be parsed: {parser.ErrorLine}");

        Console.WriteLine(ex.Message);
    }
}
```

This makes `TextFieldParser` useful for import tools because you can report the bad row instead of crashing without context.

## Reading tab-separated or pipe-separated files

CSV is only one kind of delimited file. Some systems use tabs, pipes, semicolons, or other separators.

For a tab-separated file, use `"\t"`:

```csharp id="31y7i3"
using Microsoft.VisualBasic.FileIO;

using TextFieldParser parser = new("people.tsv");

parser.TextFieldType = FieldType.Delimited;
parser.SetDelimiters("\t");
parser.HasFieldsEnclosedInQuotes = true;

while (!parser.EndOfData)
{
    string[]? fields = parser.ReadFields();

    if (fields is not null)
    {
        Console.WriteLine(string.Join(", ", fields));
    }
}
```

For a pipe-separated file, use `"|"`:

```csharp id="bivxj5"
parser.SetDelimiters("|");
```

`SetDelimiters` accepts multiple delimiters, but use that carefully. If your file format is meant to use one delimiter, specifying several can hide data-quality problems.

## Reading fixed-width files

Some older systems do not separate fields with commas or tabs. Instead, each field occupies a fixed number of characters. This is called a fixed-width file.

For example:

```text
00001Bond      London    
00002Q         Unknown   
00003M         London    
```

In this data:
- `Id` uses 5 characters.
- `Name` uses 10 characters.
- `City` uses the rest of the line.

You can parse it like this:

```cs
using Microsoft.VisualBasic.FileIO;

using TextFieldParser parser = new("agents.txt");

parser.TextFieldType = FieldType.FixedWidth;
parser.SetFieldWidths(5, 10, -1);

while (!parser.EndOfData)
{
    string[]? fields = parser.ReadFields();

    if (fields is null)
    {
        continue;
    }

    string id = fields[0].Trim();
    string name = fields[1].Trim();
    string city = fields[2].Trim();

    Console.WriteLine($"{id}: {name} works in {city}.");
}
```

The `-1` width means “read the rest of the line.” Microsoft’s documentation shows the same pattern when configuring a fixed-width parser with `FieldWidths`.

Fixed-width files are less common in new applications, but they still appear in finance, government, education, healthcare, manufacturing, and legacy data exports.

## Constructing a parser from a file, stream, or reader

The examples so far pass a file path to the constructor:

```csharp id="a16vku"
using TextFieldParser parser = new("people.csv");
```

`TextFieldParser` can also be constructed from a `Stream` or `TextReader`. Microsoft documents constructors for paths, streams, and text readers, with overloads that allow an encoding and byte-order-mark detection to be specified.

This is useful when data does not come directly from a local file:

```cs
using Microsoft.VisualBasic.FileIO;
using System.Text;

string csv = """
Id,Name,City
1,"Bond, James",London
2,Q,Unknown
""";

using StringReader reader = new(csv);
using TextFieldParser parser = new(reader);

parser.TextFieldType = FieldType.Delimited;
parser.SetDelimiters(",");
parser.HasFieldsEnclosedInQuotes = true;

while (!parser.EndOfData)
{
    string[]? fields = parser.ReadFields();

    if (fields is not null)
    {
        Console.WriteLine(string.Join(" | ", fields));
    }
}
```

This pattern is also useful for unit tests because the test can provide sample data directly as a string.

## `ReadFields` compared to `ReadLine`

`TextFieldParser` has both `ReadFields` and `ReadLine`, but they do different jobs:
- Use `ReadFields` when you want the parser to understand the file format and return the fields in the current record.
- Use `ReadLine` only when you want raw text without parsing. Microsoft’s documentation notes that `ReadLine` performs no parsing, and an end-of-line character inside a delimited field is treated as the actual end of the line.

For structured data, `ReadFields` is usually the method you want.

| Method       | Use                                                      |
| ------------ | -------------------------------------------------------- |
| `ReadFields` | Parses the next record and returns field values.         |
| `ReadLine`   | Reads a raw line of text without parsing fields.         |
| `ReadToEnd`  | Reads the remaining text without parsing it into fields. |

## Limitations

`TextFieldParser` is useful, but it is not a complete data-import framework:
- It does not map rows automatically to custom objects.
- It does not validate data types for you.
- It does not infer schemas.
- It does not provide asynchronous APIs.
- It does not provide the same breadth of CSV options as mature third-party packages.
- It does not replace a proper CSV library for complex import/export systems.

A realistic approach is:
- Use `TextFieldParser` when you want a built-in parser for straightforward delimited or fixed-width text.
- Use a dedicated CSV library when you need mapping, validation, writing, async processing, extensive configuration, or high-performance batch imports.

Good practice:
- Do not parse CSV with `string.Split(',')` unless you control the file format and know that fields can never contain commas, quotation marks, or line breaks.
- Prefer `TextFieldParser` or a dedicated CSV library for real-world delimited files.
- Set the parser configuration explicitly, especially `TextFieldType`, delimiters, and quote handling.
- Handle `MalformedLineException` when importing data from outside your application.
- Report line numbers when rejecting bad input.
- Trim fields only when the file format says surrounding whitespace is not significant.
- Use `StringReader` in tests so you can test parsing without creating temporary files.

## Summary

`TextFieldParser` is a useful built-in .NET parser for structured text files. It is especially helpful for delimited and fixed-width files, and it handles cases that simple string splitting gets wrong. Although it lives in the `Microsoft.VisualBasic.FileIO` namespace, C# developers can use it directly.

For small tools, teaching examples, data imports, and fixed-width legacy files, `TextFieldParser` is worth knowing. For larger production CSV workflows, compare it with a dedicated CSV library before committing to it.
