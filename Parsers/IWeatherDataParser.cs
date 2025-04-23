namespace weather_bot_monitoring.Parsers;

using weather_bot_monitoring.Models;
public interface IWeatherDataParser
{
    WeatherData Parse(string input);
}