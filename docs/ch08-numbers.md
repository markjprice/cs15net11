# Specialized numbers

- [Specialized numbers](#specialized-numbers)
  - [Complex numbers](#complex-numbers)
  - [Understanding native-sized integers](#understanding-native-sized-integers)
  - [Using the BFloat16 floating-point type](#using-the-bfloat16-floating-point-type)
    - [Converting values to BFloat16](#converting-values-to-bfloat16)
    - [Performing calculations](#performing-calculations)
    - [Comparing BFloat16 with Half](#comparing-bfloat16-with-half)


## Complex numbers

A complex number can be expressed as `a + bi`, where `a` and `b` are real numbers and `i` is an imaginary unit, where `i2 = −1`. If the real part a is zero, it is a purely imaginary number. If the imaginary part `b` is zero, it is a real number.

Complex numbers have practical applications in many science, technology, engineering, and mathematics (STEM) fields of study. They are added by separately adding the real and imaginary parts of the summands; consider this:
```
(a + bi) + (c + di) = (a + c) + (b + d)i
```

Let’s explore complex numbers:
1.	In `Program.cs`, add statements to add two complex numbers:
```cs
Complex c1 = new(real: 4, imaginary: 2);
Complex c2 = new(real: 3, imaginary: 7);
Complex c3 = c1 + c2;

// Output using the default ToString implementation.
WriteLine($"{c1} added to {c2} is {c3}");

// Output using a custom format.
WriteLine("{0} + {1}i added to {2} + {3}i is {4} + {5}i",
  c1.Real, c1.Imaginary,
  c2.Real, c2.Imaginary,
  c3.Real, c3.Imaginary);
```

2.	Run the code and view the result:
```
<4; 2> added to <3; 7> is <7; 9>
4 + 2i added to 3 + 7i is 7 + 9i
```

## Understanding native-sized integers

C# has the `nint` and `nuint` keyword aliases for native-sized integers, meaning that the storage size for the integer value is platform-specific.

They store a 32-bit integer in a 32-bit process and `sizeof()` returns 4 bytes; they store a 64-bit integer in a 64-bit process and `sizeof()` returns 8 bytes. The aliases represent pointers to the integer value in memory, which is why their .NET names are `IntPtr` and `UIntPtr`. The actual storage type will be either `System.Int32` or `System.Int64`, depending on the process.

In a 64-bit process, let’s say we have the following code:
```cs
WriteLine($"Environment.Is64BitProcess = {Environment.Is64BitProcess}");
WriteLine($"int.MaxValue = {int.MaxValue:N0}");
WriteLine($"nint.MaxValue = {nint.MaxValue:N0}");
```

This produces the following output:
```
Environment.Is64BitProcess = True
int.MaxValue = 2,147,483,647
nint.MaxValue = 9,223,372,036,854,775,807
```

## Using the BFloat16 floating-point type

`BFloat16` is a 16-bit floating-point type designed primarily for machine learning, artificial intelligence, and other workloads that process large collections of approximate numeric values.

The type is declared in the `System.Numerics` namespace:

```csharp
using System.Numerics;
```

Like `Half`, a `BFloat16` value occupies 16 bits, which is half the storage required by a `float`. The two types divide those bits differently, however.

A floating-point value is made from three components: a sign, an exponent, and a significand, sometimes informally called the mantissa. The exponent determines the range of magnitudes that can be represented, while the significand determines the precision.

The following table compares the most relevant binary floating-point types:

| .NET type  | Total bits | Exponent bits | Explicit significand bits |   Approximate precision |
| ---------- | ---------: | ------------: | ------------------------: | ----------------------: |
| `Half`     |         16 |             5 |                        10 |   3 to 4 decimal digits |
| `BFloat16` |         16 |             8 |                         7 |   2 to 3 decimal digits |
| `float`    |         32 |             8 |                        23 |   6 to 9 decimal digits |
| `double`   |         64 |            11 |                        52 | 15 to 17 decimal digits |

`BFloat16` has the same eight-bit exponent as `float`, so it can represent approximately the same range of very small and very large magnitudes. It achieves its smaller size by retaining far fewer significand bits. It therefore has much less precision than `float`.

This trade-off is useful in machine learning. Neural-network operations often process millions or billions of parameters, activations, and intermediate values. Reducing each value from 32 bits to 16 bits can reduce memory usage and memory bandwidth requirements. On hardware with native BFloat16 instructions, it can also improve calculation throughput.

> **Good Practice:** Use `BFloat16` when reduced precision is an intentional part of a machine-learning, scientific, or high-throughput numeric design. Do not use it merely because it consumes less memory.

### Converting values to BFloat16

C# does not have a built-in literal suffix for `BFloat16`. Create a value by explicitly converting another numeric type:

```csharp
using System.Numerics;

float original = 1.234567f;

BFloat16 reduced = (BFloat16)original;
float restored = (float)reduced;

Console.WriteLine($"Original: {original}");
Console.WriteLine($"BFloat16: {reduced}");
Console.WriteLine($"Restored: {restored}");
```

Converting the `float` value to `BFloat16` discards precision. Converting it back to `float` does not restore the lost bits. The resulting `float` has more storage space, but it still represents the rounded BFloat16 value.

The conversion is explicit because it can lose information:

```csharp
BFloat16 value = (BFloat16)123.456f;
```

Without the cast, the compiler prevents an accidental narrowing conversion:

```csharp
// Does not compile because the conversion can lose precision.
BFloat16 value = 123.456f;
```

### Performing calculations

`BFloat16` implements .NET's generic mathematics interfaces, including `IBinaryFloatingPointIeee754<TSelf>`. It supports familiar arithmetic, comparison, parsing, formatting, and mathematical operations:

```csharp
using System.Numerics;

BFloat16 left = (BFloat16)10.5f;
BFloat16 right = (BFloat16)2.25f;

BFloat16 sum = left + right;
BFloat16 product = left * right;

Console.WriteLine($"{left} + {right} = {sum}");
Console.WriteLine($"{left} * {right} = {product}");
Console.WriteLine($"Square root: {BFloat16.Sqrt(left)}");
```

Be aware that the result of each operation is represented with BFloat16 precision. Rounding errors can therefore accumulate more quickly than they would with `float` or `double`.

In many machine-learning systems, BFloat16 values are used for storing inputs and model parameters, while some calculations or accumulations are performed using `float`. This is an example of *mixed-precision arithmetic*: different numeric formats are used at different stages to balance memory use, speed, range, and accuracy.

For example, you could convert values to `float` before accumulating them:

```csharp
using System.Numerics;

BFloat16[] values =
[
    (BFloat16)1.1f,
    (BFloat16)2.2f,
    (BFloat16)3.3f
];

float total = 0;

foreach (BFloat16 value in values)
{
    total += (float)value;
}

Console.WriteLine($"Total: {total}");
```

The individual values have already been rounded to BFloat16 precision, but the running total is maintained as a `float`, avoiding an additional BFloat16 rounding step after every addition.

### Comparing BFloat16 with Half

Although both types occupy 16 bits, `Half` and `BFloat16` are optimized for different goals.

`Half` provides more precision but a narrower numeric range. `BFloat16` provides less precision but retains approximately the same range as `float`.

Conceptually, `BFloat16` can be thought of as a shortened `float`:

```text
float
┌──────┬──────────┬─────────────────────────┐
│ sign │ exponent │       significand       │
│ 1 bit│  8 bits  │         23 bits         │
└──────┴──────────┴─────────────────────────┘

BFloat16
┌──────┬──────────┬─────────────┐
│ sign │ exponent │ significand │
│ 1 bit│  8 bits  │   7 bits    │
└──────┴──────────┴─────────────┘

Half
┌──────┬──────────┬────────────────┐
│ sign │ exponent │  significand   │
│ 1 bit│  5 bits  │    10 bits     │
└──────┴──────────┴────────────────┘
```

Choose between them based on what matters most:

* Use `Half` when 16-bit storage and greater precision are more important than retaining the range of `float`.
* Use `BFloat16` when retaining a `float`-like range is more important than precision.
* Use `float` for general-purpose, single-precision calculations.
* Use `double` when greater precision is required.
* Use `decimal` for base-10 calculations such as money, where predictable decimal rounding is more important than floating-point performance.

`BFloat16` is not intended for financial values, measurements requiring fine precision, identifiers, counters, or ordinary business calculations. Its main benefit appears when software, data formats, libraries, and hardware are designed to work with the format together.

> **Warning:** A smaller numeric type does not automatically make an individual calculation faster. Performance improvements depend on the surrounding library, processor instructions, vectorization, memory access patterns, and whether values must repeatedly be converted to and from other types.
