# Limiting flags enum values

Earlier in this chapter, we defined a field to store a person’s favorite ancient wonder. But we then made the `enum` able to store combinations of values. 

Now, let’s limit the favorite to one:
1.	In `Person.cs`, comment out the `FavoriteAncientWonder` field and add a comment to note it has moved to the `PersonAutoGen.cs` code file:
```cs
// This has been moved to PersonAutoGen.cs as a property.
// public WondersOfTheAncientWorld FavoriteAncientWonder;
```

2.	In `PersonAutoGen.cs`, add a private field and public property for `FavoriteAncientWonder`:
```cs
private WondersOfTheAncientWorld _favoriteAncientWonder;

public WondersOfTheAncientWorld FavoriteAncientWonder
{
  get { return _favoriteAncientWonder; }
  set
  {
    string wonderName = value.ToString();

    if (wonderName.Contains(','))
    {
      throw new ArgumentException(
        message: "Favorite ancient wonder can only have a single enum value.",
        paramName: nameof(FavoriteAncientWonder));
    }

    if (!Enum.IsDefined(typeof(WondersOfTheAncientWorld), value))
    {
      throw new ArgumentException(
        $"{value} is not a member of the WondersOfTheAncientWorld enum.",
        paramName: nameof(FavoriteAncientWonder));
    }

    _favoriteAncientWonder = value;
  }
}
```

> We could simplify the validation by only checking if the value is defined in the original `enum` because `IsDefined` returns `false` for multiple values and undefined values. However, I want to show a different exception for multiple values, so I will use the fact that multiple values formatted as a string would include a comma in the list of names. This also means we must check for multiple values before we check if the value is defined. A comma-separated list is how multiple enum values are represented as a string, but you cannot use commas to set multiple enum values. You should use `|` (the bitwise OR).

3.	In `Program.cs`, in the *Storing a value using an enum type* region, set Bob’s favorite wonder to more than one `enum` value:
```cs
bob.FavoriteAncientWonder =
  WondersOfTheAncientWorld.StatueOfZeusAtOlympia |
  WondersOfTheAncientWorld.GreatPyramidOfGiza;
```
4.	Run the `PeopleApp` project and note the exception:
```
Unhandled exception. System.ArgumentException: Favorite ancient wonder can only have a single enum value. (Parameter 'FavoriteAncientWonder')
   at Packt.Shared.Person.set_FavoriteAncientWonder(WondersOfTheAncientWorld value) in C:\cs15net11\Chapter05\PacktLibraryNet2\PersonAutoGen.cs:line 67
   at Program.<Main>$(String[] args) in C:\cs15net11\Chapter05\PeopleApp\Program.cs:line 57
```
5.	In `Program.cs`, set Bob’s favorite wonder to an invalid enum value like `128`:
```cs
bob.FavoriteAncientWonder = (WondersOfTheAncientWorld)128;
```

6.	Run the `PeopleApp` project and note the exception:
```
Unhandled exception. System.ArgumentException: 128 is not a member of the WondersOfTheAncientWorld enum. (Parameter 'FavoriteAncientWonder')
```

7.	In `Program.cs`, set Bob’s favorite wonder back to a valid single enum value.
