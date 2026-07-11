using Microsoft.EntityFrameworkCore; // To use UseSqlite.
using Microsoft.Extensions.DependencyInjection; // To use IServiceCollection.

namespace Northwind.EntityModels;

public static class NorthwindDbExtensions
{
  /// <summary>
  /// Adds NorthwindDb to the specified IServiceCollection. Uses the Sqlite database provider.
  /// </summary>
  /// <param name="services">The service collection.</param>
  /// <param name="relativePath">Default is ".."</param>
  /// <param name="databaseName">Default is "Northwind.db"</param>
  /// <returns>An IServiceCollection that can be used to add more services.</returns>
  public static IServiceCollection AddNorthwindDb(
    this IServiceCollection services, // The type to extend.
    string relativePath = "..",
    string databaseName = "Northwind.db")
  {
    string path = Path.Combine(relativePath, databaseName);
    path = Path.GetFullPath(path);
    NorthwindDbLogger.WriteLine($"Database path: {path}");

    if (!File.Exists(path))
    {
      throw new FileNotFoundException(
        message: $"{path} not found.", fileName: path);
    }

    services.AddDbContextFactory<NorthwindDb>(options =>
    {
      // Data Source is the modern equivalent of Filename.
      options.UseSqlite($"Data Source={path}");

      options.LogTo(NorthwindDbLogger.WriteLine,
        new[] { Microsoft.EntityFrameworkCore
          .Diagnostics.RelationalEventId.CommandExecuting });
    });

    return services;
  }
}
