using Microsoft.EntityFrameworkCore; // To use EF Core async methods.

namespace Northwind.Blazor.Services;

public class NorthwindServiceServerSide : INorthwindService
{
  private readonly IDbContextFactory<NorthwindDb> _contextFactory;

  public NorthwindServiceServerSide(
    IDbContextFactory<NorthwindDb> contextFactory)
  {
    _contextFactory = contextFactory;
  }

  public async Task<List<Customer>> GetCustomersAsync()
  {
    await using NorthwindDb db =
      await _contextFactory.CreateDbContextAsync();

    return await db.Customers
      .AsNoTracking()
      .ToListAsync();
  }

  public async Task<List<Customer>> GetCustomersAsync(string country)
  {
    await using NorthwindDb db =
      await _contextFactory.CreateDbContextAsync();

    return await db.Customers
      .AsNoTracking()
      .Where(c => c.Country == country)
      .ToListAsync();
  }

  public async Task<Customer?> GetCustomerAsync(string id)
  {
    await using NorthwindDb db =
      await _contextFactory.CreateDbContextAsync();

    return await db.Customers
      .AsNoTracking()
      .FirstOrDefaultAsync(c => c.CustomerId == id);
  }

  public async Task<Customer> CreateCustomerAsync(Customer c)
  {
    await using NorthwindDb db =
      await _contextFactory.CreateDbContextAsync();

    db.Customers.Add(c);
    await db.SaveChangesAsync();

    return c;
  }

  public async Task<Customer> UpdateCustomerAsync(Customer c)
  {
    await using NorthwindDb db =
          await _contextFactory.CreateDbContextAsync();

    db.Entry(c).State = EntityState.Modified;
    await db.SaveChangesAsync();

    return c;
  }

  public async Task DeleteCustomerAsync(string id)
  {
    await using NorthwindDb db =
          await _contextFactory.CreateDbContextAsync();

    Customer? customer = await db.Customers
      .FindAsync(id);

    if (customer == null)
    {
      return;
    }
    else
    {
      db.Customers.Remove(customer);
      await db.SaveChangesAsync();
    }
  }
}
