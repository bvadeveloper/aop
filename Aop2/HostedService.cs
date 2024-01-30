using Aop2.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Aop2;

public class HostedService : IHostedService
{
    private readonly IFoo _foo;
    private readonly ILogger<HostedService> _logger;

    public HostedService(IFoo foo, ILogger<HostedService> logger)
    {
        _foo = foo;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var result = _foo.Run();

        _logger.LogInformation(result);

        var resultAsync = await _foo.RunAsync();

        _logger.LogInformation(resultAsync);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}