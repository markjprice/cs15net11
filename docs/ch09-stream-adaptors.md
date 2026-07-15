- [Reading directly from existing memory](#reading-directly-from-existing-memory)
- [Writing directly into existing memory](#writing-directly-into-existing-memory)
- [Reading from segmented memory](#reading-from-segmented-memory)


# Reading directly from existing memory

`ReadOnlyMemoryStream` adapts an existing `ReadOnlyMemory<byte>` value into a read-only, seekable stream:

```cs
static async Task CopyPayloadAsync(
  ReadOnlyMemory<byte> payload,
  Stream destination)
{
  using Stream source =
    new ReadOnlyMemoryStream(payload);

  await source.CopyToAsync(destination);
}
```

The adapter does not copy the payload into another buffer. Reads are served directly from the supplied memory, and the stream’s `Length` is the length of that memory. This is useful when data already exists in memory but must be passed to an API that accepts only a `Stream`, such as an older parser, serializer, uploader, or HTTP content API.

Read-only does not necessarily mean that the backing storage is immutable. For example, a `ReadOnlyMemory<byte>` might refer to an array that other code can still modify. The stream prevents writes through its own API, but the application must still manage access to the original buffer safely.

# Writing directly into existing memory

`WritableMemoryStream` presents a `Memory<byte>` value as a readable, writable, and seekable stream:

```cs
using System.Text;

byte[] buffer = new byte[64];

using WritableMemoryStream output = new(buffer);

output.Write("status=ready"u8);

int bytesWritten = checked((int)output.Length);

string result = Encoding.UTF8.GetString(
  buffer.AsSpan(0, bytesWritten));

WriteLine(result);
```

The output is:

```text
status=ready
```

The bytes are written directly into `buffer`. The stream does not allocate a second expandable buffer.

A `WritableMemoryStream` has a fixed capacity equal to the length of the supplied memory. Its initial `Length` is zero, and writing increases the length until the capacity is reached. It cannot expand, so an attempt to write beyond the supplied memory throws `NotSupportedException`. Use it when you already have a destination buffer with a known maximum size. Use an ordinary expandable `MemoryStream` when the final size is not known.

# Reading from segmented memory

High-performance networking and parsing APIs often represent data using `ReadOnlySequence<byte>`. A sequence can contain multiple separate memory segments while presenting them as one logical sequence.

`ReadOnlySequenceStream`, in the `System.Buffers` namespace, adapts that sequence into a read-only, seekable stream:

```cs
using System.Buffers;

ReadOnlySequence<byte> sequence = GetBufferedData();

using Stream input =
  new ReadOnlySequenceStream(sequence);

await ProcessDataAsync(input);
```

The stream reads directly across the sequence’s segments. It does not first flatten them into one contiguous byte array. This makes it useful when integrating `System.IO.Pipelines` or another segmented-buffer API with an existing component that accepts a `Stream`. The backing segments must remain valid for as long as the stream uses them.
