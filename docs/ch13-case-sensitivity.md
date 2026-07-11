# Designing web services for case sensitivity

If you want to be able to do case-insensitive queries, then the most efficient solution is to enable case-insensitive text comparison for the Country column in the Customers table. Then, you could use uk or france or gErmAny in the queries.

If you cannot change the database, then you could force the country search value and Country column values to be uppercase or lowercase on both sides. But beware, because "while it may be tempting to use string.ToLower to force a case-insensitive comparison in a case-sensitive database, doing so may prevent your application from using indexes." You can read more about how to handle case-sensitivity in EF Core at the following link: https://learn.microsoft.com/en-us/ef/core/miscellaneous/collations-and-case-sensitivity.

Casing depends on need. For faster searches, you would use case-sensitive, which is why the Country column uses that in Microsoft's example Northwind database. What we are building at this point in the book is an API for code to call. An end user is never going to type a country name into the address bar of the browser, so you do not need to worry about incorrect casing. Instead, you would build an app or website UI that can make sure the user picks a country name that exists in the table column and has the correct casing when it makes the call to the API.

For case-insensitive searches using standard SQL features without losing the speed of indexed searches, you could store the original content in mixed/proper case for display, and also store a normalized version (for example, in all lowercase) in another column for searching/sorting/indexing, and convert the user's search input text into matching lowercase at runtime for comparison. This gives the best of both worlds at the expense of needing more storage space.

For a proper full-text, case-insensitive search on larger amounts of more varied text, such as a product description, you would implement full-text search (FTS) capabilities, for example, in SQL Server. Each database has its own FTS product.
