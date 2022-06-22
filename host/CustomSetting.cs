namespace PhotoEditingService;

/// <summary>
///     Методы расширения ConfigureHostBuilder
/// </summary>
public static class CustomSetting
{
    /// <summary>
    ///     Использовать кастомный конфиг для приложения
    /// </summary>
    /// <param name="host">
    ///     Хост
    /// </param>
    public static void SetCustomSettings(this ConfigureHostBuilder host)
    {
        host.ConfigureAppConfiguration((context, configurationBuilder)
            => {
                configurationBuilder.AddJsonFile("custom.settings.json",
                    optional: true,
                    reloadOnChange: true);
               }
            );
    }
}