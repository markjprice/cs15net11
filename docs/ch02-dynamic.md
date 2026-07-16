**Dynamic Types**

The `dynamic` type in C# offers flexibility by deferring type checking to runtime instead of compile-time. While it should be used cautiously due to its lack of compile-time safety, it has several practical applications where this flexibility is advantageous.

- [Storing dynamic types](#storing-dynamic-types)
- [Dynamic types with ExpandoObject](#dynamic-types-with-expandoobject)
- [Interoperability with COM Objects](#interoperability-with-com-objects)
- [Interoperability with Dynamic Languages](#interoperability-with-dynamic-languages)
- [Working with Reflection](#working-with-reflection)
- [Handling JSON or XML with Unknown Structures](#handling-json-or-xml-with-unknown-structures)
- [Scripting and DSLs](#scripting-and-dsls)
- [Prototyping and Rapid Development](#prototyping-and-rapid-development)
- [Runtime Polymorphism](#runtime-polymorphism)
- [Summary](#summary)
  - [Caveats and Recommendations](#caveats-and-recommendations)
  - [Best Practices](#best-practices)

# Storing dynamic types

There is a special type named `dynamic` that can store any type of data, but even more than `object`, its flexibility comes at the cost of performance. However, unlike `object`, the value stored in the variable can have its members invoked without an explicit cast. 

Let’s make use of a `dynamic` type:
1.	Add statements to declare a `dynamic` variable. Assign a string literal value, and then an integer value, and then an array of integer values. Finally, add a statement to output the length of the dynamic variable:
```cs
dynamic something;

// Storing an array of int values in a dynamic object.
// An array of any type has a Length property.
something = new[] { 3, 5, 7 };

// Storing an int in a dynamic object.
// int does not have a Length property.
something = 12;

// Storing a string in a dynamic object.
// string has a Length property.
something = "Ahmed";

// This compiles but might throw an exception at run-time.
Console.WriteLine($"The length of something is {something.Length}");

// Output the type of the something variable.
Console.WriteLine($"something is a {something.GetType()}");
```

> You will learn about arrays in *Chapter 3, Controlling Flow, Converting Types, and Handling Exceptions*.

2.	Run the code and note that it works because the last value assigned to something was a `string` value that does have a `Length` property:
```text
The length of something is 5
something is a System.String
```
3.	Comment out the statement that assigns a `string` value to the `something` variable by prefixing the statement with two slashes, `//`.
4.	Run the code and note the runtime error because the last value assigned to something is an `int` that does not have a `Length` property:
```text
Unhandled exception. Microsoft.CSharp.RuntimeBinder.RuntimeBinderException: 'int' does not contain a definition for 'Length'
```

5.	Comment out the statement that assigns an `int` to the `something` variable.
6.	Run the code and note the output because an array does have a `Length` property:
```text
The length of something is 3
something is a System.Int32[]
```

> One limitation of `dynamic` is that code editors cannot show IntelliSense to help you write the code. This is because the compiler cannot check what the type is during build time. Instead, the Common Language Runtime (CLR) checks for the member at runtime and throws an exception if it is missing. Exceptions are a way to indicate that something has gone wrong at runtime. You will learn more about them and how to handle them in *Chapter 3, Controlling Flow, Converting Types, and Handling Exceptions*.

# Dynamic types with ExpandoObject

`ExpandoObject` lives in the `System.Dynamic` namespace. It's a `dynamic` object that lets you add and remove properties at runtime, and internally it uses a dictionary to store the keys and values of these properties, but it’s accessible with dot notation just like with a regular class. You can also assign a delegate (such as `Action`) to simulate adding a method.

It's useful when you don't want or need a fixed class definition. If you are familiar with JavaScript, it works in a similar way. `ExpandoObject` is good for scripting, lightweight data containers, JSON serialization scenarios, and building dynamic APIs, for example, with ASP.NET Core Web API. But avoid it in performance-critical code or where type safety is important.

Let’s see a simple example of using `ExpandoObject`:

1.	In `Variables` project, in `Program.cs`, at the top of the file, add a statement to import the `System.Dynamic` namespace so that we can use the `ExpandoObject` class:
```cs
using System.Dynamic; // To use ExpandoObject.
```

2.	In `Program.cs`, add statements to create a `dynamic` object with three properties to define a person, and then output the person object by writing each individual property, and then by casting the object into a dictionary and enumerating it:
```cs
dynamic person = new ExpandoObject();

// Add properties.
person.FirstName = "John";
person.LastName = "Doe";
person.Age = 30;

Console.WriteLine($"{person.FirstName} {person.LastName} is {person.Age} years old.");

// Cast the ExpandoObject into a dictionary.
var dictionary = (IDictionary<string, object>)person;

// Each item in the dictionary is a key-value pair.
foreach (var item in dictionary)
{
  Console.WriteLine($"{item.Key} = {item.Value}");
}
```

3.	Run the `Variables` project and view the result, as partially shown in the following output:
```text
John Doe is 30 years old.
FirstName = John
LastName = Doe
Age = 30
```

Similarly, you can create custom types by inheriting from `DynamicObject` to intercept property and method calls dynamically.

, you can create custom types by inheriting from `DynamicObject` to intercept property and method calls dynamically.

> You will learn more about casting and dictionaries later in this book, so do not worry about them for now. You can learn more about ExpandoObject at the following link: https://learn.microsoft.com/en-us/dotnet/fundamentals/runtime-libraries/system-dynamic-expandoobject.

# Interoperability with COM Objects

The `dynamic` type is highly useful when interacting with COM-based APIs, such as Microsoft Office Interop: Automating Excel, Word, or Outlook, where the APIs often involve late binding.
```csharp
dynamic excelApp = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));
excelApp.Visible = true;
excelApp.Workbooks.Add();
```

Without dynamic, you would need to cast to specific interfaces, which can be cumbersome.

# Interoperability with Dynamic Languages

When working with libraries or frameworks written in dynamic languages like Python, Ruby, or JavaScript (via IronPython, IronRuby, or Node.js hosting), `dynamic` allows seamless integration:
```csharp
dynamic pyEngine = IronPython.Hosting.Python.CreateEngine();
pyEngine.Execute("print('Hello from Python')");
```

# Working with Reflection

Reflection often involves runtime type discovery and invocation of members. Using dynamic simplifies this process:
```csharp
var obj = Activator.CreateInstance(someType);
dynamic dynamicObj = obj;
dynamicObj.SomeMethod(); // No need for MethodInfo.Invoke
```

Without `dynamic`, you would need to use `MethodInfo` and manually invoke methods, making the code less readable.

# Handling JSON or XML with Unknown Structures

When dealing with data whose schema is not well-defined, such as deserialized JSON objects, `dynamic` is a convenient way to access properties:
```csharp
dynamic jsonObject = JsonConvert.DeserializeObject("{ \"Name\": \"Alice\", \"Age\": 25 }");
Console.WriteLine(jsonObject.Name); // Alice
Console.WriteLine(jsonObject.Age);  // 25
```

However, libraries like `System.Text.Json` or `Newtonsoft.Json` now encourage strongly-typed models for safety.

# Scripting and DSLs

`dynamic` is a great fit for scenarios involving embedded scripting or domain-specific languages (DSLs), where runtime behavior is determined by user scripts or configurations:
```csharp
dynamic scriptEngine = CreateScriptEngine();
scriptEngine.Execute("DoSomething()");
```

# Prototyping and Rapid Development

During prototyping, when object structures and APIs are fluid, dynamic lets you iterate quickly without defining rigid types. This can be helpful in proof-of-concept code but should be replaced with strong types in production.

# Runtime Polymorphism

In certain cases, you may need to invoke different behavior based on the runtime type of an object without using type checks or `switch` statements:
```csharp
void Execute(dynamic obj) {
    obj.Run(); // Call is resolved at runtime
}
```

This approach is risky but can simplify polymorphic operations in some dynamic scenarios.

# Summary 

## Caveats and Recommendations

- **Performance Cost**: `dynamic` incurs overhead because runtime resolution is slower than compile-time binding.
- **Lack of IntelliSense and Compile-Time Safety**: You lose the benefits of static typing, including IDE tooling and compile-time error checks.
- **Limited Debugging Support**: Errors might surface only during runtime, making debugging harder.

## Best Practices

- Prefer `dynamic` only when strong typing is impractical or excessively verbose.
- Use it for interoperability or scenarios where runtime flexibility is necessary.
- Avoid using it in core application logic; favor strong typing for maintainability.

By keeping these considerations in mind, `dynamic` can be a powerful tool for solving specific problems in C#.
