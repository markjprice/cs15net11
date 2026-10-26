# Prompts for *Chapter 6 Implementing Interfaces and Inheriting Classes*

```
Please explain when operator overloading makes a C# API clearer and when it makes it misleading. Cover expected semantics, return types, side effects, exceptions, symmetry, commutativity, and discoverability. Then review the `Person` `+` and `*` operators from this chapter as an API designer. Identify which aspects are playful and useful for teaching but would be surprising in production code, then suggest clearer alternatives.
```

```
Compare `Hashtable` with `Dictionary<TKey, TValue>`. Explain compile-time type safety, boxing and unboxing, run-time casts, memory use, and when their performance differences matter.
```

```
Please explain implicit and explicit interface implementations. Show how explicit implementation can resolve member-name collisions and keep members out of a class’s normal public API.
```

```
Please explain default interface implementations, why they were added to C#, how they help evolve public APIs, and why they should not be treated as a replacement for abstract base classes.
```

```
Compare nullable value types and nullable reference types. Explain why `int?` becomes `Nullable<int>` but `string?` is still the same run-time type as `string`.
```

```
Please explain C# nullable flow analysis and the warnings CS8600, CS8602, CS8603, CS8604, and CS8618. For each warning, show a safe fix and an unsafe way to silence it.
```

```
Please explain the differences among `?.`, `??`, `??=`, `!`, `is null`, `is not null`, and `ArgumentNullException.ThrowIfNull`.
```

```
Please explain method hiding with `new` and method overriding with `virtual` and `override`. Show how the compile-time type of a variable and the run-time type of its object affect which implementation is called.
```

```
Compare an interface, an abstract class, a concrete base class, composition, and delegation. Give practical rules for choosing between them.
```

```
Compare `abstract`, `sealed`, and `closed` classes in C#. Explain direct and indirect inheritance, assembly boundaries, and exhaustive pattern matching. Then compare closed class hierarchies with union types. Explain when each is a better way to model a fixed set of states or outcomes.
```

```
Compare an explicit cast, the `as` operator, a declaration pattern such as `is Employee employee`, and a `switch` expression. Explain when a failed cast should return `null` and when it should throw an exception.
```

```
Please explain when to define a custom exception in .NET. Cover naming, base classes, standard constructors, inner exceptions, exception messages, and when an existing exception type is preferable.
```

```
Design both a mutable fluent API that returns this and an immutable fluent API that returns a new object. Compare their behavior, debugging experience, thread safety, and error handling.
```

```
Please explain how traditional extension methods and C# extension blocks are resolved. Cover namespaces, ambiguities, collisions, instance-member precedence, extension properties, static extension members, and extension operators.
```
