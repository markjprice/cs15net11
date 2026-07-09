# Reading and writing with random access handles

For the first 20 years of .NET’s life, the only API to work directly with files was the one for stream classes. These work great for automated tasks that only need to process data sequentially. But when a human interacts with the data, they often want to jump around and return multiple times to the same location.

There is an API for working with files without needing a file stream and in a random-access way. Let’s see a simple example:

1.	Use your preferred code editor to add a new **Console App** / `console` project named `RandomAccess` to the `Chapter09` solution.
2.	In the project file, add an element to import the `System.Console` class statically and globally.
3.	In `Program.cs`, delete the existing statements, and then get a handle to a file named `coffee.txt`:
```cs
using Microsoft.Win32.SafeHandles; // To use SafeFileHandle.
using System.Text; // To use Encoding.

using SafeFileHandle handle =
  File.OpenHandle(path: "coffee.txt",
    mode: FileMode.OpenOrCreate,
    access: FileAccess.ReadWrite);
```

4.	Write some text encoded as a byte array, and then store it in a read-only memory buffer to the file:
```cs
string message = "Café £4.39";
ReadOnlyMemory<byte> buffer = new(Encoding.UTF8.GetBytes(message));
await RandomAccess.WriteAsync(handle, buffer, fileOffset: 0);
```

5.	To read from the file, get the length of the file, allocate a memory buffer for the contents using that length, and then read the file:
```cs
long length = RandomAccess.GetLength(handle);
Memory<byte> contentBytes = new(new byte[length]);
await RandomAccess.ReadAsync(handle, contentBytes, fileOffset: 0);
string content = Encoding.UTF8.GetString(contentBytes.ToArray());
WriteLine($"Content of file: {content}");
```
6.	Run the code, and note the content of the file:
```
Content of file: Café £4.39
```
