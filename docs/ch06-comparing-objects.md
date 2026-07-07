# Comparing objects when sorting

One of the most common interfaces that you will want to implement in your types that represent data is `IComparable`. If a type implements one of the `IComparable` interfaces, then arrays and collections containing instances of that type can be sorted.

- [Comparing objects when sorting](#comparing-objects-when-sorting)
  - [Comparing objects to each other](#comparing-objects-to-each-other)
  - [Common implementations of CompareTo](#common-implementations-of-compareto)
  - [Comparing objects using a separate class](#comparing-objects-using-a-separate-class)

## Comparing objects to each other

This is an example of an abstraction for the concept of sorting. To sort any type, the minimum functionality would be the ability to compare two items and decide which goes before the other. If a type implements that minimum functionality, then a sorting algorithm can use it to sort instances of that type in any way the sorting algorithm wants.

The `IComparable` interface has one method named `CompareTo`. This has two variations, one that works with a nullable object type and one that works with a nullable generic type `T`:
```cs
namespace System
{
  public interface IComparable
  {
    int CompareTo(object? obj);
  }

  public interface IComparable<in T>
  {
    int CompareTo(T? other);
  }
}
```

The `in` keyword specifies that the type parameter `T` is contravariant, which means that you can use a less derived type than that specified. For example, if `Employee` derives from `Person`, then both can be compared to each other.

> **Prompt**: Explain generic covariance and contravariance using `IEnumerable<out T>`, `Action<in T>`, `Person`, and `Employee`. Include examples that compile and examples that do not.

## Common implementations of CompareTo

For example, the `string` type implements `IComparable` by returning:

- `-1` if the string should appear *before* the string it is compared to
- `1` if it should appear *after*  the string it is compared to
- `0` if they are equal

The `int` type implements `IComparable` by returning `-1` if the `int` is less than the `int` being compared to, `1` if it is greater, and `0` if they are equal.

`CompareTo` return values can be summarized as shown in Table 6.2:

`this` before `other`|`this` is equal to `other`|`this` after `other`
---|---|---
`-1`|`0`|`1`
*Table 6.2: Summary of the `CompareTo` return values*

Before we implement the `IComparable` interface and its `CompareTo` method for the `Person` class, let’s see what happens when we try to sort an array of `Person` instances without implementing this interface, including some that are `null` or have a `null` value for their `Name` property:
1.	In the `PeopleApp` project, add a new class file named `Program.Helpers.cs`.
2.	In `Program.Helpers.cs`, delete any existing statements. Then define a method for the `partial Program` class that will output all the names of a collection of people passed as a parameter, with a title beforehand:
```cs
using Packt.Shared;

partial class Program
{
  private static void OutputPeopleNames(
    IEnumerable<Person?> people, string title)
  {
    WriteLine(title);

    foreach (Person? p in people)
    {
      WriteLine(" {0}",
        p is null ? "<null> Person" : p.Name ?? "<null> Name");
      /* if p is null then output: <null> Person
         else output: p.Name
         unless p.Name is null then output: <null> Name */
    }
  }
}
```

3.	In `Program.cs`, add statements that create an array of `Person` instances, call the `OutputPeopleNames` method to write the items to the console, and then attempt to sort the array and write the items to the console again:
```cs
Person?[] people =
{
  null,
  new() { Name = "Simon" },
  new() { Name = "Jenny" },
  new() { Name = "Adam" },
  new() { Name = null },
  new() { Name = "Richard" }
};

OutputPeopleNames(people, "Initial list of people:");

Array.Sort(people);

OutputPeopleNames(people,
  "After sorting using Person's IComparable implementation:");
```

4.	Run the `PeopleApp` project and an exception will be thrown. As the message explains, to fix the problem, our type must implement `IComparable`:
```
Unhandled Exception: System.InvalidOperationException: Failed to compare two elements in the array. ---> System.ArgumentException: At least one object must implement IComparable.
```

5.	In `Person.cs`, after `Person`, add a comma and enter `IComparable<Person?>`:
```cs
public class Person : IComparable<Person?>
```

> Your code editor will draw a red squiggle under the new code to warn you that you have not yet implemented the method you promised to. Your code editor can write the skeleton implementation for you.

6.	Click on the light bulb and then click **Implement interface**.
7.	Scroll down to the bottom of the `Person` class to find the method that was written for you:
```cs
public int CompareTo(Person? other)
{
  throw new NotImplementedException();
}
```

8.	Delete the statement that throws the `NotImplementedException` error.
9.	Add statements to handle variations of input values, including `null`. Call the `CompareTo` method of the `Name` field, which uses the `string` type’s implementation of `CompareTo`. Return the result:
```cs
int position;

if (other is not null)
{
  if ((Name is not null) && (other.Name is not null))
  {
    // If both Name values are not null, then
    // use the string implementation of CompareTo.
    position = Name.CompareTo(other.Name);
  }
  else [MP1.1]if ((Name is not null) && (other.Name is null))
  {
    position = -1; // this Person precedes other Person.
  }
  else if ((Name is null) && (other.Name is not null))
  {
    position = 1; // this Person follows other Person.
  }
  else // Name and other.Name are both null.
  {
    position = 0; // this and other are at same position.
  }
}
else if (other is null)
{
  position = -1; // this Person precedes other Person.
}
else // this and other are both null.
{
  position = 0; // this and other are at same position.
}
return position;
```

Note the following:
- We have chosen to compare two `Person` instances by comparing their `Name` fields. `Person` instances will, therefore, be sorted alphabetically by their name. 
- `null` values will be sorted to the bottom of the collection. 
- Storing the calculated position before returning it is useful when debugging. 
- I’ve used more round brackets than the compiler needs to make the code easier to read. If you prefer fewer brackets, then feel free to remove them.
- The final `else` statement will never execute because the logic of the `if` and `else if` clauses means it will only execute when `this` (the current object instance) is `null`. In that scenario, the method could not execute anyway since the object wouldn’t exist! I wrote the `if` statement to exhaustively cover all combinations of `null` and `not null` for `other` and `this`, but the last of those combinations could, in practice, never happen.

10.	Run the `PeopleApp` project. Note that this time it works as it should, sorted alphabetically by name:
```
Initial list of people:
  Simon
  <null> Person
  Jenny
  Adam
  <null> Name
  Richard
After sorting using Person's IComparable implementation:
  Adam
  Jenny
  Richard
  Simon
  <null> Name
  <null> Person
```

> **Good practice**: If you want to sort an array or collection of instances of your type, then implement the `IComparable` interface.

## Comparing objects using a separate class

Sometimes, you won’t have access to the source code for a type, and it might not implement the `IComparable` interface. Luckily, there is another way to sort instances of a type. You can create a separate type that implements a slightly different interface, named `IComparer`:

1.	In the `PacktLibrary` project, add a new class file named `PersonComparer.cs`, containing a class implementing the `IComparer` interface that will compare two people, that is, two `Person` instances. We will implement it by comparing the length of their `Name` fields, or if the names are the same length, then compare the names alphabetically, as shown in the following code:
```cs
namespace Packt.Shared;

public class PersonComparer : IComparer<Person?>
{
  public int Compare(Person? x, Person? y)
  {
    int position;

    if ((x is not null) && (y is not null))
    {
      if ((x.Name is not null) && (y.Name is not null))
      {
        // If both Name values are not null...
        // ...then compare the Name lengths...
        int result = x.Name.Length.CompareTo(y.Name.Length);
        // ...and if they are equal...
        if (result == 0)
        {
          // ...then compare by the Names...
          return x.Name.CompareTo(y.Name);
        }
        else
        {
          // ...otherwise compare by the lengths.
          position = result;
        }
      }
      else if ((x.Name is not null) && (y.Name is null))
      {
        position = -1; // x Person precedes y Person.
      }
      else if ((x.Name is null) && (y.Name is not null))
      {
        position = 1; // x Person follows y Person.
      }
      else // x.Name and y.Name are both null.
      {
        position = 0; // x and y are at same position.
      }
    }
    else if ((x is not null) && (y is null))
    {
      position = -1; // x Person precedes y Person.
    }
    else if ((x is null) && (y is not null))
    {
      position = 1; // x Person follows y Person.
    }
    else // x and y are both null.
    {
      position = 0; // x and y are at same position.
    }
    return position;
  }
}
```

2.	In `Program.cs`, add statements to sort the array using an alternative implementation, as shown in the following code:
```cs
Array.Sort(people, new PersonComparer());

OutputPeopleNames(people,
  "After sorting using PersonComparer's IComparer implementation:");
```

3.	Run the `PeopleApp` project, and view the result of sorting the people by the length of their names and then alphabetically, as shown in the following output:
```
After sorting using PersonComparer's IComparer implementation:
  Adam
  Jenny
  Simon
  Richard
  <null> Name
  <null> Person
```

This time, when we sort the people array, we explicitly ask the sorting algorithm to use the `PersonComparer` type instead so that the people are sorted with the shortest names first, like Adam, and the longest names last, like Richard. When the lengths of two or more names are equal, they are sorted alphabetically, like Jenny and Simon.
