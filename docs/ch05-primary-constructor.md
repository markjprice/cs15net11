# Defining a primary constructor for a class

You can define one constructor as part of the class definition. This is called the **primary constructor**.

Traditionally, we separate the class definition from any constructors:
```cs
public class Headset // Class definition.
{
  public Headset(string manufacturer, string productName) // Constructor.
  {
    // You can reference manufacturer and productName parameters in
    // the constructor but not in the rest of the class.
  }
}
```

With class primary constructors, you combine both into a more succinct syntax:
```cs
public class Headset(string manufacturer, string productName);
```

Let’s see an example:
1.	In the `PacktLibraryModern` project, add a class file named `Headset.cs`.
2.	Modify the code file contents to give the class two parameters for manufacturer and product name respectively:
```cs
namespace Packt.Shared;

public class Headset(string manufacturer, string productName);
```

3.	In `Program.cs`, add statements to instantiate a headset:
```cs
Headset vp = new("Apple", "Vision Pro");
WriteLine($"{vp.ProductName} is made by {vp.Manufacturer}.");
```

> **Warning!** One of the differences between a `record` and a `class` type with a primary constructor is that its parameters don’t become public properties automatically, so you will see `CS1061` compiler errors. `ProductName` and `productName` cannot be accessed outside the class.

4.	In `Headset.cs`, add statements to define two properties and set them using the parameters passed to the primary constructor:
```cs
namespace Packt.Shared;

public class Headset(string manufacturer, string productName)
{
 public string Manufacturer { get; set; } = manufacturer;
 public string ProductName { get; set; } = productName;
}
```

5.	Run the `PeopleApp` project and view the results:
```
Vision Pro is made by Apple.
```

6.	In `Headset.cs`, add a default parameterless constructor:
```cs
namespace Packt.Shared;

public class Headset(string manufacturer, string productName)
{
  public string Manufacturer { get; set; } = manufacturer;
  public string ProductName { get; set; } = productName;

  // Default parameterless constructor calls the primary constructor.
  public Headset() : this("Microsoft", "HoloLens") { }
}
```

> Note the use of `this()` to call the primary constructor and pass two parameters to it when the default constructor of `Headset` is called.

7.	In `Program.cs`, create an uninitialized instance of a headset and an instance for Meta Quest 3:
```cs
Headset holo = new();
WriteLine($"{holo.ProductName} is made by {holo.Manufacturer}.");

Headset mq = new() { Manufacturer = "Meta", ProductName = "Quest 3" };
WriteLine($"{mq.ProductName} is made by {mq.Manufacturer}.");
```
8.	Run the `PeopleApp` project and view the results:
```
Vision Pro is made by Apple.
HoloLens is made by Microsoft.
Quest 3 is made by Meta.
```

You can learn more about primary constructors for classes and structs at the following links: https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/primary-constructors and https://devblogs.microsoft.com/dotnet/csharp-primary-constructors-refactoring/. In particular, it is worth reading the comments at the bottom of the second link to understand why many developers do not like primary constructors in classes.

> **Good practice**: Only use a primary constructor in a class if it will only initialize non-read-only private fields, and does not need to execute other statements.
