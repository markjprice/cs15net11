# What does `new` do?

There have been a few examples of using the C# `new` keyword before this section, but so far, I haven’t explicitly explained what it does. The C# `new` keyword is used to allocate and/or initialize memory. To understand when you need to use `new`, you need to know a bit more about types.

Value and reference types and their relationship to memory are explained in more detail in *Chapter 6, Implementing Interfaces and Inheriting Classes*, so I will only provide a minimal explanation for now.

There are two categories of types: value types and reference types:
- **Value types** are simple and do not need to use the new keyword to explicitly allocate memory. But value types can use the new keyword to initialize their value. This is useful when there is no way to use a literal to set the value.
- **Reference types** are more complex and need to use the new keyword to explicitly allocate memory. At the same time, they can use the new keyword to initialize their state.

For example, when you declare variables, space is only allocated in memory for value types such as int and DateTime, not for reference types such as `Person`.

Consider the following code, which declares some local variables:
```cs
// Allocates 2 bytes of memory on the stack to store a System.Int16 value.
short age;

// Allocates 8 bytes of memory on the stack to store a System.Int64 value.
long population;

// Allocates 8 bytes of memory on the stack to store a System.DateTime value.
DateTime birthdate;

// Allocates 8 bytes of memory on the stack to store a System.Drawing.Point value.
Point location;

// Allocates memory in the stack that can point to a Person object in the heap.
// Initially, bob will have the value null.
Person bob;
```

Note the following about the preceding code:
- `age` has not been assigned a value (so the compiler with show an error if you attempt to access it) and 2 bytes of memory have been allocated in stack memory.
- `population` has not been assigned a value and 8 bytes of memory have been allocated in stack memory.
- `birthdate` has not been assigned a value and 8 bytes of memory have been allocated in stack memory.
- `location` has not been assigned a value and 8 bytes of memory have been allocated in stack memory.
- `bob` has a value of `null` and 4 or 8 bytes of memory have been allocated in stack memory. The size of the reference is typically 4 bytes on a 32-bit system and 8 bytes on a 64-bit system, corresponding to the size of a memory pointer. No heap memory has been allocated for the object.

Now let’s see when we might choose to use new:

```cs
// Initialize this variable to 45 using a literal value.
age = 45;

// Initialize this variable to 68 million using a literal value.
population = 68_000_000; 

// Initialize this variable to February 23, 1995.
// C# does not support literal values for date/time 
// values so we must use new.
birthdate = new(1995, 2, 23); 

// Initialize the X and Y coordinates of this value type.
location = new(10, 20); 

// Allocate memory on the heap to store a Person. 
// Any state will have default values. 
// bob is no longer null.
bob = new(); 

// Allocate memory on the heap to store a Person 
// and initialize state. bob is no longer null.
bob = new("Bob", "Smith", 45); 

// Older syntax with explicit types
birthdate = new DateTime(1995, 2, 23);
location = new Point(10, 20);
bob = new Person();
bob = new Person("Bob", "Smith", 45);
```

Note the following about the preceding code:
- `age`, `population`, `birthdate`, and `location` have already had memory allocated for them on the stack. We only need to use `new` to initialize their values if we want them to be different from their defaults.
- `bob` must use `new` to allocate heap memory for the object. The `=` assignment stores the memory address of that allocated memory on the stack. Reference types such as `Person` often have multiple constructors that are called by `new`. A default constructor assigns default values to any state in the object. A constructor with arguments can assign other values to any state in the object.

Constructors are covered in more detail in *Chapter 5, Building Your Own Types with Object-Oriented Programming*, so I have only provided a minimal explanation for now.

> **Prompt**: Please explain what the `new` keyword does for value types and reference types. Correct the common oversimplification that value types are always stored on the stack and reference types are always stored on the heap.
