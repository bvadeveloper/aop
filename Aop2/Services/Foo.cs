using Microsoft.Extensions.Logging;

namespace Aop2.Services;

public class Foo : IFoo
{
    private readonly ILogger _logger;

    public Foo(ILogger<Foo> logger)
    {
        _logger = logger;
    }
    
    public string Run()
    {
        _logger.LogInformation("foo");
        return nameof(Foo.Run);
    }

    public async Task<string> RunAsync()
    {
        await Task.Yield();
        _logger.LogInformation("foo async");
        
        return nameof(Foo.RunAsync);
    }
}