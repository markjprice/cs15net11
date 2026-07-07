# Raising and handling events

- [Raising and handling events](#raising-and-handling-events)
  - [Calling methods using delegates](#calling-methods-using-delegates)
  - [Defining and handling delegates](#defining-and-handling-delegates)
  - [Defining and handling events](#defining-and-handling-events)
  - [Multicast events in GUI apps](#multicast-events-in-gui-apps)


Methods are often described as actions that an object can perform, either on itself or on related objects. For example, `List<T>` can add an item to itself or clear itself, and `File` can create or delete a file in the filesystem.

**Events** are often described as actions that happen *to* an object.

For example, in a user interface, `Button` has a `Click` event, a click being something that happens to a button. Also, `FileSystemWatcher` listens to the filesystem for change notifications and raises events like `Created` and `Deleted`, which are triggered when a directory or file changes. Another way to think of events is that they provide a way of exchanging messages between objects.

Events are built on **delegates**, so let’s start by having a look at what delegates are and how they work.

## Calling methods using delegates

You have already seen the most common way to call or execute a method: using the `.` operator to access the method using its name. For example, `Console.WriteLine` tells the `Console` type to call its `WriteLine` method.

The other way to call or execute a method is to use a delegate. A delegate contains the memory address of a method that must match the same signature as the delegate, enabling it to be called safely with the correct parameter types. If you have used languages that support function pointers, then think of a delegate as being a *type-safe method pointer*.

The code in this section is illustrative and not meant to be typed into a project. You will explore code like this in the next section, so for now, just read the code and try to understand its meaning.

For example, imagine there is a method in the `Person` class that must have a `string` value passed as its only parameter, and it returns an `int` type:
```cs
public class Person
{
  public int MethodIWantToCall(string input)
  {
    ...
    return input.Length; // It doesn't matter what the method does.
  }
...
```

I can call this method on an instance of `Person` named `p1` like this:
```cs
Person p1 = new();
int answer = p1.MethodIWantToCall("Frog");
```

Alternatively, I can define a delegate with a matching signature to call the method indirectly. Note that the names of the parameters do not have to match. Only the types of parameters and return values must match:
```cs
delegate int DelegateWithMatchingSignature(string s);
```

> **Good practice**: A delegate is a reference type like a `class`, so if you define one in `Program.cs`, then it must be at the bottom of the file. It would be best to define it in its own class file, for example, `Program.Delegates.cs`. If you defined a delegate in the middle of `Program.cs`, then you would see the following compiler error: `CS8803: Top-level statements must precede namespace and type declarations.`

Now, I can create an instance of the delegate, point it at the method, and finally, call the delegate (which calls the method):
```cs
// Create a delegate instance that points to the method.
DelegateWithMatchingSignature d = new(p1.MethodIWantToCall);

// Call the delegate, which then calls the method.
int answer2 = d("Frog");
```

You are probably thinking, “What’s the point in that?”

It provides flexibility. For example, we could use delegates to create a queue of methods that need to be called in order. Queuing actions that need to be performed is common in services to provide improved scalability.

Another example is to allow multiple actions to execute in parallel. Delegates have built-in support for asynchronous operations that run on a different thread, which can provide improved responsiveness.

But the most important example is that delegates allow us to implement events to send messages between different objects that do not need to know about each other. Events are an example of loose coupling between components because they do not need to know about each other; they just need to know the event signature.

Delegates and events are two of the most confusing features of C# and can take a few attempts to understand, so don’t worry if you feel lost as we walk through how they work! Move on to other topics and come back again another day when your brain has had the opportunity to process the concepts while you sleep.

## Defining and handling delegates

Microsoft has two predefined delegates for use as events. They both have two parameters:
- `object? sender`: This parameter is a reference to the object raising the event or sending the message. The `?` indicates that this reference could be `null`.
- `EventArgs e` or `TEventArgs e`: This parameter contains additional relevant information about the event. For example, in a GUI app, you might define `MouseMoveEventArgs`, which has properties for the `X` and `Y` coordinates for the mouse pointer. A bank account might have a `WithdrawEventArgs` with a property for the `Amount` to withdraw.

Their signatures are simple, yet flexible:
```cs
// For methods that do not need additional argument values passed in.
public delegate void EventHandler(object? sender, EventArgs e);

// For methods that need additional argument values passed in as
// defined by the generic type TEventArgs.
public delegate void EventHandler<TEventArgs>(object? sender, TEventArgs e);
```

> **Good practice**: When you want to define an event in your own type, you should use one of these two predefined delegates.

Some types provide “empty” values for when you need an instance, but it doesn’t need to have any particular value. For example:
- `string.Empty` is an empty `string` value `""`. You can think of `string.Empty` as a global single instance of an empty string.
- `EventArgs.Empty` is an empty `EventArgs` value. Use it when you must conform to the built-in event delegates that require an `EventArgs` instance to be passed as a parameter, but it doesn’t need any particular value because it won’t be read or used in the method anyway.

You might have a delegate defined:
```cs
public EventHandler? Shout; // This field could be null.
```

In that case, there are multiple ways to call the delegate:
- Use its variable name, `Shout`:
```cs
Shout(this, EventArgs.Empty);
```

- Use its `Invoke` method to call it synchronously:
```cs
Shout.Invoke(this, EventArgs.Empty);
```

- Use its `BeginInvoke` method to call it asynchronously, without a callback function or any state:
```cs
IAsyncResult result = Shout.BeginInvoke(
  this, EventArgs.Empty, null, null);
```

The `BeginInvoke` method is beyond the scope of this book, but I have included it so that you know it exists.

Delegates and their method handlers have a potentially many-to-many relationship. One delegate can have one method handler. But one delegate can also have many method handlers (you will do this when you hook up `Shout` to both `Harry_Shout` and `Harry_Shout_2`). And many delegates can reference one method handler, or any combination of these. Let’s explore delegates and events:

1.	Add statements to the `Person` class and note the following points:
- It defines an `EventHandler` delegate field named `Shout`.
- It defines an `int` field to store `AngerLevel`.
- It defines a method named `Poke`.
- Each time a person is poked, their `AngerLevel` increments. Once their `AngerLevel` reaches `3`, they raise the `Shout` event, but only if there is at least one event delegate pointing at a method defined somewhere else in the code; that is, it is not null:
```cs
#region Events

// Delegate field to define the event.
public EventHandler? Shout; // null initially.

// Data field related to the event.
public int AngerLevel;

// Method to trigger the event in certain conditions.
public void Poke()
{
  AngerLevel++;
  if (AngerLevel < 3) return;

  // If something is listening to the event...
  if (Shout is not null)
  {
    // ...then call the delegate to "raise" the event.
    Shout(this, EventArgs.Empty);
  }
}

#endregion
```

> Checking that an object is not `null` before calling one of its methods is very common. null checks can be simplified inline using a `?` symbol before the `.` operator:
```cs
Shout?.Invoke(this, EventArgs.Empty);
```

2.	In the `PeopleApp` project, add a new class file named `Program.EventHandlers.cs`.
3.	In `Program.EventHandlers.cs`, delete any existing statements, and then add a method with a matching signature that gets a reference to the `Person` object from the `sender` parameter and outputs some information about them:
```cs
using Packt.Shared; // To use Person.

// No namespace declaration so this extends the Program class
// in the null namespace.
partial class Program
{
  // A method to handle the Shout event received by the harry object.
  private static void Harry_Shout(object? sender, EventArgs e)
  {
    // If no sender, then do nothing.
    if (sender is null) return;
    // If sender is not a Person, then do nothing and return; else assign sender to p.
    if (sender is not Person p) return;
    WriteLine($"{p.Name} is this angry: {p.AngerLevel}.");
  }
}
```

> **Good practice**: Microsoft’s convention for method names that handle events is `ObjectName_EventName`. In this project, `sender` will always be a `Person` instance, so the null checks are not necessary, and the event handler could be much simpler with just the `WriteLine` statement. However, it is important to know that these types of null checks make your code more robust in cases of event misuse.

You can have as many methods as you like to be event handlers, named whatever you like, as long as the method signature matches the delegate signature. This means you could have 50 `Person` instances, each with its own method, or have one method that they all share. The methods can be declared at any level that makes sense for the scenario and matches the access levels set (like `protected`, `private`, `public`, and so on). One of the key benefits of delegates and events is loose binding between components, so maximum flexibility is desired.

4.	In `Program.cs`, add a statement to assign the method to the delegate field, and then add statements to call the `Poke` method four times:
```cs
// Assign the method to the Shout delegate.
harry.Shout = Harry_Shout;

// Call the Poke method that eventually raises the Shout event.
harry.Poke();
harry.Poke();
harry.Poke();
harry.Poke();
```

5.	Run the `PeopleApp` project and view the result. Note that Harry says nothing the first two times he is poked, and only gets angry enough to shout once he’s been poked at least three times:
```
...
Harry is this angry: 3.
Harry is this angry: 4.
```

In step 3, note that the `sender` is checked to make sure it is a `Person` instance, and if it is, then it is assigned to a local variable named `p`:
```cs
// If sender is not a Person, then do nothing and return; else assign sender to p.
if (sender is not Person p) return;
WriteLine($"{p.Name} is this angry: {p.AngerLevel}.");
```

The first statement does two things at once, which needs more explanation. The parameter named `sender` is declared to be of type `object`. This means we cannot just say `sender.Name` or `sender.AngerLevel`. We need to cast `sender` to a local variable that is explicitly defined as `Person`. We also need to check that `sender` actually is a `Person`.

We can do both things at once in a single expression: `sender is not Person p`. This expression will return `true` if sender is not a `Person`, and hence the statement executes `return;`, so the method immediately returns. 

Or the expression returns `false` if `sender` is a `Person`, and `sender` will be stored in the local variable named `p`, which is of type `Person`. After that, we can use expressions like `p.Name` and `p.AngerLevel`.

## Defining and handling events

You’ve now seen how delegates implement the most important functionality of events: the ability to define a signature for a method that can be implemented by a completely different piece of code, calling that method and any others that are hooked up to the delegate field.

But what about events? There is less to them than you might think.

When assigning a method to a delegate field, you should not use the simple assignment operator as we did in the preceding example.

Delegates are multicast, meaning that you can assign multiple delegates to a single delegate field. Instead of the `=` assignment, we could have used the `+=` operator so that we could add more methods to the same delegate field. 

When the delegate is called, all the assigned methods are called, although you have no control over the order in which they are called. Do not use events to implement a queuing system to buy concert tickets; otherwise, the wrath of millions of Swifties will fall upon you.

If the `Shout` delegate field already referenced one or more methods, by assigning another method, that method would replace all the others. With delegates that are used for events, we usually want to make sure that a programmer only ever uses either the `+=` operator or the `-=` operator to assign and remove methods.

To enforce this:
1.	In `Person.cs`, add the event keyword to the delegate field declaration:
```cs
public event EventHandler? Shout;
```

2.	Build the `PeopleApp` project and note the compiler error message:
```
Program.cs(41,13): error CS0079: The event 'Person.Shout' can only appear on the left hand side of += or -=
```

> This is (almost) all that the `event` keyword does! If you will never have more than one method assigned to a delegate field, then technically you do not need events, but it is still good practice to indicate your meaning and that you expect a delegate field to be used as an event.

3.	In `Program.cs`, modify the comment and the method assignment to use `+=` instead of just `=`:
```cs
// Assign the method to the Shout event delegate.
harry.Shout += Harry_Shout;
```

4.	Run the `PeopleApp` project and note that it has the same behavior as before.
5.	In `Program.EventHandlers.cs`, create a second event handler for Harry’s `Shout` event:
```cs
// Another method to handle the event received by the harry object.
private static void Harry_Shout_2(object? sender, EventArgs e)
{
  WriteLine("Stop it!");
}
```

6.	In `Program.cs`, after the statement that assigns the `Harry_Shout` method to the `Shout` event, add a statement to attach the new event handler to the `Shout` event too:
```cs
// Assign the method(s) to the Shout event delegate.
harry.Shout += Harry_Shout;
harry.Shout += Harry_Shout_2;
```

7.	Run the `PeopleApp` project, then view the result. Note that both event handlers execute whenever an event is raised, which only happens once the anger level is `3` or more:
```cs
Harry is this angry: 1.
Harry is this angry: 2.
Harry is this angry: 3.
Stop it!
Harry is this angry: 4.
Stop it!
```

## Multicast events in GUI apps

In Windows desktop development, imagine that you have three buttons: `AddButton`, `SaveButton`, and `DeleteButton`. Each button has very different functionality. Good practice would be to create three methods to handle their `Click` events, named `AddButton_Click`, `SaveButton_Click`, and `DeleteButton_Click`. Each would have a different implementation code.

But now, imagine you have 26 buttons: `AButton`, `BButton`, `CButton`, and so on, up to `ZButton`. Each button has the same functionality: to filter a list of people by the first letter of their name. Good practice would be to create one method to handle their `Click` events, perhaps named `AtoZButtons_Click`. This method would have an implementation code that would use the `sender` parameter to know which button was clicked, and therefore how to apply the filtering, but otherwise be the same for all the buttons.
