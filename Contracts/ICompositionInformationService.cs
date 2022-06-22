namespace Contracts;

/// <summary>
///     Сервис поиска информации по запросу
/// </summary>
public interface ICompositionInformationService
{
    /// <summary>
    ///     Получить информацию по составу пищевого продукта с их вредностью/безопасностью
    /// </summary>
    /// <param name="searchesText">
    ///     Список исследуемых компонентов состава пищевого продукта
    /// </param>
    /// <returns>
    ///    Информация по составу пищевого продукта с их вредностью/безопасностью строкой
    /// </returns>
    Task<string> GetCompositionInformationAsync(IEnumerable<string> searchesText);
}