namespace Services;

using Newtonsoft.Json;

/// <summary>
///     Дто для десериализации ответа
/// </summary>
public class RelatedQuestionDto
{
    /// <summary>
    ///     Заголовок вопроса
    /// </summary>
    [JsonProperty("question")]
    public string? Question { get; set; }
        
    /// <summary>
    ///     Фрагмент ответа
    /// </summary>
    [JsonProperty("snippet")]
    public string? Snippet { get; set; }
}