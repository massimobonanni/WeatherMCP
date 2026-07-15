namespace WeatherAPI.WeatherClient;

/// <summary>
/// Units of measurement accepted by the OpenWeatherMap API.
/// </summary>
public enum WeatherUnits
{
    /// <summary>Kelvin (API default when no units are specified).</summary>
    Standard,

    /// <summary>Celsius.</summary>
    Metric,

    /// <summary>Fahrenheit.</summary>
    Imperial
}
