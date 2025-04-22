namespace weather_bot_monitoring.Parsers;

using weather_bot_monitoring.Models;
public interface IWeatherDataParser
{
    Task<WeatherData> ParseAsync(string input);
}