namespace weather_bot_monitoring.Parsers;
using weather_bot_monitoring.Models;
using System.Text.Json;

public class JsonWeatherDataParser : IWeatherDataParser
{
    public Task<WeatherData> ParseAsync(string input)
    {
        var result = JsonSerializer.Deserialize<WeatherData>(input);
        return Task.FromResult(result);
    }
}