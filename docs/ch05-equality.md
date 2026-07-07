# Equality of other types

It is common to compare two variables using the `==` and `!=` operators. The behavior of these two operators is different for reference types and value types.

## Equality of value types

When you check the equality of two value type variables, .NET literally compares the values of those two variables on the stack and returns `true` if they are equal:
1.	In `Program.cs`, add statements to declare two integers with equal values and then compare them:
```cs
int number1 = 3;
int number2 = 3;

WriteLine($"number1: {number1}, number2: {number2}");
WriteLine($"number1 == number2: {number1 == number2}");
```

2.	Run the `PeopleApp` project and view the result:
```
number1: 3, number2: 3
number1 == number2: True
```

## Equality of reference types

When you check the equality of two reference type variables, .NET compares the memory addresses of those two variables and returns `true` if they are equal.

1.	In `Program.cs`, add statements to declare two `Person` instances with equal names, and then compare the variables and their names:
```cs
Person p1 = new() { Name = "Kevin" };
Person p2 = new() { Name = "Kevin" };

WriteLine($"p1: {p1}, p2: {p2}");
WriteLine($"p1.Name: {p1.Name}, p2.Name: {p2.Name}");
WriteLine($"p1 == p2: {p1 == p2}");
```

2.	Run the `PeopleApp` project and view the result:
```
p1: Packt.Shared.Person, p2: Packt.Shared.Person
p1.Name: Kevin, p2.Name: Kevin
p1 == p2: False
```
This is because they are not the same object. If both variables literally pointed to the same object on the heap, then they would be equal.

3.	Add statements to declare a third `Person` object and assign `p1` to it:
```cs
Person p3 = p1;

WriteLine($"p3: {p3}");
WriteLine($"p3.Name: {p3.Name}");
WriteLine($"p1 == p3: {p1 == p3}");
```

4.	Run the `PeopleApp` project and view the result:
```
p3: Packt.Shared.Person
p3.Name: Kevin
p1 == p3: True
```

The one exception to this behavior of reference types is the string type. It is a reference type, but the equality operators have been overridden to make them behave as if they were value types.

5.	Add statements to compare the `Name` properties of two `Person` instances:
```cs
// string is the only class reference type implemented to
// act like a value type for equality.
WriteLine($"p1.Name: {p1.Name}, p2.Name: {p2.Name}");
WriteLine($"p1.Name == p2.Name: {p1.Name == p2.Name}");
```

8.	Run the `PeopleApp` project and view the result:
```
p1.Name: Kevin, p2.Name: Kevin
p1.Name == p2.Name: True
```

You can do the same as `string` with your classes to override the equality operator `==` to return `true`, even if the two variables are not referencing the same object (the same memory address on the heap) but, instead, their fields have the same values. However, that is beyond the scope of this book.

> **Good practice**: Alternatively, use a `record` class because one of its benefits is that it implements this equality behavior for you.
