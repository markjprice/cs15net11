using Microsoft.EntityFrameworkCore; // To use UseSqlite.
using Microsoft.Extensions.DependencyInjection; // To use IServiceCollection.

namespace Northwind.EntityModels;

public static class NorthwindDbExtensions
{
  private static string FindFileInCurrentOrParentDirectories(
    string fileName,
    string? startingDirectory = null)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(fileName);

    string start = Path.GetFullPath(
      startingDirectory ?? AppContext.BaseDirectory);

    if (!Directory.Exists(start))
    {
      throw new DirectoryNotFoundException(
        $"The starting directory does not exist: {start}");
    }

    DirectoryInfo? directory = new(start);

    while (directory is not null)
    {
      string candidate = Path.Combine(directory.FullName, fileName);

      if (File.Exists(candidate))
      {
        return candidate;
      }

      directory = directory.Parent;
    }

    throw new FileNotFoundException(
      $"Could not find '{fileName}' in '{start}' " +
      "or any of its parent directories.",
      fileName);
  }

  /// <summary>
  /// Adds NorthwindDb to the specified IServiceCollection. Uses the Sqlite database provider.
  /// </summary>
  /// <param name="services">The service collection.</param>
  /// <param name="databaseName">Default is "Northwind.db"</param>
  /// <returns>An IServiceCollection that can be used to add more services.</returns>
  public static IServiceCollection AddNorthwindDb(
    this IServiceCollection services, // The type to extend.
    string databaseName = "Northwind.db")
  {
    string path = 
      FindFileInCurrentOrParentDirectories(databaseName);

    try
    {
      NorthwindDbLogger.WriteLine($"Database path: {path}");
    }
    catch (Exception ex)
    {
      WriteLine(ex.Message);
    }

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
        [ Microsoft.EntityFrameworkCore
          .Diagnostics.RelationalEventId.CommandExecuting ]);
    });

    return services;
  }
}
