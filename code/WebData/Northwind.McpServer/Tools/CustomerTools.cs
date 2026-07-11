using Microsoft.EntityFrameworkCore; // To use AsNoTracking, ToArrayAsync.
using ModelContextProtocol.Server; // To use [McpServerToolType] and [McpServerTool].
using Northwind.EntityModels; // To use NorthwindDb.
using System.ComponentModel; // To use [Description].

[McpServerToolType]
public class CustomerTools
{
  [McpServerTool]
  [Description(
    "Gets Northwind customers in a specified country.")]
  public async Task<CustomerSummary[]> GetCustomersByCountry(
    [Description("The country name, such as Germany or USA.")]
    NorthwindDb db,
    string country,
    CancellationToken cancellationToken)
  {
    return await db.Customers
      .AsNoTracking()
      .Where(customer => customer.Country == country)
      .OrderBy(customer => customer.CompanyName)
      .Select(customer => new CustomerSummary(
        customer.CustomerId,
        customer.CompanyName,
        customer.City,
        customer.Country))
      .ToArrayAsync(cancellationToken);
  }
}

public sealed record CustomerSummary(
  string CustomerId,
  string CompanyName,
  string? City,
  string? Country);
