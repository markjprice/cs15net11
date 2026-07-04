# Why you should always use braces with if statements

A block is zero or more statements enclosed in braces `{ }`. The C# specification refers to a block as a **compound statement**. They are what allow multiple statements to be grouped together wherever a single statement is expected.

All C# keyword control statements like `if`, `else`, `while`, and `for` can use either a single statement without braces or one or more statements wrapped in braces, also known as an **embedded statement**. A block or compound statement is one kind of embedded statement, and a single statement without braces is another kind of embedded statement.

Here is the safe way to write a typical `if-else` block using braces:
```cs
string password = "ninja";

if (password.Length < 8)
{
  WriteLine("Your password is too short. Use at least 8 chars.");
}
else
{
  WriteLine("Your password is strong.");
}

// Continue processing.
```

As there is only a single statement inside each block, the preceding code could be written without the curly braces:
```cs
string password = "ninja";

if (password.Length < 8)
  WriteLine("Your password is too short. Use at least 8 chars.");
else
  WriteLine("Your password is strong.");

// Continue processing.
```

This style of `if` statement should be avoided because it can introduce serious bugs. An infamous example is the `#gotofail` bug in Apple’s iPhone iOS operating system. For 18 months after Apple’s iOS 6 was released, in September 2012, it had a bug due to an if statement without braces in its Secure Sockets Layer (SSL) encryption code. This meant that any user running Safari, the device’s web browser, who tried to connect to secure websites, such as their bank, was not properly secure because an important check was being accidentally skipped.

It is clearer to always use braces for embedded statements for all keywords that allow them, not just if statements, so that is my recommendation.

An embedded statement is a statement used as a sub-statement of a control statement. It can be a block or a single statement. You can learn more about statements in the C# Language Specification at the following link: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/statements.

> **Good practice**: Just because you can leave out the curly braces, doesn’t mean you should. Your code is not “more efficient” without them; instead, it is harder to read, less maintainable, and, potentially, more dangerous.
