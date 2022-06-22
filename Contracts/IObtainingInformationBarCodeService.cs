namespace Contracts;

/// <summary>
///     Сервис получения номера штрих-кода с изображения
/// </summary>
public interface IObtainingInformationBarCodeService
{
    /// <summary>
    ///     Получить информацию с штрих-кода изображения
    /// </summary>
    /// <param name="stream">
    ///     (Изображение) с штрих-кодом
    /// </param>
    /// <returns>
    ///     Штрих-код строкой
    /// </returns>
    Task<string?> GetCompositionInformationAsync(MemoryStream stream);
}