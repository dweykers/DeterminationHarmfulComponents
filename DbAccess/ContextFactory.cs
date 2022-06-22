namespace DbAccess;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

/// <summary>
///     Управление схемой БД и миграциями в дизайн-тайме
/// </summary>
public class ContextFactory : IDesignTimeDbContextFactory<DataStore>
{
    public DataStore CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataStore>();

        optionsBuilder.UseNpgsql("Host=localhost;Database=product_data;Username=postgres;Password=159357");
        
        return new DataStore(optionsBuilder.Options);
    }
}