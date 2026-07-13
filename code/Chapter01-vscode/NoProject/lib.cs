#:property OutputType=Library

namespace MyLib;

public static class Greeter
{
  public static string Greet(string name)
  {
    return $"Hello, {name}!";
  }
}
