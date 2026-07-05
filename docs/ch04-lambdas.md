# Using lambdas in function implementations

F# is Microsoft’s strongly typed functional-first programming language that, like C#, compiles to **Intermediate Language (IL)** to be executed by .NET. Functional languages evolved from the lambda calculus, a computational system based only on functions. The code looks more like mathematical functions than steps in a recipe.

Some of the important attributes of functional languages are defined in the following list:
- **Modularity**: The same benefit of defining functions in C# applies to functional languages. This breaks up a large, complex code base into smaller pieces.
- **Immutability**: Variables in the C# sense do not exist. Any data value inside a function cannot change. Instead, a new data value can be created from an existing one. This reduces bugs.
- **Maintainability**: Functional code is cleaner and clearer (for mathematically inclined programmers).

C# has features to support a more functional approach, for example, tuples and pattern matching, non-null reference types, records aka immutable objects, and expression-bodied function members. 

In C#, lambdas are the use of the `=>` character to indicate a return value from a function. They work especially well for single-statement functions, as they can look much cleaner.

## Understanding the Fibonacci sequence

The Fibonacci sequence of numbers always starts with `0` and `1`. Then, the rest of the sequence is generated using the rule of adding together the previous two numbers, as shown in the following sequence of numbers:
```
0 1 1 2 3 5 8 13 21 34 55 ...
```

The next term in the sequence would be `34` + `55`, which is `89`.

## Generating the Fibonacci sequence function using imperative code

We will use the Fibonacci sequence to illustrate the difference between an imperative and a declarative function implementation:

1.	In `Program.Functions.cs`, write a function named `FibImperative`, which will be written in an imperative style:
```cs
static int FibImperative(uint term)
{
  if (term == 0)
  {
    throw new ArgumentOutOfRangeException();
  }
  else [MP1.1]if (term == 1)
  {
    return 0;
  }
  else if (term == 2)
  {
    return 1;
  }
  else
  {
    return FibImperative(term - 1) + FibImperative(term - 2);
  }
}
```

2.	In `Program.Functions.cs`, write a function named `RunFibImperative` that calls `FibImperative` inside a `for` statement that loops from `1` to `30`:
```cs
static void RunFibImperative()
{
  for (uint i = 1; i <= 30; i++)
  {
    WriteLine("The {0} term of the Fibonacci sequence is {1:N0}.",
      arg0: CardinalToOrdinal(i),
      arg1: FibImperative(term: i));
  }
}
```

3.	In `Program.cs`, comment out the other method calls and call the `RunFibImperative` method.
4.	Run the console app and view the results:
```text
The 1st term of the Fibonacci sequence is 0.
The 2nd term of the Fibonacci sequence is 1.
The 3rd term of the Fibonacci sequence is 1.
The 4th term of the Fibonacci sequence is 2.
The 5th term of the Fibonacci sequence is 3.
...
The 29th term of the Fibonacci sequence is 317,811.
The 30th term of the Fibonacci sequence is 514,229.
```

## Generating the Fibonacci sequence function using declarative code

Now let's see how we can improve the implementation using **functional programming (FP)** style:

1.	In `Program.Functions.cs`, write a function named `FibFunctional` written in a declarative style:
```cs
static int FibFunctional(uint term) => term switch
  {
    0 => throw new ArgumentOutOfRangeException(),
    1 => 0,
    2 => 1,
    _ => FibFunctional(term - 1) + FibFunctional(term - 2)
  };
```

2.	In `Program.Functions.cs`, write a function to call the `FibFunctional` function inside a `for` statement that loops from `1` to `30`:
```cs
static void RunFibFunctional()
{
  for (uint i = 1; i <= 30; i++)
  {
    WriteLine("The {0} term of the Fibonacci sequence is {1:N0}.",
      arg0: CardinalToOrdinal(i),
      arg1: FibFunctional(term: i));
  }
}
```

3.	In `Program.cs`, comment out the `RunFibImperative` method call and call the `RunFibFunctional` method.
4.	Run the code and view the results (which will be the same as before).
