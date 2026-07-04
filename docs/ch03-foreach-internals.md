# Understanding how foreach works internally

A developer who defines a type that represents multiple items, like an array or collection, should make sure that a programmer can use the `foreach` statement to enumerate through the type’s items.

Technically, the `foreach` statement will work on any type that follows these rules:
- The type must have a method named `GetEnumerator` that returns an `object`.
- The returned object must have a property named `Current` and a method named `MoveNext`.
- The `MoveNext` method must change the value of `Current` and return `true` if there are more items to enumerate through or return `false` if there are no more items.

There are interfaces named `IEnumerable` and `IEnumerable<T>` that formally define these rules, but technically, the compiler does not require the type to implement these interfaces.

Consider the following code:
```cs
List<string> names = [ "Adam", "Barry", "Charlie" ];

foreach (string name in names)
{
  WriteLine($"{name} has {name.Length} characters.");
}
```

The C# compiler turns the `foreach` statement in the preceding example into something like the following pseudocode:
```cs
List<string> names = [ "Adam", "Barry", "Charlie" ];

IEnumerator e = names.GetEnumerator();

while (e.MoveNext())
{
  string name = (string)e.Current;
  WriteLine($"{name} has {name.Length} characters.");
}
```

Due to the use of an iterator and its read-only `Current` property, the variable declared in a `foreach` statement cannot be used to modify the value of the current item. If you try to assign a value to it then an exception is thrown.

If the `names` variable were an array of `string` values instead of a collection of `string` values:
```cs
string[] names = [ "Adam", "Barry", "Charlie" ];
```

Then the C# compiler is smart enough to ignore that arrays implement the `IEnumerable<T>` interface and instead of using a `foreach` statement, it writes a `for` loop that uses the `Length` property of the array:
```cs
for (int item = 0; item < names.Length; item++)
{
  WriteLine($"{names[item]} has {names[item].Length} characters.");
}
```

This is because it is more efficient to use `for` than using the `IEnumerable<T>` interface.
