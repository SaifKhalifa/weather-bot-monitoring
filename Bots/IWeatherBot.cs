namespace weather_bot_monitoring.Bots;

using weather_bot_monitoring.Models;

public interface IWeatherBot
{
    bool CheckAndActivate(WeatherData data);
}