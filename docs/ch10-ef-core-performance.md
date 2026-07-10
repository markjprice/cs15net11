- [Avoiding EF Core performance traps](#avoiding-ef-core-performance-traps)
  - [Start by measuring, not guessing](#start-by-measuring-not-guessing)
  - [Mistake 1: Not looking at the SQL](#mistake-1-not-looking-at-the-sql)
  - [Mistake 2: Forgetting that indexes and statistics matter](#mistake-2-forgetting-that-indexes-and-statistics-matter)
  - [Mistake 3: Creating N+1 queries with lazy loading](#mistake-3-creating-n1-queries-with-lazy-loading)
  - [Mistake 4: Causing cartesian explosion with multiple collection includes](#mistake-4-causing-cartesian-explosion-with-multiple-collection-includes)
  - [Mistake 5: Returning full entities when a projection would do](#mistake-5-returning-full-entities-when-a-projection-would-do)
  - [Mistake 6: Calling `ToList` too early](#mistake-6-calling-tolist-too-early)
  - [Mistake 7: Tracking read-only data unnecessarily](#mistake-7-tracking-read-only-data-unnecessarily)
  - [Mistake 8: Keeping a DbContext alive too long or sharing it between threads](#mistake-8-keeping-a-dbcontext-alive-too-long-or-sharing-it-between-threads)
  - [Mistake 9: Updating or deleting rows one entity at a time](#mistake-9-updating-or-deleting-rows-one-entity-at-a-time)
  - [Mistake 10: Building unsafe or uncacheable dynamic queries](#mistake-10-building-unsafe-or-uncacheable-dynamic-queries)
  - [Mistake 11: Blocking server threads with synchronous database calls](#mistake-11-blocking-server-threads-with-synchronous-database-calls)
  - [A practical EF Core performance checklist](#a-practical-ef-core-performance-checklist)
  - [Links](#links)

# Avoiding EF Core performance traps

EF Core does not remove the need to understand databases. It removes a lot of repetitive data access code, but the database still executes SQL, uses indexes and statistics, chooses query plans, transfers rows, locks data, and returns results. EF Core adds some runtime overhead, but for most real applications, query efficiency, index usage, database I/O, network latency, and roundtrips matter far more.

This section summarizes ten common EF Core performance mistakes. These are not advanced edge cases. They are the kinds of problems that appear when code works correctly with a small development database but becomes slow, expensive, or unreliable when it meets production data.

## Start by measuring, not guessing

Before changing code, identify the query that is slow. EF Core can log generated SQL, `ToQueryString` can show the SQL for many LINQ queries, and `TagWith` can add comments to generated SQL so that it is easier to connect a line of C# code to a query seen in logs or database tools. I recommend diagnosing performance problems before assuming the root cause.

```cs
decimal minimumPrice = 20.00M;

IQueryable<Product> query = db.Products
  .TagWith("Products costing more than the minimum price.")
  .Where(p => p.UnitPrice > minimumPrice)
  .OrderBy(p => p.ProductName);

WriteLine(query.ToQueryString());
```

`ToQueryString` does not execute the query. It lets you inspect the SQL that EF Core intends to send to the database. The query is executed later, when it is enumerated or materialized, for example by calling `foreach`, `ToList`, `First`, or `Count`.

> **Good practice**: Do not optimize EF Core code blindly. First find the SQL, then inspect the database query plan, then decide whether the fix belongs in C#, EF Core mapping, indexes, statistics, or database design.

## Mistake 1: Not looking at the SQL

A LINQ query can look simple while the generated SQL is expensive. For example, an innocent-looking query might select more columns than needed, use a join that multiplies rows, ignore an index, call a function that prevents index usage, or run once per item in a loop.

The first mistake beginners make is to reason only about the C# code. EF Core translates LINQ into provider-specific SQL, and the database executes the SQL. If the SQL is poor, the query will be poor.

For learning, log the SQL and compare it with the LINQ:

```cs
optionsBuilder
  .UseSqlite(connectionString)
  .LogTo(WriteLine);
```

For focused diagnostics, log only the events you need. For production diagnostics, use structured logging and database tools rather than writing raw SQL logs to the console.

> **Warning!** Do not enable sensitive data logging in production. It can include parameter values in logs, and those values might contain personal data, search terms, identifiers, or secrets.

## Mistake 2: Forgetting that indexes and statistics matter

Missing indexes can turn a fast application into a slow one. A query that should seek into a small part of a table might instead scan the whole table. This can happen even if the EF Core code is well written.

Indexes are especially important for columns used in `Where`, `Join`, `OrderBy`, foreign key relationships, and common lookup patterns. Use indexes correctly and check query plans to identify scans and inefficient access patterns.

For SQL Server, statistics are also part of the story. The SQL Server Query Optimizer uses statistics to estimate how many rows a query will return, and those estimates influence which plan it chooses. Stale or misleading statistics can cause the optimizer to choose a poor plan.

For example, a table might have grown from 10,000 rows to 10 million rows, but the statistics might not reflect the new distribution of values. The optimizer might then choose a plan that made sense for the old data but is disastrous for the current data.

> **Good practice**: When a query is slow, inspect the actual execution plan. Look for table scans, index scans where you expected seeks, large row-count estimation errors, expensive sorts, hash joins, key lookups, and warnings. SQL Server Management Studio can show actual execution plans with runtime information.

> **Warning!** Missing-index suggestions are suggestions, not commands. Review table structure, column order, existing indexes, included columns, and write overhead before adding indexes. I recommend reviewing and combining missing-index suggestions carefully.

## Mistake 3: Creating N+1 queries with lazy loading

The N+1 problem happens when code runs one query to load a set of parent rows, and then one more query for each parent row to load related data. If the first query returns 100 categories, the code might execute 101 queries. If it returns 10,000 rows, the result can be catastrophic.

This often happens when lazy loading is enabled and a navigation property is accessed inside a loop. Lazy loading can cause unnecessary extra roundtrips, and community performance discussions repeatedly identify N+1 queries as one of the most common ORM problems.

Poor code:

```cs
foreach (Category category in db.Categories)
{
  WriteLine(category.CategoryName);

  foreach (Product product in category.Products)
  {
    WriteLine($"  {product.ProductName}");
  }
}
```

If lazy loading is enabled, accessing `category.Products` might execute a separate SQL query for each category.

Better code when you need entities:

```cs
List<Category> categories = db.Categories
  .Include(c => c.Products)
  .ToList();

foreach (Category category in categories)
{
  WriteLine(category.CategoryName);

  foreach (Product product in category.Products)
  {
    WriteLine($"  {product.ProductName}");
  }
}
```

Often better code for read-only output:

```cs
var categories = db.Categories
  .Select(c => new
  {
    c.CategoryName,
    Products = c.Products.Select(p => p.ProductName).ToList()
  })
  .ToList();
```

> **Good practice**: Disable lazy loading by default in web applications. Load related data intentionally using projection, `Include`, filtered `Include`, or explicit loading.

## Mistake 4: Causing cartesian explosion with multiple collection includes

`Include` is useful, but it is not magic. When a query includes multiple collection navigation properties at the same level, the database might return a cross product of the related rows. This is called cartesian explosion and shows that if one parent row has 10 posts and 10 contributors, a single query can return 100 rows for that one parent.

The dangerous shape looks like this:

```cs
var blogs = db.Blogs
  .Include(b => b.Posts)
  .Include(b => b.Contributors)
  .ToList();
```

The two collection includes are siblings. Each post row can be combined with each contributor row. More sibling collections mean more multiplication.

A split query can avoid the cross product by sending separate SQL queries for the related collections:

```cs
var blogs = db.Blogs
  .Include(b => b.Posts)
  .Include(b => b.Contributors)
  .AsSplitQuery()
  .ToList();
```

`AsSplitQuery` is not always better. It can reduce duplicated rows, but it sends multiple queries. That means more roundtrips and different consistency trade-offs if data changes between queries.

> **Good practice**: For read-only screens and web API responses, prefer projection to a DTO. Use `Include` when you genuinely need entity graphs, not just because you need values from related tables.

## Mistake 5: Returning full entities when a projection would do

A common beginner query asks the database for whole entity objects when the user interface only needs a few values:

```cs
List<Product> products = db.Products
  .Where(p => !p.Discontinued)
  .ToList();
```

This can fetch every mapped column, create entity instances, set up tracking, and expose data that the caller does not need. I recommend projecting only the properties required by the application.

Better code:

```cs
var products = db.Products
  .Where(p => !p.Discontinued)
  .OrderBy(p => p.ProductName)
  .Select(p => new
  {
    p.ProductId,
    p.ProductName,
    p.UnitPrice
  })
  .ToList();
```

For an ASP.NET Core web API, project into a DTO:

```cs
var products = db.Products
  .Where(p => !p.Discontinued)
  .OrderBy(p => p.ProductName)
  .Select(p => new ProductSummaryDto
  {
    Id = p.ProductId,
    Name = p.ProductName,
    Price = p.UnitPrice
  })
  .ToList();
```

> **Good practice**: Do not return EF Core entity classes directly from web APIs. Use DTOs or view models that contain only the data the caller needs.

## Mistake 6: Calling `ToList` too early

`ToList`, `ToArray`, `First`, `Single`, `Count`, and similar methods execute a query. If you call `ToList` too early, the rest of the work happens in .NET instead of in the database.

Poor code:

```cs
List<Product> allProducts = db.Products.ToList();

List<Product> filtered = allProducts
  .Where(p => p.UnitPrice > 20)
  .OrderBy(p => p.ProductName)
  .Take(10)
  .ToList();
```

This loads every product from the database, then filters, sorts, and limits the results in memory.

Better code:

```cs
List<Product> filtered = db.Products
  .Where(p => p.UnitPrice > 20)
  .OrderBy(p => p.ProductName)
  .Take(10)
  .ToList();
```

The better version lets EF Core translate the filter, sort, and limit into SQL so the database can do the work.

> **Warning!** Development databases are often too small to reveal this mistake. Code that works instantly with 77 Northwind products might fail badly with 77 million real products.

## Mistake 7: Tracking read-only data unnecessarily

EF Core tracks entity instances by default. This is useful when you plan to modify those objects and call `SaveChanges`. It is unnecessary overhead when you only need to read and display data.

No-tracking queries are useful for read-only scenarios and are generally quicker because EF Core does not need to set up change-tracking information.

Use `AsNoTracking` for read-only entity queries:

```cs
List<Product> products = db.Products
  .AsNoTracking()
  .Where(p => !p.Discontinued)
  .OrderBy(p => p.ProductName)
  .ToList();
```

However, do not treat `AsNoTracking` as a cure for every performance problem. If you project directly to a DTO and do not carry entity instances inside the projection, then entity tracking is usually not the main issue.

```cs
var products = db.Products
  .Where(p => !p.Discontinued)
  .Select(p => new ProductSummaryDto
  {
    Id = p.ProductId,
    Name = p.ProductName,
    Price = p.UnitPrice
  })
  .ToList();
```

Stack Overflow questions show that developers often misunderstand where to place `AsNoTracking` in complex queries with `Include`. It applies to the query, so repeating it after every `Include` is redundant.

> **Good practice**: Use `AsNoTracking` for read-only entity queries. For web APIs and list pages, prefer projection first, then consider whether tracking is relevant at all.

## Mistake 8: Keeping a DbContext alive too long or sharing it between threads

A `DbContext` is a unit-of-work object. It is not a database connection singleton, a global cache, or a thread-safe service. A `DbContext` instance is designed for a single unit of work, its lifetime is usually short, and it is not thread-safe.

A typical unit of work is:

1. Create a `DbContext`.
2. Query or attach entities.
3. Make changes if needed.
4. Call `SaveChanges` or `SaveChangesAsync`.
5. Dispose the `DbContext`.

In ASP.NET Core, the default scoped lifetime often works well because each HTTP request naturally maps to a unit of work. In Blazor Server, background workers, desktop apps, and parallel operations, you must be more deliberate. Do not run two EF Core operations at the same time on the same context instance.

Poor code:

```cs
Task<List<Product>> expensiveProductsTask = db.Products
  .Where(p => p.UnitPrice > 50)
  .ToListAsync();

Task<List<Category>> categoriesTask = db.Categories
  .ToListAsync();

await Task.WhenAll(expensiveProductsTask, categoriesTask);
```

This starts two operations on the same context. Use separate context instances, or run the operations sequentially.

> **Good practice**: Keep `DbContext` instances short-lived. In code that needs to create contexts on demand, consider using `IDbContextFactory<TContext>`.

## Mistake 9: Updating or deleting rows one entity at a time

Beginners often load many rows into memory, loop through them, modify each entity, and then call `SaveChanges`:

```cs
List<Product> products = db.Products
  .Where(p => p.UnitsInStock == 0)
  .ToList();

foreach (Product product in products)
{
  product.Discontinued = true;
}

db.SaveChanges();
```

This loads data that is not needed by the application, sets up change tracking for every entity, and then sends updates. For set-based updates and deletes, EF Core provides `ExecuteUpdate` and `ExecuteDelete`. `ExecuteUpdate` can perform the operation in one roundtrip without loading entities and without using the change tracker.

Better code:

```cs
int affected = db.Products
  .Where(p => p.UnitsInStock == 0)
  .ExecuteUpdate(setters => setters
    .SetProperty(p => p.Discontinued, true));

WriteLine($"{affected} products were discontinued.");
```

For asynchronous server-side code:

```cs
int affected = await db.Products
  .Where(p => p.UnitsInStock == 0)
  .ExecuteUpdateAsync(setters => setters
    .SetProperty(p => p.Discontinued, true));
```

> **Warning!** Do not call `SaveChanges` inside a loop unless each iteration genuinely needs its own transaction boundary.

## Mistake 10: Building unsafe or uncacheable dynamic queries

Raw SQL is sometimes useful. It is also easy to misuse.

`FromSql` and `FromSqlInterpolated` are safe against SQL injection because parameter values are sent separately from the SQL text. `FromSqlRaw` can be vulnerable if you insert untrusted values directly into the SQL string.

Safer code:

```cs
string city = "London";

List<Customer> customers = db.Customers
  .FromSql($"SELECT * FROM Customers WHERE City = {city}")
  .ToList();
```

Risky code:

```cs
string city = ReadLine()!;

List<Customer> customers = db.Customers
  .FromSqlRaw(
    $"SELECT * FROM Customers WHERE City = '{city}'")
  .ToList();
```

The second example builds SQL text by injecting a string directly into it. A malicious value could change the meaning of the SQL.

Dynamic query construction can also hurt performance even when it is not a security bug. EF Core caches queries by expression tree shape. If dynamically generated queries create a different shape every time, EF Core and the database might lose the benefit of query-plan reuse. Dynamically constructed queries are a common source of performance problems when built incorrectly.

> **Good practice**: Prefer LINQ for composable queries. Use `FromSql` for parameterized SQL. Use `FromSqlRaw` only when you must dynamically compose SQL structure, such as a column name, and validate that structure against a whitelist.

## Mistake 11: Blocking server threads with synchronous database calls

The examples in this book use synchronous EF Core calls in console apps because that keeps the learning path simple. In ASP.NET Core, prefer asynchronous methods such as `ToListAsync`, `FirstOrDefaultAsync`, `SingleAsync`, `AnyAsync`, and `SaveChangesAsync`.

Asynchronous operations avoid blocking a thread while the query runs in the database, and that this can increase throughput in web applications because the thread can serve other requests while waiting for I/O.

Console app learning code:

```cs
List<Product> products = db.Products.ToList();
```

Server-side web app code:

```cs
List<Product> products = await db.Products.ToListAsync();
```

> **Warning!** Async does not mean parallel on the same context. EF Core does not support multiple parallel operations on one `DbContext` instance. Await each operation before starting the next one, or use separate context instances.

## A practical EF Core performance checklist

When an EF Core query is slow, ask these questions in order:

1. Which C# query produced the SQL?
2. What SQL did EF Core generate?
3. How many times did the SQL execute?
4. How many rows and columns came back?
5. Is the query using the expected indexes?
6. Are SQL Server statistics current enough for a good plan?
7. Is there an accidental N+1 pattern?
8. Is there cartesian explosion from sibling collection includes?
9. Is the query tracking entities that will never be updated?
10. Is the code loading entities when a projection would be enough?
11. Is `ToList`, `AsEnumerable`, or `AsAsyncEnumerable` moving work from the database to .NET too early?
12. Is the `DbContext` lifetime short and safe?
13. Is a bulk update or delete being performed row by row?
14. Is raw SQL parameterized?
15. Is the code using asynchronous EF Core methods in server-side applications?

> **Good practice**: The fastest EF Core query is often the one that asks the database for exactly the rows and columns needed, once, using a plan that can use good indexes.

## Links

Advanced Performance Topics - EF Core: https://learn.microsoft.com/en-us/ef/core/performance/advanced-performance-topics

Performance Diagnosis - EF Core: https://learn.microsoft.com/en-us/ef/core/performance/performance-diagnosis

Efficient Querying - EF Core: https://learn.microsoft.com/en-us/ef/core/performance/efficient-querying

Statistics - SQL Server: https://learn.microsoft.com/en-us/sql/relational-databases/statistics/statistics

Display an Actual Execution Plan - SQL Server: https://learn.microsoft.com/en-us/sql/relational-databases/performance/display-an-actual-execution-plan

Tune Nonclustered Indexes with Missing Index Suggestions: https://learn.microsoft.com/en-us/sql/relational-databases/indexes/tune-nonclustered-missing-index-suggestions

Single vs. Split Queries - EF Core: https://learn.microsoft.com/en-us/ef/core/querying/single-split-queries

Tracking vs. No-Tracking Queries - EF Core: https://learn.microsoft.com/en-us/ef/core/querying/tracking

EF Core .Include and .AsNoTracking correct usage: https://stackoverflow.com/questions/78260162/ef-core-include-and-asnotracking-correct-usage

DbContext Lifetime, Configuration, and Initialization: https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/

Efficient Updating - EF Core: https://learn.microsoft.com/en-us/ef/core/performance/efficient-updating

SQL Queries - EF Core: https://learn.microsoft.com/en-us/ef/core/querying/sql-queries

Asynchronous Programming - EF Core: https://learn.microsoft.com/en-us/ef/core/miscellaneous/async
