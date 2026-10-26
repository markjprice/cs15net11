# Prompts for *Chapter 4 Writing, Debugging, and Testing Functions*

```
Compare a local function, a private static method in a `partial Program` class, and a method in a separate class. Explain when each approach is appropriate.
```

```
Review a tax-calculation function that accepts country codes and US state codes in the same string parameter. Identify design, validation, casing, culture, and maintainability problems, and propose a better design.
```

```
Trace the execution of `Factorial(5)` one recursive call at a time. Show the call stack growing, identify the base case, and then show the return values unwinding back through the stack.
```

```
Please explain checked and unchecked arithmetic in C#. Include constant expressions, runtime expressions, project-wide overflow settings, integer overflow, floating-point behavior, and examples where overflow silently produces a plausible but incorrect result.
```

```
Please explain the difference between Step Into, Step Over, and Step Out in a debugger. Give a concrete example involving a method that calls two other methods and describe when each command should be used.
```

```
Please explain conditional breakpoints, hit-count breakpoints, tracepoints, watches, the Immediate window, and the call stack. Give a debugging scenario where each feature would be more useful than adding `WriteLine` statements.
```

```
Please explain the limitations of .NET Hot Reload. Categorize common code changes into those that can usually be applied immediately and those that normally require restarting the application.
```

```
Please explain what makes a unit test deterministic, isolated, independent, repeatable, and fast. Show examples of tests that accidentally depend on execution order, the current culture, the clock, random numbers, files, or network access.
```

```
Design unit tests for `CardinalToOrdinal` using equivalence partitioning and boundary-value analysis. Include ordinary values, the 11th–13th special cases, values ending in 1–3, zero, and large unsigned integers.
```

```
Compare xUnit `[Fact]`, `[Theory]`, `[InlineData]`, `[MemberData]`, and `[ClassData]`. Show when test data belongs directly in an attribute and when it should be supplied by a separate member or class.
```

```
Please explain the difference between a usage error, a program error, and a system error. For each one, show whether the code should validate an argument, throw an exception, retry, log, return a result, or terminate.
```

```
Compare `throw;`, `throw ex;`, and throwing a new exception with an inner exception. Show how each changes the stack trace and explain which approach is normally best.
```
