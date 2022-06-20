using Destructurama;
using Serilog;

namespace Revix.Core.Infrastructure.Common.Testing;

public class TestBase
{
    static TestBase()
    {
        // setup logging configuration
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Destructure.UsingAttributes()
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithEnvironmentName()
            .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
            // .WriteTo.File(Path.Combine(Directory.GetCurrentDirectory(), "Logs\\Log.txt"), rollingInterval: RollingInterval.Day, retainedFileCountLimit: 1)
            .CreateLogger();
    }
}