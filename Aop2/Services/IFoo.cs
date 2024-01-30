namespace Aop2.Services;

public interface IFoo
{
    public string Run();
 
    public Task<string> RunAsync();
}