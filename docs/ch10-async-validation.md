- [Validating asynchronously with DataAnnotations](#validating-asynchronously-with-dataannotations)
- [Defining an asynchronous validation attribute](#defining-an-asynchronous-validation-attribute)
- [Invoking asynchronous validation](#invoking-asynchronous-validation)
- [Defining object-level asynchronous validation](#defining-object-level-asynchronous-validation)
- [Understanding the relationship with EF Core](#understanding-the-relationship-with-ef-core)


# Validating asynchronously with DataAnnotations

Validation attributes such as `[Required]`, `[StringLength]`, and `[RegularExpression]` perform local checks using values already in memory. They do not need to wait for a database, web service, filesystem, or other external resource.

Some validation rules require I/O. For example, an application might need to check whether:

* A customer ID already exists in a database.
* A VAT number is registered with a tax authority.
* A username is available from an identity service.
* A postal address can be verified by an external API.

Performing such checks synchronously would block a thread while the external operation completes. .NET 11 adds asynchronous validation to `System.ComponentModel.DataAnnotations`, allowing these checks to use `await` and accept a `CancellationToken`.

There are three main ways to define and invoke asynchronous validation:

| API                              | Use                                                                                                |
| -------------------------------- | -------------------------------------------------------------------------------------------------- |
| `AsyncValidationAttribute`       | Define an asynchronous rule for a property, field, or parameter                                    |
| `IAsyncValidatableObject`        | Define asynchronous validation for an entire object, including rules involving multiple properties |
| Asynchronous `Validator` methods | Explicitly validate an object, property, or value                                                  |

The new `Validator` methods include `ValidateObjectAsync`, `TryValidateObjectAsync`, `ValidatePropertyAsync`, `TryValidatePropertyAsync`, `ValidateValueAsync`, and `TryValidateValueAsync`. The `Validate` methods throw a `ValidationException` when validation fails. The `TryValidate` methods return a `bool` and can add failures to a collection of `ValidationResult` objects.

# Defining an asynchronous validation attribute

Suppose an application creates a Northwind customer. The customer ID must consist of five uppercase letters, and it must not already exist in the database.

The format can be checked synchronously:

```cs
[Required]
[StringLength(5, MinimumLength = 5)]
[RegularExpression("[A-Z]{5}")]
public string CustomerId { get; set; } = string.Empty;
```

Checking whether the ID is already in use requires a database query. First, define an abstraction for that query:

```cs
public interface ICustomerIdLookup
{
  Task<bool> IsAvailableAsync(
    string customerId,
    CancellationToken cancellationToken);
}
```

A server-side implementation of this interface could use EF Core’s `AnyAsync` method to search the `Customers` table. Keeping the database operation behind an interface prevents the validation attribute from depending directly on a particular EF Core context or database provider.

Next, derive an attribute from `AsyncValidationAttribute`:

```cs
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property)]
public sealed class CustomerIdAvailableAttribute
  : AsyncValidationAttribute
{
  protected override ValidationResult? IsValid(
    object? value,
    ValidationContext validationContext)
  {
    throw new InvalidOperationException(
      "Use an asynchronous Validator method.");
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

    ICustomerIdLookup lookup =
      (ICustomerIdLookup?)validationContext.GetService(
        typeof(ICustomerIdLookup))
      ?? throw new InvalidOperationException(
        "ICustomerIdLookup is not available.");

    bool isAvailable = await lookup.IsAvailableAsync(
      customerId,
      cancellationToken);

    return isAvailable
      ? ValidationResult.Success
      : new ValidationResult(
          "The customer ID is already in use.",
          [validationContext.MemberName!]);
  }
}
```

`ValidationContext` implements `IServiceProvider`, so the attribute can request a service rather than creating a database context itself. The application must supply a service provider that can resolve `ICustomerIdLookup`.

An `AsyncValidationAttribute` must also provide synchronous behavior because it inherits from `ValidationAttribute`. An attribute that supports only asynchronous validation can throw from its synchronous `IsValid` implementation, as shown in the preceding example. Code using such an attribute must call one of the asynchronous `Validator` methods.

Apply the new attribute to the model:

```cs
public sealed class NewCustomer
{
  [Required]
  [StringLength(5, MinimumLength = 5)]
  [RegularExpression("[A-Z]{5}")]
  [CustomerIdAvailable]
  public string CustomerId { get; set; } = string.Empty;

  [Required]
  [StringLength(40)]
  public string CompanyName { get; set; } = string.Empty;
}
```

The synchronous attributes reject an incorrectly formatted ID before the asynchronous database lookup is attempted. In the asynchronous validation pipeline, synchronous attributes run first. Async attributes for a value run only when its synchronous attributes have succeeded. ([GitHub][3])

# Invoking asynchronous validation

Create a `ValidationContext` using a service provider that can resolve the lookup service, and then call `TryValidateObjectAsync`:

```cs
NewCustomer customer = new()
{
  CustomerId = "ALFKI",
  CompanyName = "Alfreds Futterkiste"
};

List<ValidationResult> results = [];

ValidationContext context = new(
  customer,
  serviceProvider,
  items: null);

bool isValid = await Validator.TryValidateObjectAsync(
  customer,
  context,
  results,
  validateAllProperties: true,
  cancellationToken: cancellationToken);

if (!isValid)
{
  foreach (ValidationResult result in results)
  {
    WriteLine(result.ErrorMessage);
  }
}
```

Passing `validateAllProperties: true` causes the validation attributes on the immediate properties to be evaluated. Because a non-null results collection is supplied, all detected validation failures can be collected instead of stopping after the first one.

The cancellation token should normally come from the operation that initiated validation. In an ASP.NET Core app, for example, it could be the request cancellation token. This allows a database or remote API operation to stop when the client abandons the request.

# Defining object-level asynchronous validation

For a rule involving several properties, implement `IAsyncValidatableObject` instead of attaching an attribute to one property. Its `ValidateAsync` method returns an `IAsyncEnumerable<ValidationResult>`, allowing it to produce validation failures asynchronously.

`IAsyncValidatableObject` extends the existing `IValidatableObject` interface. An implementing class must therefore provide both `Validate` and `ValidateAsync`. The asynchronous `Validator` pipeline calls `ValidateAsync`; synchronous validation continues to call `Validate`.

Use an object-level rule when the validity of one value depends on another, such as checking that a delivery service operates in the specified shipping country.

# Understanding the relationship with EF Core

Asynchronous DataAnnotations validation is a .NET validation feature, not an EF Core model-building feature. An attribute such as `[Column]`, `[Key]`, or `[StringLength]` can affect how EF Core maps a type to a database. A custom attribute derived from `AsyncValidationAttribute` performs application validation but does not add a database constraint or alter the EF Core model.

Do not assume that `SaveChanges` or `SaveChangesAsync` automatically invokes asynchronous DataAnnotations validation. Validate the input explicitly before adding or updating an entity, or add an application-specific validation step around the save operation. EF Core has historically left this validation to the application rather than automatically running `Validator` during `SaveChanges`.

Async validators can run concurrently. Their implementations must therefore be thread-safe. In particular, do not share one `DbContext` instance between multiple concurrent validators because a context is designed for one short unit of work and does not support concurrent operations. A lookup service can create a fresh context for each operation or otherwise ensure that database access is not performed concurrently through the same context.

> **Good practice:** Use synchronous validation for rules that depend only on the submitted values. Use asynchronous validation only when a rule genuinely requires I/O. Always repeat authoritative validation on the server. An asynchronous uniqueness check can improve the error shown to a user, but it cannot prevent a race in which another request inserts the same value after the check. Keep primary keys, unique constraints, foreign keys, and check constraints in the database as the final protection for data integrity.

> **Prompt:** Please explain the difference between synchronous and asynchronous DataAnnotations validation. Show when to derive from `AsyncValidationAttribute`, when to implement `IAsyncValidatableObject`, and how to call `TryValidateObjectAsync`. Why must database constraints still enforce rules such as uniqueness?
