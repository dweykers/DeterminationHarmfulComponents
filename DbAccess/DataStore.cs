using DbAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DbAccess;

/// <summary>
///     Контекст хранилища бд
/// </summary>
public class DataStore : DbContext
{
    /// <summary>
    ///     Штрих-код продукта
    /// </summary>
    public DbSet<BarCode> Codes { get; set; }
    
    /// <summary>
    ///     Продукт
    /// </summary>
    public DbSet<Product> Products { get; set; }
    
    /// <summary>
    ///     Описание вредных компонентов продукта
    /// </summary>
    public DbSet<ProductComposition> ProductCompositions { get; set; }
    
    public DataStore(DbContextOptions<DataStore> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("product_information");
    }
}