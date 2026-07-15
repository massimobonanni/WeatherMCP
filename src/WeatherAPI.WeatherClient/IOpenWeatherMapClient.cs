using WeatherAPI.WeatherClient.Models;

namespace WeatherAPI.WeatherClient;

/// <summary>
/// Client used to retrieve current weather data from the OpenWeatherMap API.
/// See https://openweathermap.org/current for the API reference.
/// </summary>
public interface IOpenWeatherMapClient
{
    /// <summary>
    /// Gets the current weather conditions for the given geographic coordinates.
    /// </summary>
    /// <param name="latitude">Latitude of the location.</param>
    /// <param name="longitude">Longitude of the location.</param>
    /// <param name="units">Units of measurement to use in the response.</param>
    /// <param name="language">Optional language code used to localize the response.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task<CurrentWeatherResponse> GetCurrentWeatherAsync(
        double latitude,
        double longitude,
        WeatherUnits units = WeatherUnits.Standard,
        string? language = null,
        CancellationToken cancellationToken = default);
}
