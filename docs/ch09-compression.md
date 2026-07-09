# Compressing streams

XML is relatively verbose, so it takes up more space in bytes than plain text. We could squeeze the XML using a common compression algorithm, known as `gzip` (to match the Linux command) or **GZIP** (to match the RFC specification), or we could use an implementation of the Brotli compression algorithm. In performance, Brotli is like the algorithm used in DEFLATE and GZIP, but the output is about 20% denser.

Let’s compare the two compression algorithms:
1.	Add a new class file named `Program.Compress.cs`.
2.	In `Program.Compress.cs`, write statements to use instances of `GZipStream` or `BrotliStream` to create a compressed file that contains the same XML elements as before, and then decompress it while reading it and outputting to the console:
```cs
using Packt.Shared; // To use Viper.
using System.IO.Compression; // To use BrotliStream, GZipStream.
using System.Xml; // To use XmlWriter, XmlReader.

partial class Program
{
  private static void Compress(string algorithm = "gzip")
  {
    // Define a file path using the algorithm as file extension.
    string filePath = Combine(
      CurrentDirectory, $"streams.{algorithm}");

    FileStream file = File.Create(filePath);
    Stream compressor;

    if (algorithm == "gzip")
    {
      compressor = new GZipStream(file, CompressionMode.Compress);
    }
    else
    {
      compressor = new BrotliStream(file, CompressionMode.Compress);
    }

    using (compressor)
    {
      using (XmlWriter xml = XmlWriter.Create(compressor))
      {
        xml.WriteStartDocument();
        xml.WriteStartElement("callsigns");
        foreach (string item in Viper.Callsigns)
        {
          xml.WriteElementString("callsign", item);
        }
      }
    } // Also closes the underlying stream.

    OutputFileInfo(filePath);

    // Read the compressed file.
    WriteLine("Reading the compressed XML file:");
    file = File.Open(filePath, FileMode.Open);
    Stream decompressor;

    if (algorithm == "gzip")
    {
      decompressor = new GZipStream(
        file, CompressionMode.Decompress);
    }
    else
    {
      decompressor = new BrotliStream(
        file, CompressionMode.Decompress);
    }

    using (decompressor)
    using (XmlReader reader = XmlReader.Create(decompressor))
    while (reader.Read())
    {
      // Check if we are on an element node named callsign.
      if ((reader.NodeType == XmlNodeType.Element)
        && (reader.Name == "callsign"))
      {
        reader.Read(); // Move to the text inside element.
        WriteLine($"{reader.Value}"); // Read its value.
      }

      // Alternative syntax with property pattern matching:
      // if (reader is { NodeType: XmlNodeType.Element,
      //   Name: "callsign" })
    }
  }
}
```

The code that uses the `decompressor` object does not use the simplified `using` syntax. Instead, it uses the fact that using blocks can omit their braces for a single “statement,” just like `if` statements. Remember that if statements can have explicit braces even if only one statement is executed within a block:
```cs
if (c == 1)
{
  // Execute a single statement.
}

if (c == 1)
  // Execute a single statement.

using (someObject)
{
  // Execute a single statement.
}

using (someObject)
  // Execute a single statement
```

In the preceding code, `using (XmlReader reader = XmlReader.Create(decompressor))` and the entire `while (reader.Read()) { ... }` block are equivalent to single statements, so we can remove the braces, and the code works as expected.

3.	In `Program.cs`, add calls to `Compress` with parameters to use the gzip and brotli algorithms:
```cs
SectionTitle("Compressing streams");
Compress(algorithm: "gzip");
Compress(algorithm: "brotli");
```

4.	Run the code, and compare the sizes of the XML file and the compressed XML file using the gzip and brotli algorithms:
```
**** File Info ****
File: streams.gzip
Path: C:\cs15net11\Chapter09\Streams\bin\Debug\net11.0
Size: 151 bytes.
/------------------
¬?
z?{??}En?BYjQqf~???????Bj^r~Jf^??RiI??????MrbNNqfz^1?i?QZ??Zd?@H?$%?&gc?t,
?????*????H?????t?&?d??%b??H?aUPbrjIQ"??b;????9
------------------/
Reading the compressed XML file:
Husker
Starbuck
Apollo
Boomer
Bulldog
Athena
Helo
Racetrack

**** File Info ****
File: streams.brotli
Path: C:\cs15net11\Chapter09\Streams\bin\Debug\net11.0
Size: 117 bytes.
/-------------------
 ??d?&?_????\@?Gm????/?h>?6????? ??^?__???wE?'?t<J??]??
???b?\fA?>?+??F??]
?T?\?~??A?J?Q?q6 ?-??
???
--------------------/
Reading the compressed XML file:
Husker
Starbuck
Apollo
Boomer
Bulldog
Athena
Helo
Racetrack
```

We can summarize the file sizes as follows:
- Uncompressed: 320 bytes
- GZIP-compressed: 151 bytes
- Brotli-compressed: 117 bytes

As well as choosing a compression mode, you can also choose a compression level. You can learn more about this at the following link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.compressionlevel.
