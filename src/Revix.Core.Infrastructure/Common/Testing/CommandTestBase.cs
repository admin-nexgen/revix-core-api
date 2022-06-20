using System;
using Revix.Core.Infrastructure.Persistence;

namespace Revix.Core.Infrastructure.Common.Testing;

public class CommandTestBase : TestBase, IDisposable
{
    private bool _disposed;
        
    protected readonly ApplicationDbContext Context;

    protected CommandTestBase()
    {
        Context = InMemoryDbContextFactory.Create();
    }
        
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            InMemoryDbContextFactory.Destroy(Context);
        }
        _disposed = true;
    }
}