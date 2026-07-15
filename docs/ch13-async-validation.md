- [Validating Minimal API requests asynchronously](#validating-minimal-api-requests-asynchronously)
- [Defining a request DTO](#defining-a-request-dto)
- [Defining an asynchronous validation attribute](#defining-an-asynchronous-validation-attribute)
- [Using the request DTO in the endpoint](#using-the-request-dto-in-the-endpoint)
- [Testing asynchronous validation](#testing-asynchronous-validation)
- [Validating an entire object asynchronously](#validating-an-entire-object-asynchronously)


# Validating Minimal API requests asynchronously

Validation attributes such as `[Required]`, `[StringLength]`, and `[Range]` evaluate values that are already in memory. They are synchronous because they do not need to wait for another system.

Some validation rules require input/output, or I/O. For example, an API might need to check whether:

* A customer ID is already stored in a database.
* An email address has already been registered.
* A product code exists in another service.
* A delivery address is supported by a shipping provider.

Making an endpoint handler asynchronous does not make its validation asynchronous. The validation rule itself must support `await`.

Minimal API validation supports two asynchronous DataAnnotations APIs:

* `AsyncValidationAttribute` defines an asynchronous rule for a parameter, type, or property.
* `IAsyncValidatableObject` defines asynchronous rules for an entire object, including rules involving several properties.

When `AddValidation` is registered, `Microsoft.Extensions.Validation` runs these validators before invoking the endpoint. If validation fails, ASP.NET Core returns `400 Bad Request`, and the endpoint handler does not execute. The request cancellation token is passed to the validator so that database or remote-service calls can stop if the client abandons the request.

Let’s add an asynchronous check that prevents the customer API from accepting an ID that is already in use.

# Defining a request DTO

The existing POST endpoint accepts a `Customer` entity directly. A real API should normally accept a request DTO containing only properties that callers are permitted to supply.

1. In the `Northwind.WebApi` project, add a class file named `CreateCustomerRequest.cs`.

2. Replace its contents with the following code:

```cs
using System.ComponentModel.DataAnnotations;

public sealed class CreateCustomerRequest
{
  [Required]
  [StringLength(5, MinimumLength = 5)]
  [RegularExpression("^[A-Za-z]{5}$")]
  [CustomerIdAvailable]
  public string CustomerId { get; init; } = string.Empty;

  [Required]
  [StringLength(40)]
  public string CompanyName { get; init; } = string.Empty;

  public string? ContactName { get; init; }

  public string? ContactTitle { get; init; }

  public string? Address { get; init; }

  public string? City { get; init; }

  public string? Region { get; init; }

  public string? PostalCode { get; init; }

  public string? Country { get; init; }

  public string? Phone { get; init; }

  public string? Fax { get; init; }
}
```

The first three attributes validate the format of `CustomerId` synchronously. `[CustomerIdAvailable]` will query the database asynchronously.

Running inexpensive synchronous checks first avoids making a database query for an empty or incorrectly formatted ID.

# Defining an asynchronous validation attribute

3. Add a class file named `CustomerIdAvailableAttribute.cs`.

4. Replace its contents with the following code:

```cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Northwind.EntityModels;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(
  AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class CustomerIdAvailableAttribute
  : AsyncValidationAttribute
{
  protected override ValidationResult? IsValid(
    object? value,
    ValidationContext validationContext)
  {
    throw new InvalidOperationException(
      "Use asynchronous validation for CustomerIdAvailable.");
  }

  protected override async Task<ValidationResult?> IsValidAsync(
    object? value,
    ValidationContext validationContext,
    CancellationToken cancellationToken)
  {
    if (value is not string customerId ||
      string.IsNullOrWhiteSpace(customerId))
    {
      return ValidationResult.Success;
    }

    IDbContextFactory<NorthwindDb> contextFactory =
      validationContext
        .GetRequiredService<IDbContextFactory<NorthwindDb>>();

    await using NorthwindDb db =
      await contextFactory.CreateDbContextAsync(
        cancellationToken);

    string normalizedId = customerId.ToUpperInvariant();

    bool alreadyExists = await db.Customers
      .AsNoTracking()
      .AnyAsync(
        customer => customer.CustomerId == normalizedId,
        cancellationToken);

    if (alreadyExists)
    {
      return new ValidationResult(
        $"Customer ID {normalizedId} is already in use.",
        [nameof(CreateCustomerRequest.CustomerId)]);
    }

    return ValidationResult.Success;
  }
}
```

`ValidationContext` can resolve services from the application’s dependency-injection container. The validator obtains the database-context factory that was registered by `AddNorthwindDb`, creates a context for this operation, and disposes it when the query finishes.

The attribute must implement both synchronous and asynchronous methods because `AsyncValidationAttribute` also participates in the existing DataAnnotations design. Minimal API validation always calls its asynchronous path. If the rule has no meaningful synchronous implementation, the synchronous method can throw `InvalidOperationException`, as in this example.

Using `IDbContextFactory<NorthwindDb>` is preferable to retaining or sharing a database context. Async validators can run concurrently, and an EF Core database context does not support concurrent operations.

# Using the request DTO in the endpoint

5. Confirm that validation is registered before the call to `Build`:

```cs
builder.Services.AddValidation();
```

6. In `WebApplicationExtensions.cs`, replace the existing POST endpoint with the following implementation:

```cs
// POST: /customers
// BODY: CreateCustomerRequest (JSON)
app.MapPost(pattern: "/customers", handler:
  async Task<IResult> (
    CreateCustomerRequest request,
    NorthwindDb db) =>
{
  Customer customer = new()
  {
    CustomerId = request.CustomerId.ToUpperInvariant(),
    CompanyName = request.CompanyName,
    ContactName = request.ContactName,
    ContactTitle = request.ContactTitle,
    Address = request.Address,
    City = request.City,
    Region = request.Region,
    PostalCode = request.PostalCode,
    Country = request.Country,
    Phone = request.Phone,
    Fax = request.Fax
  };

  await db.Customers.AddAsync(customer);

  int affected = await db.SaveChangesAsync();

  if (affected == 1)
  {
    return TypedResults.Created(
      $"/customers/{customer.CustomerId}",
      customer);
  }

  return TypedResults.BadRequest(
    "Failed to create customer.");
});
```

No validation call appears inside the handler. ASP.NET Core validates the `CreateCustomerRequest` parameter before invoking it.

If the ID is missing or has the wrong format, the synchronous attributes produce the validation failure. If the format is valid, ASP.NET Core awaits `CustomerIdAvailableAttribute`. The endpoint executes only when all validation succeeds.

# Testing asynchronous validation

7. In `create-customer.http`, add the following request:

```http
### Try to create a customer using an existing ID.
POST {{base_address}}
Content-Type: application/json

{
  "customerId": "ALFKI",
  "companyName": "Another Alfreds",
  "country": "Germany"
}
```

8. Start `Northwind.WebApi`, and then send the request.

The validation system queries the database, discovers that `ALFKI` already exists, and returns a `400 Bad Request` response containing a validation error similar to the following:

```json
{
  "errors": {
    "CustomerId": [
      "Customer ID ALFKI is already in use."
    ]
  }
}
```

The POST endpoint is not invoked, so EF Core does not attempt to insert the duplicate customer.

# Validating an entire object asynchronously

Use `IAsyncValidatableObject` when a rule applies to an entire request rather than one property. For example, an order request might need to check that a product is available in the requested quantity and can be shipped to the supplied country.

The interface defines `ValidateAsync`, which returns an `IAsyncEnumerable<ValidationResult>`. Because it extends `IValidatableObject`, the type must also implement the synchronous `Validate` method. A type that supports only asynchronous validation can make its synchronous method throw `InvalidOperationException`.

`AsyncValidationAttribute` is usually the clearer choice for one-property rules. `IAsyncValidatableObject` is better for rules involving several related values. ([GitHub][1])

> **Good practice:** Use asynchronous validation only for rules that genuinely require I/O. Use ordinary validation attributes for checks involving values already in memory. Keep async validators small, pass cancellation tokens to every awaited operation, and avoid sharing non-thread-safe services such as an EF Core database context between validators.

> **Good practice:** An availability check does not replace a database constraint. Another request could insert the same customer ID between the validation query and `SaveChangesAsync`. Keep the primary key or unique constraint in the database and handle a resulting database conflict appropriately.
