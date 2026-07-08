# Good practice with collections

Let’s end this section about collections by reviewing some good practices that you should follow when working with collections.

- [Good practice with collections](#good-practice-with-collections)
  - [Pre-sizing collections](#pre-sizing-collections)
  - [Passing collections to methods](#passing-collections-to-methods)
  - [Returning collections from members](#returning-collections-from-members)


## Pre-sizing collections

Types such as `StringBuilder`, `List<T>`, `Queue<T>`, `Stack<T>`, `Dictionary<T>`, and `HashSet<T>` have a method named `EnsureCapacity` that can pre-size their internal storage array to the expected final size of the collection or `string` value.

This improves performance because it does not have to repeatedly increment the size of the array as more characters are appended:
```cs
List<string> names = new();
names.EnsureCapacity(10_000);
// Load ten thousand names into the list.
```

## Passing collections to methods

Let’s say you need to create a method to process a collection. For maximum flexibility, you could declare the input parameter to be `IEnumerable<T>` and make the method generic:
```cs
void ProcessCollection<T>(IEnumerable<T> collection)
{
  // Process the items in the collection,
  // perhaps using a foreach statement.
}
```

I could pass an array, a list, a queue, or a stack, containing any type, such as `int`, `string`, `Person`, or anything else that implements `IEnumerable<T>`, into this method, and it will process the items. However, the flexibility to pass any collection to this method comes at a performance cost.

One of the performance problems with `IEnumerable<T>` is also one of its benefits: deferred execution, also known as lazy loading. Types that implement this interface do not have to implement deferred execution, but many do.

But the worst performance problem with `IEnumerable<T>` is that the iteration must allocate an object on the heap. To avoid this memory allocation, you should define your method using a concrete type, as shown in the following code:
```cs
void ProcessCollection<T>(List<T> collection)
{
  // Process the items in the collection,
  // perhaps using a foreach statement.
}
```

This will use the `List<T>.Enumerator GetEnumerator()` method, which returns a struct value, instead of the `IEnumerator<T> GetEnumerator()` method, which returns a reference type. Your code will be two to three times faster and require less memory. As with all recommendations related to performance, you should confirm the benefit by running performance tests on your actual code in a production environment.

## Returning collections from members

Collections are reference types, which means they can be `null`. You might define methods or properties that return `null`:
```cs
public class Vehicle
{
  public List<Person>? GetPassengers()
  {
    ICollection<Person> passengers = GetFromDatabase();

    if (passengers.Count > 0)
    {
      return passengers;
    }

    return null;
  }

  public List<Person>? Passengers
  {
    get
    {
      ICollection<Person> passengers = GetFromDatabase();
      if (passengers.Count > 0)
      {
        return passengers;
      }

      return null;
    }
  }
}
```

This can cause issues if a developer calls your methods and properties that return a collection without checking for `null`:
```cs
var people = car.GetPassengers();

// Accessing people could throw a NullReferenceException!
WriteLine($"There are {people.Count} people.");
foreach (Person p in car.Passengers)
{
  // Process each person.
}
```

In your implementations of methods and properties that return collections, return an empty collection or array instead of `null`:
```cs
// Return an empty sequence instead.
return Enumerable.Empty<Person>();

// Or an empty array.
return Array.Empty<Person>();

// Or an empty collection expression.
return [];
```

*Table 8.16* shows a summary of your choices for returning collections from members:

Expression|Type Returned|Mutable|Allocates Memory|When to Use
---|---|---|---|---
`Enumerable.Empty<T>()`|`IEnumerable<T>`|No|No|To return an efficient, read-only empty sequence.
`new List<T>()` or `ToList()`|`List<T>`|Yes|Yes|To return a mutable, concrete collection.
`new T[] { ... }` or `[]`|`T[]` (array)|Yes*|Yes|Use when returning arrays, usually fixed-size or interop.
`ImmutableArray<T>.Empty`|`ImmutableArray<T>`|No|No|Use when you want a truly immutable structure.

*Table 8.16: Summary of return types for collections*

*An array is mutable in the sense that the items in it can be changed or replaced, but the size of an array cannot change so you cannot add or remove items.

> **Warning!** Be careful if returning mutable collections directly from internals because callers can modify them.
