using weather_bot_monitoring.Models;

namespace weather_bot_monitoring.Bots
{
    public interface IWeatherBot
    {
        void CheckAndActivate(WeatherData data);
    }
}