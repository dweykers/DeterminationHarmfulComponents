namespace DbAccess
{
    using System;
    
    using Core;
    
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    ///     Модуль бд
    /// </summary>
    public class DbAccessModule : BaseModule
    {
        public override void Setup(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<DataStore>(x =>
            {
                x.UseLazyLoadingProxies();
                x.EnableSensitiveDataLogging();
                x.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    z =>
                    {
                        z.MigrationsHistoryTable("__EFMigrationsHistory", "product_information");
                    });
            });
        }
    }
}