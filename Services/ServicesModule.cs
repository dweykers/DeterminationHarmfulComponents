namespace Services;

using Contracts;
using Core;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Сервис зависимостей
/// </summary>
public class ServicesModule : BaseModule
{
    /// <summary>
    ///     Установить зависимости
    /// </summary>
    public override void Setup(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPhotoEditingService, PhotoEditingService>();
        services.AddScoped<ICompositionInformationService, CompositionInformationService>();
        services.AddScoped<IObtainingInformationBarCodeService, ObtainingInformationBarCodeService>();
        services.AddScoped<IProductInformationService, ProductInformationService>();
        services.AddMvc().AddApplicationPart(GetType().Assembly).AddControllersAsServices();
    }
}