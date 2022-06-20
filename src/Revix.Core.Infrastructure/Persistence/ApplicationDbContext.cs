using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Revix.Core.Application.Common.Interfaces;
using Revix.Core.Domain.Common;
using Revix.Core.Domain.Entities;

namespace Revix.Core.Infrastructure.Persistence;

public class ApplicationDbContext: DbContext, IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserService currentUserService,
        IDateTime dateTime) 
        : base(options)
    {
        _currentUserService = currentUserService;
        _dateTime = dateTime;
    }

    public bool IsInMemoryDb => Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";

    public DbSet<Cryptocurrency> Cryptocurrencies { get; set; }
    
    public DbSet<Currency> Currencies { get; set; }
    
    public DbSet<Quote> Quotes { get; set; }
    
    public DbSet<Platform> Platforms { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                    entry.Entity.Created = _dateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.LastModified = _dateTime.Now;
                    break;
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        base.OnModelCreating(builder);
    }

}