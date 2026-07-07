# Pattern matching with objects

In *Chapter 3, Controlling Flow, Converting Types, and Handling Exceptions*, you were introduced to basic pattern matching. In this section, we will explore pattern matching in more detail.

## Pattern matching flight passengers

In this example, we will define some classes that represent various types of passengers on a flight, and then we will use a switch expression with pattern matching to determine the cost of their flight:

1.	In the `PacktLibraryNet2` project/folder, add a new file named `FlightPatterns.cs`.
2.	If you use Visual Studio, in `FlightPatterns.cs`, delete the existing statements, including the class named `FlightPatterns`, because we will define multiple classes, and none match the name of the code file.
3.	In `FlightPatterns.cs`, add statements to define three types of passengers with different properties:
```cs
// All the classes in this file will be defined in the following namespace.
namespace Packt.Shared;

public class Passenger
{
  public string? Name { get; set; }
}

public class BusinessClassPassenger : Passenger
{
  public override string ToString()
  {
    return $"Business Class: {Name}";
  }
}

public class FirstClassPassenger : Passenger
{
  public int AirMiles { get; set; }

  public override string ToString()
  {
    return $"First Class with {AirMiles:N0} air miles: {Name}";
  }
}

public class CoachClassPassenger : Passenger
{
  public double CarryOnKG { get; set; }

  public override string ToString()
  {
    return $"Coach Class with {CarryOnKG:N2} KG carry on: {Name}";
  }
}
```

> You will learn about overriding the `ToString` method in *Chapter 6, Implementing Interfaces and Inheriting Classes*.

4.	In `Program.cs`, add statements to define an object array containing five passengers of various types and property values, and then enumerate them, outputting the cost of their flight:
```cs
// An array containing a mix of passenger types.
Passenger[] passengers = {
  new FirstClassPassenger { AirMiles = 1_419, Name = "Suman" },
  new FirstClassPassenger { AirMiles = 16_562, Name = "Lucy" },
  new BusinessClassPassenger { Name = "Janice" },
  new CoachClassPassenger { CarryOnKG = 25.7, Name = "Dave" },
  new CoachClassPassenger { CarryOnKG = 0, Name = "Amit" },
};

foreach (Passenger passenger in passengers)
{
  decimal flightCost = passenger switch
  {
    FirstClassPassenger p when p.AirMiles > 35_000 => 1_500M,
    FirstClassPassenger p when p.AirMiles > 15_000 => 1_750M,
    FirstClassPassenger _                         => 2_000M,
    BusinessClassPassenger _                      => 1_000M,
    CoachClassPassenger p when p.CarryOnKG < 10.0 => 500M,
    CoachClassPassenger _                         => 650M,
    _                                             => 800M
  };
  WriteLine($"Flight costs {flightCost:C} for {passenger}");
}
```

While reviewing the preceding code, note the following:
- Most code editors do not align the lambda symbols `=>` as I have done above.
- To pattern match the properties of an object, you must name a local variable, like `p`, which can then be used in an expression.
- To pattern match on a type only, you can use `_` to discard the local variable; for example, `FirstClassPassenger _` means that you match on the type but you don’t care what values any of its properties have, so a named variable like `p` is not needed. In a moment, you will see how we can improve the code even more.
- The switch expression also uses `_` to represent its default branch.

5.	Run the `PeopleApp` project and view the result:
```
Flight costs $2,000.00 for First Class with 1,419 air miles: Suman
Flight costs $1,750.00 for First Class with 16,562 air miles: Lucy
Flight costs $1,000.00 for Business Class: Janice
Flight costs $650.00 for Coach Class with 25.70 KG carry on: Dave
Flight costs $500.00 for Coach Class with 0.00 KG carry on: Amit
```

## Enhancements to pattern matching in modern C#

You do not need to use the underscore to discard the local variable when doing type matching:

1.	In `Program.cs`, use a nested switch expression and the support for conditionals, like `>`, as highlighted in the following code:
```cs
decimal flightCost = passenger switch
{
  /* C# 8 syntax
  FirstClassPassenger p when p.AirMiles > 35_000 => 1_500M,
  FirstClassPassenger p when p.AirMiles > 15_000 => 1_750M,
  FirstClassPassenger _ => 2_000M, */

  // C# 9 or later syntax
  FirstClassPassenger p => p.AirMiles switch
  {
    > 35_000 => 1_500M,
    > 15_000 => 1_750M,
    _ => 2_000M
  },
  BusinessClassPassenger                        => 1_000M,
  CoachClassPassenger p when p.CarryOnKG < 10.0 => 500M,
  CoachClassPassenger                           => 650M,
  _                                             => 800M
};
```

2.	Run the `PeopleApp` project to view the results, and note that they are the same as before.

You could also use the relational pattern in combination with the property pattern to avoid the nested switch expression:
```cs
FirstClassPassenger { AirMiles: > 35000 } => 1500M,
FirstClassPassenger { AirMiles: > 15000 } => 1750M,
FirstClassPassenger                       => 2000M,
```

Pattern matching helps us work with objects by focusing on their shape and values. 
