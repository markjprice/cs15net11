# Sorting collections

A `List<T>` class can be sorted by manually calling its `Sort` method (but remember that the indexes of each item will change). Manually sorting a list of `string` values or other built-in types will work without extra effort on your part, but if you create a collection of your own type, then that type must implement an interface named `IComparable`. You learned how to do this in an optional online section in *Chapter 6, Implementing Interfaces and Inheriting Classes*.

A `Stack<T>` or `Queue<T>` collection cannot be sorted because you wouldn’t usually want that functionality. For example, you would probably never sort a queue of guests checking into a hotel. But sometimes, you might want to sort a dictionary or a set.

Sometimes, it would be useful to have an automatically sorted collection, that is, one that maintains the items in a sorted order as you add and remove them.

There are multiple auto-sorting collections to choose from. The differences between these sorted collections are often subtle but can have an impact on the memory requirements and performance of your application, so it is worth putting effort into picking the most appropriate option for your requirements.

`OrderedDictionary<TKey, TValue>` provides `TryAdd` and `TryGetValue` for addition and retrieval. There are scenarios where you might want to perform additional operations:
```cs
public class OrderedDictionary<TKey, TValue>
{
  // New overloads with out index parameters.
  public bool TryAdd(TKey key, TValue value, out int index);
  public bool TryGetValue(TKey key, out TValue value, out int index);
}
```

The `index` parameter can then be used with `GetAt` or `SetAt` for fast access to the entry. An example usage of the `TryAdd` overload is to add or update a key/value pair in the ordered dictionary:
```cs
public static void IncrementValue(
  OrderedDictionary<string, int> orderedDictionary, string key)
{
  // Try to add a new key with value 1.
  if (!orderedDictionary.TryAdd(key, 1, out int index))
  {
    // Key was present, so increment the existing value instead.
    int value = orderedDictionary.GetAt(index).Value;
    orderedDictionary.SetAt(index, value + 1);
  }
}
```

This API is used in `JsonObject` to improve the performance of updating properties by 10–20%.

Some other common auto-sorting collections are shown in *Table 8.15*:
Collection|Description
---|---
`SortedDictionary<TKey, TValue>`|This represents a collection of key-value pairs that are sorted by key. Internally, it maintains a binary tree for items.
`SortedList<TKey, TValue>`|This represents a collection of key-value pairs that are sorted by key. The name is misleading because this is not a list. Compared to `SortedDictionary<TKey, TValue>`, retrieval performance is similar, it uses less memory, and insert and remove operations are slower for unsorted data. If it is populated from sorted data, then it is faster. Internally, it maintains a sorted array with a binary search to find elements.
`SortedSet<T>`|This represents a collection of unique objects that are maintained in a sorted order.

*Table 8.15: Common auto-sorting collections*
