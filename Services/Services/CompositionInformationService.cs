namespace Services;

using System.Collections;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SerpApi;
using Contracts;

/// <inheritdoc/>
public class CompositionInformationService : ICompositionInformationService
{
    private readonly ILogger<PhotoEditingService> _logger;
    private readonly IConfiguration _configuration;

    public CompositionInformationService(ILogger<PhotoEditingService> logger,
                                         IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    /// <inheritdoc/>
    public async Task<string> GetCompositionInformationAsync(IEnumerable<string> searchesText)
    {
        var config = _configuration.GetSection("Settings");

        var apiKey = config["ApiKey"] ?? throw new ArgumentNullException("Не установлен ключ для Azure Bing Api");

        var listResult = new List<string>();

        await Parallel.ForEachAsync(searchesText, (searchText, token) =>
        {
            var hashtable = new Hashtable
            {
                {"q", searchText},
                {"hl", "ru"},
                {"google_domain", "google.com"},
                {"num", "2"}
            };

            var search = new GoogleSearch(hashtable, apiKey);

            var jsonObject = search.GetJson();
            
            var results = jsonObject["related_questions"]?.Children().ToList();

            if (results != null)
            {
                var data = results[0]?.ToObject<RelatedQuestionDto>();
                
                listResult.Add(data?.Snippet);
            }

            return default;
        });

        var result = string.Join($"{Environment.NewLine}{Environment.NewLine}", listResult) + ".";

        return result;
    }
}