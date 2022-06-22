namespace Services;

using Contracts;
using DbAccess;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

/// <inheritdoc/>
public class ProductInformationService : IProductInformationService
{
    private readonly DataStore _dataStore;
    private readonly ILogger<ProductInformationService> _logger;

    public ProductInformationService(DataStore dataStore, 
                                     ILogger<ProductInformationService> logger)
    {
        _dataStore = dataStore;
        _logger = logger;
    }
    
    /// <inheritdoc/>
    public async Task<string> GetProductInformationAsync(string barCode)
    {
        if (string.IsNullOrEmpty(barCode))
        {
            throw new ArgumentNullException("Ошибка получения информации о составе пищевого продукта из базы данных");
        }
        
        var barCodeId = await _dataStore.Codes.Where(x => x.BarCodeNumber == barCode)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();

        if (barCodeId == Guid.Empty)
        {
            return string.Empty;
        }

        var productInformation = await _dataStore.Products.Where(x => x.BarCode.Id == barCodeId)
            .SelectMany(x => x.ProductCompositions)
            .Select(x => $"{x.ProductCompositionName} - {x.HazardDescription}")
            .ToListAsync();

        var result = string.Join($"{Environment.NewLine}{Environment.NewLine}", productInformation) + ".";

        return result;
    }
}