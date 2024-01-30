using System.Diagnostics;
using Aop2;
using Aop2.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.AddConsole();

builder.Services
    .AddSingleton(new ActivitySource("Aop", "0.0.1"))
    .AddHostedService<HostedService>();

builder.Services.AddSingletonDecorator<IFoo, Foo>();

await builder.Build().RunAsync();