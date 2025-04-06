namespace weather_bot_monitoring.Parsers;
using weather_bot_monitoring.Models;
using System.Text.Json;

public class JsonWeatherDataParser : IWeatherDataParser
{
    public WeatherData Parse(string input)
    {
        return JsonSerializer.Deserialize<WeatherData>(input);
    }
}