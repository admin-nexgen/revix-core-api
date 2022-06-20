using System.Threading.Tasks;

namespace Revix.Core.Infrastructure.Persistence;

public static class ApplicationInMemoryDbContextSeed
{
    /// <summary>
    /// Seed data if required
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static Task SeedDataAsync(ApplicationDbContext context)
    {
        return Task.CompletedTask;
    }
}