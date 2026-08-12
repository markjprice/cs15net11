using System.Diagnostics.CodeAnalysis; // To use [SetsRequiredMembers].

namespace Packt.Shared;

public class Book
{
  public required string? Isbn;
  public required string? Title;
  public string? Author;
  public int PageCount;

  // Constructor for use with object initializer syntax.
  public Book() { }

  // Constructor with parameters to set required fields.
  [SetsRequiredMembers]
  public Book(string? isbn, string? title)
  {
    Isbn = isbn;
    Title = title;
  }
}
