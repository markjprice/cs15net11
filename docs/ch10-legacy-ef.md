# Understanding legacy Entity Framework
**Entity Framework (EF)** was first released back in 2008. Since then, EF has evolved, as Microsoft has observed how programmers use **object-relational mapping (ORM)** tools in the real world.

ORM uses a mapping definition to associate columns in tables with properties in classes. Then, a programmer can interact with objects of different types in a way that they are familiar with, instead of having to deal with knowing how to store the values in a relational table or another structure provided by a NoSQL data store.

The version of EF included with .NET Framework is **Entity Framework 6 (EF6)**. It is mature and stable and supports an EDMX (XML file) way of defining the model, as well as complex inheritance models and a few other advanced features.

EF 6.3 and later versions have been extracted from .NET Framework as a separate package, so they can be supported on .NET Core 3 and later. This enables existing projects, such as web applications and services, to be ported and run cross-platform. However, EF6 should be considered legacy technology because it has some limitations when running cross-platform, and no new features will be added to it.

# Using the legacy Entity Framework 6.3 or later

To use the legacy Entity Framework in a .NET Core 3 or later project, you must add a package reference to it in your project file:
```xml
<PackageReference Include="EntityFramework" Version="6.5.1" />
```

> **Good practice**: Only use legacy EF6 if you must; for example, you might use it to migrate a **Windows Presentation Foundation (WPF)** app that uses EF6 on .NET Framework to modern .NET. 

This book is about modern cross-platform development, so in the rest of this chapter, I will only cover the modern EF Core. You will not need to reference the legacy EF6 package, as shown above, in the projects for this chapter.

The truly cross-platform version, **EF Core**, is different from the legacy Entity Framework. Although EF Core has a similar name, you should be aware of how it varies from EF6. 
