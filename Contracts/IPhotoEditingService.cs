namespace Contracts;

/// <summary>
///     Сервис обработки Изображения
/// </summary>
public interface IPhotoEditingService
{
    /// <summary>
    ///     Обработать фотографию
    /// </summary>
    /// <param name="stream">
    ///    (Изображение) Последовательность байтов
    /// </param>
    /// <returns>
    ///     Список компонентов исследуемого продукта
    /// </returns>
    Task<string[]> EditPhotoAsync(MemoryStream stream);
}