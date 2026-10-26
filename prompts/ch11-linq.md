# Prompts for *Chapter 11 Querying and Manipulating Data Using LINQ*

```
Please give me five progressively more difficult examples of
deferred LINQ execution. For each example, ask me to predict the output
before explaining the answer.
```

```
Please explain the difference between a delegate and an expression
tree using equivalent Where queries with LINQ to Objects and EF Core.
```

```
Please explain which LINQ methods do not have query-syntax
equivalents and show examples that combine query syntax with method
syntax cleanly.
```

```
Please explain how equality is determined by Distinct, Union,
Intersect, Except, Join, and GroupBy. Compare reference types, records,
anonymous types, and custom equality comparers.
```

```
Please explain the differences between IEnumerable<T>, IQueryable<T>,
and IAsyncEnumerable<T>. Show when each is appropriate and where the
query actually executes.
```

```
Please show me how to investigate an EF Core LINQ query with
ToQueryString, logging, and LINQPad. Explain what each tool reveals and
what it does not reveal.
```

```
Please show me how C# translates query comprehension syntax into
LINQ method calls, including where, orderby, select, join, group, let,
and into.
```

```
Compare GroupBy, GroupJoin, ToLookup, and ToDictionary. Explain
their result types, execution behavior, treatment of duplicate keys, and
best use cases.
```

```
Compare SQL inner, left, right, and full outer joins using
small input sequences. Include unmatched elements, duplicate keys, and
nullability in the results. Then please explain why joining non-unique
keys can multiply rows. Give examples of one-to-one, one-to-many,
many-to-many, and accidental Cartesian results.
```

```
Please explain why calling AsEnumerable() in the middle of an EF
Core query can change its performance and behavior. Show the SQL boundary
in several examples.
```

```
Design unit tests for a LINQ query that cover empty input, one item,
duplicate items, null values, unstable ordering, and unmatched join keys.
```

```
Please show me how repeated enumeration of a LINQ query can cause
repeated work, repeated database queries, or different results. Explain
when materializing with ToList is appropriate.
```
