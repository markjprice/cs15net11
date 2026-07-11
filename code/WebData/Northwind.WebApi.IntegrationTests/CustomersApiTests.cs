using Microsoft.AspNetCore.Mvc.Testing; // To use WebApplicationFactory<T>.
using Northwind.EntityModels; // To use Customer.
using System.Net; // To use HttpStatusCode.

namespace Northwind.WebApi.IntegrationTests;

public sealed class CustomersApiTests :
  IClassFixture<WebApplicationFactory<Program>>
{
  private readonly HttpClient client;

  public CustomersApiTests(
    WebApplicationFactory<Program> factory)
  {
    client = factory.CreateClient();
  }

  [Fact]
  public async Task GetCustomer_ReturnsCustomer_WhenIdExists()
  {
    Customer? customer = await client.GetFromJsonAsync<Customer>(
      "/customers/ALFKI", TestContext.Current.CancellationToken);

    Assert.NotNull(customer);
    Assert.Equal("ALFKI", customer.CustomerId);
    Assert.Equal("Alfreds Futterkiste", customer.CompanyName);
  }

  [Fact]
  public async Task GetCustomer_ReturnsNotFound_WhenIdDoesNotExist()
  {
    using HttpResponseMessage response =
      await client.GetAsync("/customers/ZZZZZ", 
        TestContext.Current.CancellationToken);

    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
  }
}
