namespace Services.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Contracts;
using Newtonsoft.Json;
using Services.Dto;

/// <summary>
///     Контроллер получения состава пищевого продукта по изображению
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UploadImageController : ControllerBase
{
    private readonly ICompositionInformationService _compositionInformationService;
    private readonly IPhotoEditingService _photoEditingService;

    public UploadImageController(ICompositionInformationService compositionInformationService,
                                IPhotoEditingService photoEditingService)
    {
        _compositionInformationService = compositionInformationService;
        _photoEditingService = photoEditingService;
    }
    
    /// <summary>
    ///     Получить информацию по составу пищевого продукта с их вредностью/безопасностью по изображению
    /// </summary>
    /// <param name="file">
    ///     Изображение
    /// </param>
    /// <returns>
    ///     Информация по составу пищевого продукта с их вредностью/безопасностью
    /// </returns>
    [HttpPost("GetProductInformation")]
    public async Task<string> GetProductInformationAsync(IFormFile file)
    {
        using var ms = new MemoryStream();

        await file.CopyToAsync(ms);

        var searchText = await _photoEditingService.EditPhotoAsync(ms);

        var compositionProduct = await _compositionInformationService.GetCompositionInformationAsync(searchText);

        var result = new ResponseResult
        {
            Success = true,
            Data = compositionProduct
        };

        return JsonConvert.SerializeObject(result);
    }
}