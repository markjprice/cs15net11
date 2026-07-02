- [Revealing the extent of the C# vocabulary](#revealing-the-extent-of-the-c-vocabulary)
- [Example of asking ChatGPT to explain code](#example-of-asking-chatgpt-to-explain-code)


# Revealing the extent of the C# vocabulary

We know that there are more than 100 keywords in C#, but how many types are there? Let’s write some code to find out how many types (and their methods) are available to C# in our simple console app.

Don’t worry about exactly how this code works for now, but know that it uses a technique called reflection:
1.  Use your preferred code editor to add a new **Console App** / `console` project named `Vocabulary` to the `Chapter02` solution.
2.	Delete all the existing statements in `Program.cs`.
3.	Import the `System.Reflection` namespace at the top of `Program.cs` so that we can use some of the types in that namespace, such as `Assembly` and `TypeName`, as shown in the following code:
```cs
using System.Reflection; // To use Assembly, TypeName, and so on.
```

> **Good practice**: We could use the implicit imports and global using features to import this namespace for all `.cs` files in this project, but since there is only one file, it is better to import the namespace in the one file in which it is needed.

4.	Write statements to get the compiled console app and loop through all the types that it has access to, outputting the names and number of methods each has, as shown in the following code:
```cs
// Get the assembly that is the entry point for this app.
Assembly? myApp = Assembly.GetEntryAssembly();

// If the previous line returned nothing then end the app.
if (myApp is null) return;

// Loop through the assemblies that my app references.
foreach (AssemblyName name in myApp.GetReferencedAssemblies())
{
  // Load the assembly so we can read its details.
  Assembly a = Assembly.Load(name);

  // Declare a variable to count the number of methods.
  int methodCount = 0;

  // Loop through all the types in the assembly.
  foreach (TypeInfo t in a.DefinedTypes)
  {
    // Add up the counts of all the methods.
    methodCount += t.GetMethods().Length;
  }

  // Output the count of types and their methods.
  WriteLine("{0:N0} types with {1:N0} methods in {2} assembly.",
    arg0: a.DefinedTypes.Count(),
    arg1: methodCount,
    arg2: name.Name);
}
```

> `N0` is uppercase `N` followed by the digit zero. It is not uppercase `N` followed by uppercase `O`. `N0` means “format a number (`N`) with zero (`0`) decimal places.”

5.	Run the project. You will see the actual number of types and methods that are available to you in the simplest application when running on your operating system (OS). The number of types and methods displayed will be different depending on the OS that you are using, as shown in the following output on Windows:
```text
0 types with 0 methods in System.Runtime assembly.
41 types with 639 methods in System.Console assembly.
112 types with 1,190 methods in System.Linq assembly.
```

Why does the `System.Runtime` assembly contain zero types? This assembly is special because it contains only type-forwarders rather than actual types. A type-forwarder represents a type that has been implemented outside of .NET or for some other advanced reason.

6.	Add statements to the top of the file (after importing the `System.Reflection` namespace) to declare some variables, as shown in the following code:
```cs
// Declare some unused variables using types in
// additional assemblies to make them load too.
System.Data.DataSet ds = new();
HttpClient client = new();
```

7.	Run the console app again and view the results, which should look like the following output:
```text
0 types with 0 methods in System.Runtime assembly.
406 types with 7,139 methods in System.Data.Common assembly.
425 types with 4,681 methods in System.Net.Http assembly.
41 types with 639 methods in System.Console assembly.
112 types with 1,190 methods in System.Linq assembly.
```

Now, you have a better sense of why learning C# is a challenge – because there are so many types and methods to learn. Methods are only one category of a member that a type can have, and you and other programmers are constantly defining new types and members!

# Example of asking ChatGPT to explain code

The code we wrote in the `Vocabulary` project is a good example of when you might want to ask an AI chatbot to explain something. If you enter a prompt of `Explain the following code:` followed by the code, here’s what it responds with:
```text
This C# code performs an analysis of the assemblies (essentially libraries or modules) referenced by the current application. It tallies up the total number of types and methods in each referenced assembly.

Let’s break down the code in more detail:

1.	System.Reflection namespace is used to work with assemblies, modules, types, methods, and so on, at runtime.
2.	The System.Data.DataSet and HttpClient instances are declared but not used. The comment implies that these are declared to ensure their respective assemblies are loaded for the subsequent analysis. Creating instances of classes from certain assemblies can cause those assemblies to be loaded into memory.
3.	Assembly.GetEntryAssembly() gets the assembly that is the entry point of the application.
4.	If myApp is null (meaning there’s no entry assembly, which should be impossible in a normally running .NET app), the application exits.
5.	The foreach loop iterates over each assembly name in the referenced assemblies of the entry point assembly.
6.	Assembly.Load(name) loads an assembly given the assembly name. This allows you to interact with the assembly programmatically.
7.	A counter variable, methodCount, is declared and initialized to 0. This is used to count the total number of methods in the assembly.
8.	Another nested foreach loop then iterates over each type in the assembly.
9.	For each type, it gets the number of methods (t.GetMethods().Length) and adds it to the methodCount.
10.	Finally, the number of types and their methods in the assembly are output to the console using WriteLine(). The {0:N0} format specifier in the string is a placeholder for a number (0 refers to the argument index, N is for number format, and 0 after N specifies the number of decimal places).

So, in essence, this script counts how many types and methods exist in each assembly referenced by the application. This might be useful to understand the complexity or size of the dependencies the application has.
```

And now back to the flesh and blood, Mark. That’s a pretty good explanation with no mistakes or hallucinations!

> **Good practice**: Tools such as GitHub Copilot and ChatGPT are especially useful for programmers when they are learning new things. And a good programmer is always learning new things! These tools are not just for writing code for you. They can explain existing code written by others and even suggest improvements.
