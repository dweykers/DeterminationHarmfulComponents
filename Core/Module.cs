namespace Core;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

/// <summary>
///     Класс базового модуля
/// </summary>
public abstract class BaseModule
{
    public abstract void Setup(IServiceCollection services, IConfiguration configuration);
}

/// <summary>
///     Расширения для IServiceCollection
/// </summary>
public static class ModuleExt
{
    public static void InstallModule<TModule>(this IServiceCollection services, IConfiguration configuration) where TModule : BaseModule, new()
    {
        var module = new TModule();
        module.Setup(services, configuration);
    }

    public static void RegisterService<TService, TImpl>(this IServiceCollection services) 
        where TService : class 
        where TImpl : class, TService 
        => services.AddScoped<TService, TImpl>();
}
