# Prompts for *Chapter 10 Working with Data Using Entity Framework Core*

```
Compare database-first and code-first EF Core development. When
would a professional team choose each approach?
```

```
Please explain when a developer should choose ADO.NET instead of
EF Core. Give examples where the lower-level API is a better fit.
```

```
Please explain how EF Core conventions map C# classes and properties
to database tables and columns. Give examples where conventions are not
enough.
```

```
Please explain object-relational mapping using a simple C# class
and a relational database table. Show what EF Core is doing behind the
scenes.
```

```
Compare EF Core conventions, data annotation attributes, and the
Fluent API. Which one wins when more than one configuration method is used?
```

```
Please explain why it can be useful to put EF Core entity classes
and the DbContext class in separate class library projects.
```

```
Please explain the difference between a DbContext, a DbSet<T>, an
entity class, and a navigation property in EF Core.
```

```
Please explain why EF Core scaffolding generates partial classes.
How should I safely customize generated entity classes?
```

```
Please explain SQLite's dynamic typing and how it can affect EF
Core scaffolding compared with SQL Server. Explain how EF Core maps .NET
decimal values to SQLite. What are the trade-offs when converting decimal
to double?
```

```
Please explain deferred execution in LINQ to EF Core. When does
a query get converted into SQL and sent to the database? Please also
explain why DbContext instances should usually be short-lived. What can
go wrong if a DbContext is shared across multiple threads?
```

```
Please explain what ToQueryString does in EF Core. What can it
show, and what can it not show?
```

```
Please explain the difference between EF Core logging,
EnableSensitiveDataLogging, and EnableDetailedErrors. Why should sensitive
data logging be disabled in production?
```

```
Please explain EF Core query tags. How do they help when reading
logs from a large application?
```

```
Please explain the difference between First, FirstOrDefault, Single,
and SingleOrDefault in EF Core. Why might Single generate SQL that asks
for two rows?
```

```
Please explain global query filters in EF Core. Show how they can
be used for soft deletes and multi-tenant apps.
```

```
Please explain how EF Core parameterizes queries to avoid SQL
injection. Compare LINQ queries, FromSql, and FromSqlRaw.
```
