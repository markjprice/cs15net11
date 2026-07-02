# Getting and setting the default values for types

Most of the primitive types except `string` are value types, which means that they must have a value. You can determine the default value of a type by using the `default()` operator and passing the type as a parameter. You can assign the default value of a type by using the `default` keyword.

The `string` type is a reference type. This means that `string` variables contain the memory address of a value, not the value itself. A reference type variable can have a `null` value, which is a literal that indicates that the variable does not reference anything (yet). `null` is the default for all reference types.

You’ll learn more about value types and reference types in *Chapter 6, Implementing Interfaces and Inheriting Classes*.

Let’s explore default values:
1.	Add statements to show the default values of an int, a bool, a DateTime, and a string:
```cs
Console.WriteLine($"default(int) = {default(int)}");
Console.WriteLine($"default(bool) = {default(bool)}");
Console.WriteLine($"default(DateTime) = {
 default(DateTime)}");
Console.WriteLine($"default(string) = {
 default(string) ?? "<NULL>"}");
```

> The `??` operator means, if `null`, then return the following instead. So if `default(string)` is `null`, then the text, `<NULL>` will be returned.

2.	Run the code and view the result. Note that your output for the date and time might be formatted differently if you are not running it in the UK because date and time values are formatted using the current culture of your computer:
```text
default(int) = 0
default(bool) = False
default(DateTime) = 01/01/0001 00:00:00
default(string) = <NULL>
```

3.	Add statements to declare a number, assign a value, and then reset it to its default value:
```cs
int number = 13;
Console.WriteLine($"number set to: {number}");
number = default;
Console.WriteLine($"number reset to its default: {number}");
```
4.	Run the code and view the result:
```text
number set to: 13
number reset to its default: 0
```
