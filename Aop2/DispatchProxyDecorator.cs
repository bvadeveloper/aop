using System.Diagnostics;
using System.Reflection;
using OpenTelemetry.Trace;

namespace Aop2;

/// <summary>
/// T must be an interface
/// </summary>
/// <typeparam name="T"></typeparam>
public class DispatchProxyDecorator<T> : DispatchProxy where T : class
{
    private static ActivitySource _activitySource;

    private T Target { get; set; }
    
    public static T Decorate(T target, ActivitySource activitySource)
    {
        if (target == null) throw new ArgumentNullException(nameof(target));
        
        _activitySource = activitySource;

        var proxy = Create<T, DispatchProxyDecorator<T>>() as DispatchProxyDecorator<T>;
        proxy.Target = target;

        return proxy as T;
    }

    protected override object Invoke(MethodInfo targetMethod, object[] args)
    {
        var activityName = $"{targetMethod.DeclaringType.Name}.{targetMethod.Name}";
        
        using var activity = _activitySource.StartActivity(activityName);
        try
        {
            activity?.SetTag("args", args);
            
            return targetMethod.Invoke(Target, args);
        }
        catch (TargetInvocationException ex)
        {
            activity?.RecordException(ex);
            throw ex.InnerException ?? new TargetInvocationException(ex);
        }
    }
}