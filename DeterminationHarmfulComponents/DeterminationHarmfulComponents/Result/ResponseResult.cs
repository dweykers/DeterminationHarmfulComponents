namespace DeterminationHarmfulComponents.Result;

/// <summary>
///     Ответ запроса
/// </summary>
public class ResponseResult
{
    /// <summary>
    ///     Признак успешного/неуспешного ответа запроса
    /// </summary>
    public bool? Success { get; set; }
    
    /// <summary>
    ///     Данные
    /// </summary>
    public string? Data { get; set; }
}