# Rounding numbers and the default rounding rules

You have now seen that the cast operator trims the decimal part of a real number and that the `System.Convert` method rounds up or down. However, what is the rule for rounding?

In British primary schools for children aged 5 to 11, pupils are taught to round up if the decimal part is `.5` or higher and round down if the decimal part is less. Of course, these terms only make sense because, at that age, the pupils are only dealing with positive numbers. With negative numbers, these terms become confusing and should be avoided. This is why the .NET API uses the enum values `AwayFromZero`, `ToZero`, `ToEven`, `ToPositiveInfinity`, and `ToNegativeInfinity` for improved clarity.

Let’s explore if C# follows the same primary school rule:
1.	Type statements to declare and assign an array of `double` values, convert each of them into an integer, and then write the result to the console:
```cs
double[,] doubles = {
  { 9.49, 9.5, 9.51 },
  { 10.49, 10.5, 10.51 },
  { 11.49, 11.5, 11.51 },
  { 12.49, 12.5, 12.51 } ,
  { -12.49, -12.5, -12.51 },
  { -11.49, -11.5, -11.51 },
  { -10.49, -10.5, -10.51 },
  { -9.49, -9.5, -9.51 }
};

WriteLine($"| double | ToInt32 | double | ToInt32 | double | ToInt32 |");

for (int x = 0; x < 8; x++)
{
  for (int y = 0; y < 3; y++)
  {
    Write($"| {doubles[x, y],6} | {ToInt32(doubles[x, y]),7} ");
  }
  WriteLine("|");
}
WriteLine();
```

2.	Run the code and view the result:
```
| double | ToInt32 | double | ToInt32 | double | ToInt32 |
|   9.49 |       9 |    9.5 |      10 |   9.51 |      10 |
|  10.49 |      10 |   10.5 |      10 |  10.51 |      11 |
|  11.49 |      11 |   11.5 |      12 |  11.51 |      12 |
|  12.49 |      12 |   12.5 |      12 |  12.51 |      13 |
| -12.49 |     -12 |  -12.5 |     -12 | -12.51 |     -13 |
| -11.49 |     -11 |  -11.5 |     -12 | -11.51 |     -12 |
| -10.49 |     -10 |  -10.5 |     -10 | -10.51 |     -11 |
|  -9.49 |      -9 |   -9.5 |     -10 |  -9.51 |     -10 |
```

We have shown that the rule for rounding in C# is subtly different from the primary school rule:
- It always rounds toward zero if the decimal part is less than the midpoint `.5`.
- It always rounds away from zero if the decimal part is more than the midpoint `.5`.
- It will round away from zero if the decimal part is the midpoint `.5` and the non-decimal part is odd, but it will round toward zero if the non-decimal part is even.

This rule is known as **Banker’s rounding**, and it is preferred because it reduces bias by alternating when it rounds toward or away from zero. Sadly, other languages such as JavaScript use the primary school rule.

## Taking control of rounding rules

You can take control of the rounding rules by using the `Round` method of the `Math` class:

1.	Type statements to round each of the `double` values using the “away from zero” rounding rule, also known as rounding “up,” and then write the result to the console:
```cs
foreach (double n in doubles)
{
  WriteLine(format:
    "Math.Round({0}, 0, MidpointRounding.AwayFromZero) is {1}",
    arg0: n,
    arg1: Math.Round(value: n, digits: 0,
            mode: MidpointRounding.AwayFromZero));
}
```

> You can use a `foreach` statement to enumerate all the items in a multi-dimensional array.

2.	Run the code and view the result:
```
Math.Round(9.49, 0, MidpointRounding.AwayFromZero) is 9
Math.Round(9.5, 0, MidpointRounding.AwayFromZero) is 10
Math.Round(9.51, 0, MidpointRounding.AwayFromZero) is 10
Math.Round(10.49, 0, MidpointRounding.AwayFromZero) is 10
Math.Round(10.5, 0, MidpointRounding.AwayFromZero) is 11
Math.Round(10.51, 0, MidpointRounding.AwayFromZero) is 11
...
```

> **Good practice**: For every programming language that you use, check its rounding rules. They may not work the way you expect! You can read more about Math.Round at the following link: https://learn.microsoft.com/en-us/dotnet/api/system.math.round.
