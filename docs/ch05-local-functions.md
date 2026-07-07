# Implementing functionality using local functions

Local functions are the method equivalent of local variables. In other words, they are methods that are only accessible from within the containing method in which they have been defined. In other languages, they are sometimes called **nested** or **inner functions**.

Local functions can be defined anywhere inside a method: the top, the bottom, or even somewhere in the middle!

We will use a local function to implement a factorial calculation:
1.	In `Person.cs`, add statements to define a `Factorial` function that uses a local function inside itself to calculate the result:
```cs
// Method with a local function.
public static int Factorial(int number)
{
  if (number < 0)
  {
    throw new ArgumentException(
      $"{nameof(number)} cannot be less than zero.");
  }
  return localFactorial(number);

  int localFactorial(int localNumber) // Local function.
  {
    if (localNumber == 0) return 1;
    return localNumber * localFactorial(localNumber - 1);
  }
}
```

2.	In `Program.cs`, add statements to call the `Factorial` function, and write the return value to the console, with exception handling:
```cs
// Change to -1 to make the exception handling code execute.
int number = 5;
try
{
  WriteLine($"{number}! is {Person.Factorial(number)}");
}
catch (Exception ex)
{
  WriteLine($"{ex.GetType()} says: {ex.Message} number was {number}.");
}
```
3.	Run the `PeopleApp` project and view the result:
```
5! is 120
```
4.	Change the number to `-1` so that we can check the exception handling.
5.	Run the `PeopleApp` project and view the result:
```
System.ArgumentException says: number cannot be less than zero. number was -1.
```
