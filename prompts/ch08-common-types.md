# Prompts for *Chapter 8 Working with Common .NET Types*

```
Compare Half, BFloat16, float, double, and decimal. Explain
their storage sizes, approximate precision, numeric ranges, performance
characteristics, and the scenarios for which each is best suited.
```

```
Please explain why multiplying two int values in C# can overflow
even when the result is assigned to a long. Compare checked, casting
before multiplication, Math.BigMul, int.BigMul, Int128, and BigInteger,
with runnable examples.
```

```
Please explain deterministic pseudo-random number generation. Show
why two Random instances created with the same seed can produce repeatable
sequences, and give examples where repeatability is useful in testing,
simulations, and procedural game generation. Compare System.Random with
System.Security.Cryptography.RandomNumberGenerator. Demonstrate how
biased random selection can occur when bytes are reduced using the
remainder operator, and show a safe alternative.
```

```
Compare UUID versions 4 and 7. Explain their bit layouts, privacy
implications, collision resistance, natural ordering, and likely effects
when they are used as primary keys in a relational database.
```

```
Please show me how the same strings can sort differently using
Ordinal, OrdinalIgnoreCase, CurrentCulture, and InvariantCulture. Include
examples involving Turkish I, German ß, accented letters, and filenames
containing numbers.
```

```
Compare string concatenation, interpolation, string.Concat, string.
Join, StringBuilder, and interpolated string handlers. Explain when each
approach allocates memory and when StringBuilder produces a meaningful
benefit.
```

```
Please explain the differences between UTF-16 code units, Unicode
scalar values, Rune values, combining characters, and grapheme clusters.
Use examples containing emoji, accented characters, flags, and family
emoji.
```

```
Please explain how SearchValues<T> can optimize repeated searches
for characters, bytes, or strings. Show an example that creates the search
values once and reuses them while processing many inputs.
```

```
Please explain why regular expressions are suitable for recognizing
some text patterns but poor substitutes for parsers. Compare regex-based
processing with dedicated parsers for CSV, JSON, XML, HTML, URLs, and
email addresses.
```

```
Compare read-only wrappers, immutable collections, and frozen
collections. Explain whether each protects against later changes, how
updates are represented, and which is optimized for lookup-heavy workloads.
```

```
Please explain how a .NET dictionary uses hash codes, buckets, and
equality comparers. Show how a poor GetHashCode implementation affects
performance and why equal objects must produce equal hash codes.
```

```
Please explain why PriorityQueue<TElement,TPriority> is not stable
for equal priorities. Show how to add a sequence number as a secondary
priority to preserve FIFO behavior among equal-priority items.
```

```
Show how the compiler can lower C# collection expressions containing
ordinary elements, `with(...)`, and spread elements. Explain when it calls a constructor, `Add`, an indexer, `AddRange`, or a collection builder.
```

```
Create a decision table that compares arrays, List<T>, LinkedList<T>,
Dictionary<TKey,TValue>, HashSet<T>, Queue<T>, Stack<T>, and PriorityQ
ueue<TElement,TPriority>. Base the recommendation on lookup, insertion,
removal, ordering, uniqueness, and memory use.
```

```
Please explain the difference between a process, a thread, and a
Task in .NET. Use beginner-friendly analogies, then show how each concept
appears in a simple C# console app.
```
