namespace Services;

using System.Text.RegularExpressions;

using Contracts;
using Tesseract;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

/// <inheritdoc/>
public class PhotoEditingService : IPhotoEditingService
{
    private readonly ILogger<PhotoEditingService> _logger;
    private readonly IConfiguration _configuration;

    public PhotoEditingService(ILogger<PhotoEditingService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }
    
    /// <inheritdoc/>
    public async Task<string[]> EditPhotoAsync(MemoryStream stream)
    {
        if (stream == null)
        {
            throw new ArgumentNullException("Ошибка обработки изображения");
        }
        
        var pix = Pix.LoadFromMemory(stream.ToArray());

        var text = await RecognizeTextAsync(pix);

        var compositions = await GetCompositionsAsync(text);

        return compositions;
    }

    /// <summary>
    ///     Распознать текст на картинке
    /// </summary>
    /// <param name="pix">
    ///     Изображение в формате Pix
    /// </param>
    /// <returns>
    ///     Распознаный текст
    /// </returns>
    private async Task<string?> RecognizeTextAsync(Pix pix)
    {
        var config = await GetConfigAsync();
        
        using var engine = new TesseractEngine(config.trainDataPath, config.language, EngineMode.TesseractAndLstm);

        var page = engine.Process(pix);

        var text = page.GetText();

        return await Task.FromResult(text);
    }

    /// <summary>
    ///     Получить состав пищевого продукта
    /// </summary>
    /// <param name="text">
    ///     Состав пищевого продукта строкой
    /// </param>
    private Task<string[]> GetCompositionsAsync(string text)
    {
        var compoundIndex = text.IndexOf("Состав", StringComparison.CurrentCulture);

        var compoundText = text[compoundIndex..];

        var match = Regex.Match(compoundText, @"[.]\s");

        var compoundTextFinally = compoundText[..match.Index];

        var productCompositions = Regex.Replace(compoundTextFinally, 
            @"Состав[(\w*)(\s*)]*:+", string.Empty);

        var productCompositionsSplit = productCompositions.Split(',');

        return Task.FromResult(productCompositionsSplit);
    }
    
    /// <summary>
    ///     Получить конфигурацию для нейронной сети
    /// </summary>
    private Task<(string language, string trainDataPath)> GetConfigAsync()
    {
        var config = _configuration.GetSection("Settings");

        var language = config["Language"] ?? throw new ArgumentNullException("Не установлен язык распознавания текста");
        
        var trainDataPath = config["TrainDataPath"] ?? throw new ArgumentNullException("Не указан путь до датасета");

        return Task.FromResult((language, trainDataPath));
    }
}