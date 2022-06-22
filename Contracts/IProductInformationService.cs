namespace Contracts;

/// <summary>
///     Сервис получения информации по пищевому продукту из хранилища бд по штрих-коду
/// </summary>
public interface IProductInformationService
{
    /// <summary>
    ///     Получить информацию по пищевому продукту из хранилища бд по штрих-коду
    /// </summary>
    /// <param name="barCode">
    ///     Номер штрих-кода
    /// </param>
    /// <returns>
    ///     Информация по пищевому продукту
    /// </returns>
    Task<string> GetProductInformationAsync(string barCode);
}