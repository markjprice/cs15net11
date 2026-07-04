# List pattern matching with arrays

Earlier in this chapter, you saw how an individual object supports pattern matching against its type and properties. Pattern matching also works with arrays and collections.

List pattern matching works with any type that has a public `Length` or `Count` property and has an indexer using an `int` or `System.Index` parameter. You will learn about indexers in *Chapter 5, Building Your Own Types with Object-Oriented Programming*.

> **Warning!** When you define multiple list patterns in the same `switch` expression, you must order them so that the more specific one comes first, or the compiler will complain because a more general pattern will match the more specific pattern too, and make the more specific one unreachable.

*Table 3.4* shows examples of list pattern matching, assuming a list of `int` values:

Example|Description
---|---
`[]`|Matches an empty array or collection.
`[..]`|Matches an array or collection with any number of items, including zero, so `[..]` must come after `[]` if you need to switch on both.
`[_]`|Matches a list with any single item.
`[int item1]` or
`[var item1]`|Matches a list with any single item and can use the value in the return expression by referring to `item1`.
`[7, 2]`|Matches exactly a list of two items with those values in that order.
`[_, _]`|Matches a list with any two items.
`[var item1, var item2]`|Matches a list with any two items and can use the values in the return expression by referring to `item1` and `item2`.
`[_, _, _]`|Matches a list with any three items.
`[var item1, ..]`|Matches a list with one or more items. Can refer to the value of the first item in its return expression by referring to `item1`.
`[var firstItem, .., var lastItem]`|Matches a list with two or more items. Can refer to the value of the first and last item in its return expression by referring to `firstItem` and `lastItem`.
`[.., var lastItem]`|Matches a list with one or more items. Can refer to the value of the last item in its return expression by referring to `lastItem`.

*Table 3.4: Examples of list pattern matching*

Let’s see some examples in code:
1.	At the bottom of `Program.cs`, add statements to define some arrays of `int` values, and then pass them to a method that returns descriptive text depending on the pattern that matches best:
```cs
int[] sequentialNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
int[] oneTwoNumbers = { 1, 2 };
int[] oneTwoTenNumbers = { 1, 2, 10 };
int[] oneTwoThreeTenNumbers = { 1, 2, 3, 10 };
int[] primeNumbers = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 };
int[] fibonacciNumbers = { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89 };
int[] emptyNumbers = { }; // Or use Array.Empty<int>()
int[] threeNumbers = { 9, 7, 5 };
int[] sixNumbers = { 9, 7, 5, 4, 2, 10 };

WriteLine($"{nameof(sequentialNumbers)}: {CheckSwitch(sequentialNumbers)}");
WriteLine($"{nameof(oneTwoNumbers)}: {CheckSwitch(oneTwoNumbers)}");
WriteLine($"{nameof(oneTwoTenNumbers)}: {CheckSwitch(oneTwoTenNumbers)}");
WriteLine($"{nameof(oneTwoThreeTenNumbers)}: {CheckSwitch(oneTwoThreeTenNumbers)}");
WriteLine($"{nameof(primeNumbers)}: {CheckSwitch(primeNumbers)}");
WriteLine($"{nameof(fibonacciNumbers)}: {CheckSwitch(fibonacciNumbers)}");
WriteLine($"{nameof(emptyNumbers)}: {CheckSwitch(emptyNumbers)}");
WriteLine($"{nameof(threeNumbers)}: {CheckSwitch(threeNumbers)}");
WriteLine($"{nameof(sixNumbers)}: {CheckSwitch(sixNumbers)}");

static string CheckSwitch(int[] values) => values switch
{
  [] => "Empty array",
  [1, 2, _, 10] => "Contains 1, 2, any single number, 10.",
  [1, 2, .., 10] => "Contains 1, 2, any range including empty, 10.",
  [1, 2] => "Contains 1 then 2.",
  [int item1, int item2, int item3] =>
    $"Contains {item1} then {item2} then {item3}.",
  [0, _] => "Starts with 0, then one other number.",
  [0, ..] => "Starts with 0, then any range of numbers.",
  [2, .. int[] others] => $"Starts with 2, then {others.Length} more numbers.",
  [..] => "Any items in any order.", // <-- Note the trailing comma for easier re-ordering.
  // Use Alt + Up or Down arrow to move statements.
};
```

> The `CheckSwitch` function above uses expression-bodied function member aka lambda syntax. This is the use of the `=>` character to indicate a return value from a function. I will properly introduce this in *Chapter 4, Writing, Debugging, and Testing Functions*.

2.	Run the code and note the result:
```
sequentialNumbers: Contains 1, 2, any range including empty, 10.
oneTwoNumbers: Contains 1 then 2.
oneTwoTenNumbers: Contains 1, 2, any range including empty, 10.
oneTwoThreeTenNumbers: Contains 1, 2, any single number, 10.
primeNumbers: Starts with 2, then 9 more numbers.
fibonacciNumbers: Starts with 0, then any range of numbers.
emptyNumbers: Empty array
threeNumbers: Contains 9 then 7 then 5.
sixNumbers: Any items in any order.
```

You can learn more about list pattern matching at the following link: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#list-patterns.
