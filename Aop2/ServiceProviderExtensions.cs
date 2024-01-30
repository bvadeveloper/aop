using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Aop2;

internal static class ServiceProviderExtensions
{
    public static IServiceCollection AddSingletonDecorator<TIn, TOut>(this IServiceCollection collection)
        where TIn : class
        where TOut : class, TIn =>
        collection.AddSingleton<TIn>(provider => provider.Decorate<TIn, TOut>());

    private static TIn Decorate<TIn, TOut>(this IServiceProvider provider)
        where TIn : class
        where TOut : class, TIn
    {
        var activitySource = provider.GetRequiredService<ActivitySource>();
        var instance = ActivatorUtilities.CreateInstance<TOut>(provider);
        var proxy = DispatchProxyDecorator<TIn>.Decorate(instance, activitySource);

        return proxy;
    }
}