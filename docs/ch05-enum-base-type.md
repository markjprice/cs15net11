# Changing an enum base type for performance

The preceding section was about storing multiple values using an `enum` type. It’s about `enum` types decorated with the `[Flags]` attribute that use bitwise operations to efficiently store those multiple values. In the code example, we defined an `enum` for the Seven Wonders of the Ancient World, so it only needed seven combinable values (and `0` for `None`).

The preceding section was not about making all your `enum` types derive from `byte` to make your code faster, because that would be bad advice.

On March 18, 2024, Nick Chapsas posted a YouTube video titled *Turn All Your Enums Into Bytes Now! | Code Cop #014*, which you can watch at the following link: https://www.youtube.com/watch?v=1gWzE9SIGkQ. He criticized blog articles that recommend changing the default base integer type of `enum` types from `int` to `byte` to improve performance.

The original designers of the C# language spent effort on implementing the ability for `enum` types to derive from other integer types than just the default `int`. For example, you can use fewer bytes by using a positive integer like `byte` or `ushort`, or the same or more bytes by using a positive integer like `uint` or `ulong`. They implemented this feature because sometimes a .NET developer will need this capability.

I think it is important that my readers know that they can do it when necessary. Microsoft’s official guidance states, “Even though you can change this underlying type, it is not necessary or recommended for most scenarios. No significant performance gain is achieved by using a data type that is smaller than `Int32`,” as you can read at the following link: https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca1028.

For those developers who object to changing any enum from `int` to some other integer, there is a compiler code analysis warning that I linked to above. If enabled, it will trigger if you set an `enum` to anything other than `int`: “CA1028: Enum storage should be Int32.” This warning is not enabled by default because Microsoft knows that there are legitimate reasons why a developer might need to use it.

Let’s see some real-life examples of when you would need to change an `enum` from deriving from `int` to deriving from another integer type:
- You need to transfer data as a binary stream via a serial connection to an embedded device and you must carefully follow the protocol, or you are defining your own serial messaging protocol, and you want to reduce the packet size to make the best use of your available bandwidth.
- You have SQL tables with millions of records where some of the columns are `enum` values. Setting those columns to `tinyint` with a matching `enum : byte` property in the C# entity class can make indexes perform better by being smaller and reducing the number of page reads from disk. Some developers will work on systems that are 30 or more years old with spinning metal disks. Not everyone is deploying to a modern 64-bit OS with modern hardware.
- You need to reduce the size of a `struct` because it will be created 100,000 times per second on resource-constrained hardware, or you have game code that is set to use `byte` and `short` because you have millions of those `byte` and `short` values in contiguous arrays for the game’s data. You would gain a fair bit of performance doing this, especially from a cache point of view.
- You want to increase the size of the integer to store more than 31 options in a flag enum. The default `int` only allows 31 options because one bit is needed to indicate a negative number. Changing to `uint` would add an extra 32nd value without using any more space in memory. Changing to `ulong` would give 64 options. Changing to `ushort` would allow the same 16 options in half the bytes.

*Table 5.3* summarizes the number of options available for each integer type when used as the base type for an `enum`:

Base Type|Maximum Values
---|---
sbyte|7
byte|8
short|15
ushort|16
int|31
uint|32
long|63
ulong|64

*Table 5.3: Base enum types and their maximum values*

So if a `uint` would give one extra option, why does C# default to using an `int` as the base type for enums?

C# enums default to `int` as their underlying type primarily because it's the most common and efficient integer type in .NET, not because it's the optimal choice in terms of bit range. It's a trade-off based on performance, interoperability, and historical convention rather than capacity.

`uint` gives one more positive value, but signed `int` is the default integer type in C# and .NET. It’s used in `for` loops, array indexing, and almost every system API. That means an `enum` based on `int` is easier to work with by default, avoiding implicit cast warnings or needing explicit conversions.

`int` is also **Common Language Specification (CLS)**-compliant; `uint` is not. The CLS defines a set of rules for .NET language interoperability and it doesn’t include `uint`. So if you define an `enum` with `uint`, it can’t be used as is from some .NET languages like Visual Basic .NET. `int` enums are just safer across the .NET ecosystem.

> **Good practice**: Use the `enum` values to store combinations of discrete options. Derive an `enum` type from `byte` if there are up to eight options, from `ushort` if there are up to 16 options, from `uint` if there are up to 32 options, and from `ulong` if there are up to 64 options.
