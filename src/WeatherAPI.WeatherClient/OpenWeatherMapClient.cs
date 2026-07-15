using System.Globalization;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using WeatherAPI.WeatherClient.Models;

namespace WeatherAPI.WeatherClient;

/// <summary>
/// Implementation of <see cref="IOpenWeatherMapClient"/> that calls the OpenWeatherMap
/// "Current weather data" endpoint (https://openweathermap.org/current).
/// </summary>
public class OpenWeatherMapClient : IOpenWeatherMapClient
{
    private readonly HttpClient _httpClient;
    private readonly OpenWeatherMapClientOptions _options;

    public OpenWeatherMapClient(HttpClient httpClient, IOptions<OpenWeatherMapClientOptions> options)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        ArgumentNullException.ThrowIfNull(options);

        _httpClient = httpClient;
        _options = options.Value;

        if (string.IsNullOrWhiteSpace(_options.ApiKey))
        {
            throw new ArgumentException("An OpenWeatherMap API key must be configured.", nameof(options));
        }

        if (_httpClient.BaseAddress is null && !string.IsNullOrWhiteSpace(_options.BaseUrl))
        {
            _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        }
    }

    /// <inheritdoc />
    public async Task<CurrentWeatherResponse> GetCurrentWeatherAsync(
        double latitude,
        double longitude,
        WeatherUnits units = WeatherUnits.Standard,
        string? language = null,
        CancellationToken cancellationToken = default)
    {
        var requestUri = BuildRequestUri(latitude, longitude, units, language);

        using var response = await _httpClient.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var result = await response.Content
            .ReadFromJsonAsync<CurrentWeatherResponse>(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return result ?? throw new InvalidOperationException(
            "The OpenWeatherMap API returned an empty response for the current weather request.");
    }

    private string BuildRequestUri(double latitude, double longitude, WeatherUnits units, string? language)
    {
        var query = new List<string>
        {
            $"lat={latitude.ToString(CultureInfo.InvariantCulture)}",
            $"lon={longitude.ToString(CultureInfo.InvariantCulture)}",
            $"appid={Uri.EscapeDataString(_options.ApiKey)}"
        };

        if (units != WeatherUnits.Standard)
        {
            query.Add($"units={units.ToString().ToLowerInvariant()}");
        }

        if (!string.IsNullOrWhiteSpace(language))
        {
            query.Add($"lang={Uri.EscapeDataString(language)}");
        }

        return $"weather?{string.Join('&', query)}";
    }
}
