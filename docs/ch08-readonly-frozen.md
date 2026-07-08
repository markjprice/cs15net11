# Read-only, immutable, and frozen collections

When we looked at the generic collection interface, we saw that it has a property named IsReadOnly. This is useful when we want to pass a collection to a method but not allow it to make changes.

For example, we might define a method:
```cs
void ReadCollection<T>(ICollection<T> collection)
{
  // We can check if the collection is read-only.
  if (!collection.IsReadOnly)
  {
    // Read the collection.
    return;
  }

  WriteLine("You have given me a collection that I could change!");
}
```

Generic collections, like List<T> and Dictionary<TKey, TValue>, have an AsReadOnly method to create a ReadOnlyCollection<T> instance that references the original collection. Although the ReadOnlyCollection<T> has to have an Add and Remove method because it implements ICollection<T>, it throws a NotSupportedException to prevent changes.
If the original collection has items added or removed, the ReadOnlyCollection<T> will see those changes. You can think of a ReadOnlyCollection<T> as a protected view of a collection.
Let’s see how we can make sure a collection is read-only:
1.	In the Collections project, in Program.Helpers.cs, add a method that should only be given a read-only dictionary with string for the type of key and value, but the naughty method tries to call Add:
private static void UseDictionary(
  IDictionary<string, string> dictionary)
{
  WriteLine($"Count before is {dictionary.Count}.");
  try
  {
    WriteLine("Adding new item with GUID values.");

    // Add method with return type of void.
    dictionary.Add(
      key: Guid.NewGuid().ToString(),
      value: Guid.NewGuid().ToString());
  }
  catch (NotSupportedException)
  {
    WriteLine("This dictionary does not support the Add method.");
  }
  WriteLine($"Count after is {dictionary.Count}.");
}
The type of parameter is IDictionary<TKey, TValue>. Using an interface provides more flexibility because we can pass either a Dictionary<TKey, TValue>, a ReadOnlyDictionary<TKey, TValue>, or anything else that implements that interface.
2.	In Program.cs, add statements to pass the keywords dictionary to this naughty method:
UseDictionary(keywords);
3.	Run the code, view the result, and note that the naughty method was able to add a new key-value pair, so the count has incremented:
Count before is 3.
Adding new item with GUID values.
Count after is 4.
4.	In Program.cs, comment out the UseDictionary statement, and then add a statement to pass the dictionary converted into a read-only collection:
//UseDictionary(keywords);
UseDictionary(keywords.AsReadOnly());
5.	Run the code, view the result, and note that this time, the method was not able to add an item, so the count is the same:
Count before is 3.
Adding new item with GUID values.
This dictionary does not support the Add method.
Count after is 3.
6.	At the top of Program.cs, import the System.Collections.Immutable namespace:
using System.Collections.Immutable; // To use ImmutableDictionary<T, T>.
7.	In Program.cs, comment out the AsReadOnly statement and then add a statement to pass the keywords converted into an immutable dictionary, as shown highlighted in the following code:
//UseDictionary(keywords.AsReadOnly());
UseDictionary(keywords.ToImmutableDictionary());
8.	Run the code, view the result, and note that this time, the method was also not able to add a default value, so the count is the same – it is the same behavior as using a read-only collection, so what’s the point of immutable collections?
If you import the System.Collections.Immutable namespace, then any collection that implements IEnumerable<T> is given six extension methods to convert it into an immutable collection, like a list, dictionary, set, and so on.
Adding to an immutable collection
Although the immutable collection will have a method named Add, it does not add an item to the original immutable collection! Instead, it returns a new immutable collection with the new item in it. The original immutable collection still only has the original items in it.
Let’s see an example:
1.	In Program.cs, add statements to convert the keywords dictionary into an immutable dictionary, and then add a new keyword definition to it by randomly generating GUID values:
ImmutableDictionary<string, string> immutableKeywords =
  keywords.ToImmutableDictionary();

// Call the Add method with a return value.
ImmutableDictionary<string, string> newDictionary =
  immutableKeywords.Add(
    key: Guid.NewGuid().ToString(),
    value: Guid.NewGuid().ToString());

OutputCollection("Immutable keywords dictionary", immutableKeywords);
OutputCollection("New keywords dictionary", newDictionary);
2.	Run the code, view the result, and note that the immutable keywords dictionary does not get modified when you call the Add method on it; instead, it returns a new dictionary with all the existing keywords plus the newly added keyword:
Immutable keywords dictionary:
  [float, Single precision floating point number]
  [long, 64-bit integer data type]
  [int, 32-bit integer data type]
New keywords dictionary:
  [d0e099ff-995f-4463-ae7f-7b59ed3c8d1d, 3f8e4c38-c7a3-4b20-acb3-01b2e3c86e8c]
  [float, Single precision floating point number]
  [long, 64-bit integer data type]
  [int, 32-bit integer data type]
Newly added items will not always appear at the top of the dictionary. Internally, the order is defined by the hash of the key. This is why dictionaries are sometimes called hash tables.
Good practice: To improve performance, many applications store a shared copy of commonly accessed objects in a central cache. To safely allow multiple threads to work with those objects knowing they won’t change, you should make them immutable or use a concurrent collection type, which you can read about at the following link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.
The generic collections have some potential performance issues related to how they are designed.
First, being generic, the types of items or types used for keys and values for a dictionary have a big effect on performance, depending on what they are. Since they could be any type, the .NET team cannot optimize the algorithm. string and int types are the most used in real life. If the .NET team could rely on those always being the types used, then they could greatly improve performance.
Second, collections are dynamic, meaning that new items can be added, and existing items can be removed at any time. Even more optimizations could be made if the .NET team knew that no more changes would be made to the collection.
.NET also has the concept of frozen collections. Hmmm, we already have immutable collections, so what is different about frozen collections? Are they delicious like ice cream? The idea is that 95% of the time, a collection is populated and then never changed. So, if we could optimize them at the time of creation, then those optimizations could be made, adding some time and effort upfront, but then after that, performance for reading the collection could be greatly improved.
There are two frozen collections: FrozenDictionary<TKey, TValue> and FrozenSet<T>. More may come in future versions of .NET, but these are the two most common scenarios that would benefit from the frozen concept.[MP1.1]
Exploring frozen collections
Let’s go:
1.	At the top of Program.cs, import the System.Collections.Frozen namespace:
using System.Collections.Frozen; // To use FrozenDictionary<T, T>.
2.	At the bottom of Program.cs, add statements to convert the keywords dictionary into a frozen dictionary, output its items, and then look up the definition of long:
// Creating a frozen collection has an overhead to perform the
// sometimes complex optimizations.
FrozenDictionary<string, string> frozenKeywords =
  keywords.ToFrozenDictionary();

OutputCollection("Frozen keywords dictionary", frozenKeywords);

// Lookups are faster in a frozen dictionary.
WriteLine($"Define long: {frozenKeywords["long"]}");
3.	Run the code and view the result:
Frozen keywords dictionary:
  [int, 32-bit integer data type]
  [long, 64-bit integer data type]
  [float, Single precision floating point number]
Define long: 64-bit integer data type
What the Add method does depends on the type:
•	List<T>: This adds a new item to the end of the existing list.
•	Dictionary<TKey, TValue>: This adds a new item to the existing dictionary in a position determined by its internal structure.
•	ReadOnlyCollection<T>: This throws a not-supported exception.
•	ImmutableList<T>: This returns a new list with the new item in it. This does not affect the original list.
•	ImmutableDictionary<TKey, TValue>: This returns a new dictionary with the new item in it. This does not affect the original dictionary.
•	FrozenDictionary<TKey, TValue>: This does not exist.
The documentation for frozen collections can be found at the following link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.frozen.
