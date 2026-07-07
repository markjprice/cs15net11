# Type aliases

Type aliases allow you to rename a type in the case of conflicts or to simplify code statements.

## Avoiding a namespace conflict with a using alias

It is possible that there are two namespaces that contain the same type name, and importing both namespaces causes ambiguity. For example, `JsonOptions` exists in multiple Microsoft-defined namespaces. If you use the wrong one to configure JSON serialization, then it will be ignored and you’ll be confused as to why!

Let’s review a made-up example:
```cs
// In the file, France.Paris.cs
namespace France
{
  public class Paris
  {
  }
}

// In the file, Texas.Paris.cs
namespace Texas
{
  public class Paris
  {
  }
}

// In the file, Program.cs
using France;
using Texas;

Paris p = new();
```

If we build this project, then the compiler would complain with the following error:
```
Error CS0104: 'Paris' is an ambiguous reference between 'France.Paris' and 'Texas.Paris'
```

We can define an alias for one of the namespaces to differentiate it:
```cs
using France; // To use Paris.
using Tx = Texas; // Tx becomes alias for the namespace, and it is not imported.

Paris p1 = new(); // Creates an instance of France.Paris.

Tx.Paris p2 = new(); // Creates an instance of Texas.Paris.
```

> **Prompt**: Two libraries define a type named `Timer`. Show three ways to resolve the naming conflict in C# and discuss which is clearest.

## Renaming a type with a using alias

Another situation where you might want to use an alias is if you would like to rename a type. For example, if you use the `Environment` class in the `System` namespace a lot, you could rename it with an alias to make it shorter:
```cs
using Env = System.Environment;

WriteLine(Env.OSVersion);
WriteLine(Env.MachineName);
WriteLine(Env.CurrentDirectory);
```

You can alias any type. This means you can rename existing types or give a type name to unnamed types like tuples.

> You can learn how to refactor your code using the “alias any type” feature at the following link: https://devblogs.microsoft.com/dotnet/refactor-your-code-using-alias-any-type/.
