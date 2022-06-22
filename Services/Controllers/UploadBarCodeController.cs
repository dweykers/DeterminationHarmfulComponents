namespace Services.Controllers;

using Contracts;
using Services.Dto;
using Newtonsoft.Json;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/// <summary>
///     Контроллер получения состава пищевого продукта по штрих-коду
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UploadBarCodeController : ControllerBase
{
    private readonly IObtainingInformationBarCodeService _barCodeService;
    private readonly IProductInformationService _productInformationService;

    public UploadBarCodeController(IObtainingInformationBarCodeService barCodeService, 
                                   IProductInformationService productInformationService)
    {
        _barCodeService = barCodeService;
        _productInformationService = productInformationService;
    }
    
    /// <summary>
    ///     Получить информацию по составу пищевого продукта с их вредностью/безопасностью по штрих-коду
    /// </summary>
    /// <param name="barCode">
    ///     Штрих-код
    /// </param>
    /// <returns>
    ///     Информация по составу пищевого продукта с их вредностью/безопасностью
    /// </returns>
    [HttpPost]
    [Route("GetProductInformation")]
    public async Task<string> GetProductInformationAsync(IFormFile barCode)
    {
        using var ms = new MemoryStream();

        await barCode.CopyToAsync(ms);

        var barCodeResult = await _barCodeService.GetCompositionInformationAsync(ms);

        var productInformation = await _productInformationService.GetProductInformationAsync(barCodeResult);
        
        var result = new ResponseResult
        {
            Success = true,
            Data = productInformation
        };

        return JsonConvert.SerializeObject(result);
    }
    
    [HttpGet]
    [Route("Get")]
    public async Task<string> GetAsync()
    {
        return await Task.FromResult("hello");
    }
}