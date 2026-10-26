# Prompts for *Chapter 9 Processes, Files, Streams, and Serialization*

```
Why can reading stdout and stderr from a child process cause a
deadlock? Please explain with a diagram and then show safe C# code using
modern .NET process APIs.
```

```
Please teach me how CancellationTokenSource and CancellationToken
work with async APIs. Use process execution and file I/O examples.
```

```
Please explain why building command strings from user input can
be dangerous. Compare shell injection, argument injection, and safe use
of ArgumentList in C#.
```

```
Why should C# code use Path.Combine, Path.Join, or Path.GetFullPath
instead of manually concatenating strings to build paths?
```

```
Please explain the difference between File, FileInfo, Directory,
DirectoryInfo, and DriveInfo. Give examples of when each one is the
better choice.
```

```
Please explain the difference between a file path, a directory
path, a relative path, an absolute path, and a special folder in .NET.
```

```
Please explain FileMode, FileAccess, and FileShare in C#. Use
examples that show how file locks affect other processes.
```

```
Please explain the difference between storage streams, function
streams, stream helpers, and stream adapters. Compare FileStream,
MemoryStream, StringStream, ReadOnlyMemoryStream, WritableMemoryStream,
ReadOnlySequenceStream, GZipStream, StreamReader, and StreamWriter. Show
how these types can be combined into a pipeline.
```

```
Please explain the difference between storage streams, function
streams, and stream helper types. Show how they can be combined into a
pipeline. Why does the order of compression and encryption matter? Explain
why compressing before encrypting is usually better than encrypting
before compressing.
```

```
Please explain Zstandard compression in .NET. Compare it with GZip,
Deflate, Brotli, and ZIP, and explain when Zstandard is a good choice.
Then explain the difference between an archive format and a compression
format. Compare TAR, ZIP, GZip, Brotli, and Zstandard.
```

```
Please explain UTF-8, UTF-16, and UTF-32 to a C# beginner. Use the
string `Café £4.39` and show how many bytes each encoding uses. Why does
casting a `byte` to `char` produce confusing output for UTF-8 data? Explain
the difference between bytes, code units, code points, and characters.
```

```
Please explain what an object graph is and why serialization
is useful. Use a family tree or game-save example. Compare JSON, XML,
Protobuf, and MessagePack for serialization. When would a .NET developer
choose each format?
```

```
Compare `Newtonsoft.Json` and `System.Text.Json`. Explain feature
differences, performance differences, and migration issues.
```

```
Please explain what a JSON schema is and why generating one from
a .NET type can help validate API data.
```

```
Please explain what `[JsonInclude]` does in `System.Text.Json`. Why is
it useful with private setters, fields, and encapsulated domain models?
```

```
Please explain how to parse CSV-like files in C# without using
string.Split(','). Compare TextFieldParser, CsvHelper, and manual parsing.
```
