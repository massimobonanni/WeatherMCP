namespace WeatherAPI.WeatherClient;

/// <summary>
/// Configuration options for <see cref="OpenWeatherMapClient"/>.
/// </summary>
public class OpenWeatherMapClientOptions
{
    /// <summary>
    /// Your unique OpenWeatherMap API key (see the "API key" tab on your account page).
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// Base address of the OpenWeatherMap API.
    /// </summary>
    public string BaseUrl { get; set; } = "https://api.openweathermap.org/data/2.5/";
}
