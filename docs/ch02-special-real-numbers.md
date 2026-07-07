# Special real number values

The `float` and `double` types have some useful special values: 
- `NaN` represents *not-a-number* (for example, the result of dividing zero by zero)
- `Epsilon` represents the smallest positive number that can be stored in a float or double
- `PositiveInfinity` and `NegativeInfinity` represent infinitely large positive and negative values

The `float` and `double` types also have methods for checking for these special values, such as `IsInfinity` and `IsNaN`.

Let’s write some code statements to see what some of these special values look like:

1.	Add statements to output some special double values:
```cs
#region Special float and double values

Console.WriteLine($"double.Epsilon: {double.Epsilon}");
Console.WriteLine($"double.Epsilon to 324 decimal places: {double.Epsilon:N324}");
Console.WriteLine($"double.Epsilon to 330 decimal places: {double.Epsilon:N330}");

const int col1 = 37; // First column width.
const int col2 = 6; // Second column width.

string line = new string('-', col1 + col2 + 3);
Console.WriteLine(line);
Console.WriteLine($"{"Expression",-col1} | {"Value",col2}");
Console.WriteLine(line);
Console.WriteLine($"{"double.NaN",-col1} | {double.NaN,col2}");
Console.WriteLine($"{"double.PositiveInfinity",-col1} | {double.PositiveInfinity,col2}");
Console.WriteLine($"{"double.NegativeInfinity",-col1} | {double.NegativeInfinity,col2}");
Console.WriteLine(line);
Console.WriteLine($"{"0.0 / 0.0",-col1} | {0.0 / 0.0,col2}");
Console.WriteLine($"{"3.0 / 0.0",-col1} | {3.0 / 0.0,col2}");
Console.WriteLine($"{"-3.0 / 0.0",-col1} | {-3.0 / 0.0,col2}");
Console.WriteLine($"{"3.0 / 0.0 == double.PositiveInfinity",-col1} | {3.0 / 0.0 == double.PositiveInfinity,col2}");
Console.WriteLine($"{"-3.0 / 0.0 == double.NegativeInfinity",-col1} | {-3.0 / 0.0 == double.NegativeInfinity,col2}");
Console.WriteLine($"{"0.0 / 3.0",-col1} | {0.0 / 3.0,col2}");
Console.WriteLine($"{"0.0 / -3.0",-col1} | {0.0 / -3.0,col2}");
Console.WriteLine(line);

#endregion
```

2.	Run the code and view the result:
```text
double.Epsilon: 5E-324
double.Epsilon to 324 decimal places: 0.000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000005
double.Epsilon to 330 decimal places: 0.000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000004940656
----------------------------------------------
Expression                            |  Value
----------------------------------------------
double.NaN                            |    NaN
double.PositiveInfinity               |      8
double.NegativeInfinity               |     -8
----------------------------------------------
0.0 / 0.0                             |    NaN
3.0 / 0.0                             |      8
-3.0 / 0.0                            |     -8
3.0 / 0.0 == double.PositiveInfinity  |   True
-3.0 / 0.0 == double.NegativeInfinity |   True
0.0 / 3.0                             |      0
0.0 / -3.0                            |     -0
----------------------------------------------
```

Note the following:
- `NaN` outputs as NaN. (NaN means “not a number”.) It can be generated from an expression of zero divided by zero.
- The `PositiveInfinity` value outputs as an `8`, which looks like an infinity symbol on its side. It can be generated from an expression of any positive real number divided by zero.
- The `NegativeInfinity` value outputs as `-8`, which looks like an infinity symbol on its side with a negative sign before it. It can be generated from an expression of any negative real number divided by zero.
- Zero divided by any positive real number is zero.
- Zero divided by any negative real number is negative zero.
- Epsilon is slightly less than 5E-324, represented using scientific notation: https://en.wikipedia.org/wiki/Scientific_notation.

# New number types and unsafe code

 Like `float` and `double`, the `System.Half` type can store real numbers. It normally uses two bytes of memory. 
 
 Like `int` and `uint`, the `System.Int128` and `System.UInt128` types can store signed (positive and negative) and unsigned (only zero and positive) integer values. They normally use 16 bytes of memory.

For these new number types, the `sizeof` operator only works in an unsafe code block, and you must compile the project using an option to enable unsafe code. Let’s explore how this works:

1.	In the `Numbers` project, in `Program.cs`, at the bottom of the file, type statements to show the size of the `Half` and `Int128` number data types:
```cs
unsafe
{
  Console.WriteLine($"Half uses {sizeof(Half)} bytes and can store numbers in the range {Half.MinValue:N0} to {Half.MaxValue:N0}.");
  Console.WriteLine($"Int128 uses {sizeof(Int128)} bytes and can store numbers in the range {Int128.MinValue:N0} to {Int128.MaxValue:N0}.");
}
```

2.	In `Numbers.csproj`, add an element to enable unsafe code, as shown in the following markup:
```xml
<PropertyGroup>
  <OutputType>Exe</OutputType>
  <TargetFramework>net11.0</TargetFramework>
  <ImplicitUsings>enable</ImplicitUsings>
  <Nullable>enable</Nullable>
  <AllowUnsafeBlocks>True</AllowUnsafeBlocks>
</PropertyGroup>
```

3.	Run the `Numbers` project and note the sizes of the two new number types:
```text
Half uses 2 bytes and can store numbers in the range -65,504 to 65,504.
Int128 uses 16 bytes and can store numbers in the range -170,141,183,460,
469,231,731,687,303,715,884,105,728 to 170,141,183,460,469,231,731,687,
303,715,884,105,727.
```
The `sizeof` operator requires an unsafe code block, except for the commonly used types such as `int` and `byte`. You can learn more about `sizeof` at the following link: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/sizeof. 

Unsafe code cannot have its safety verified. You can learn more about unsafe code blocks at the following link: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/unsafe-code.
