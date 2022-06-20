using System;
using Microsoft.EntityFrameworkCore;
using Moq;
using Revix.Core.Application.Common.Interfaces;
using Revix.Core.Domain.Entities;
using Revix.Core.Infrastructure.Persistence;

namespace Revix.Core.Infrastructure.Common.Testing;

public class InMemoryDbContextFactory
{
    public static ApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var mockUserService = new Mock<ICurrentUserService>();
        mockUserService.Setup(c => c.UserId).Returns("1");

        var mockDateTime = new Mock<IDateTime>();
            
        var context = new ApplicationDbContext(options, mockUserService.Object, mockDateTime.Object);

        context.Database.EnsureCreated();

        context.Cryptocurrencies.AddRange(new[] {
            new Cryptocurrency { Id = 1, Name = "Example 1" },
            new Cryptocurrency { Id = 2, Name = "Example 2" },
            new Cryptocurrency { Id = 3, Name = "Example 3"  },
            new Cryptocurrency { Id = 4, Name = "Example 4"  },
            new Cryptocurrency { Id = 5, Name = "Example 5"  },
            new Cryptocurrency { Id = 6, Name = "Example 6" },
        });
        

        context.SaveChanges();

        return context;
    }

    public static void Destroy(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();

        context.Dispose();
    }
}