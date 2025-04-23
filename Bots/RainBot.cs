namespace weather_bot_monitoring.Bots;

using System;
using weather_bot_monitoring.Models;

public class RainBot : IWeatherBot
{
    private readonly int _humidityThreshold;
    private readonly string _message;

    public RainBot(int humidityThreshold, string message)
    {
        _humidityThreshold = humidityThreshold;
        _message = message;
    }

    public bool CheckAndActivate(WeatherData data)
    {
        if (data.Humidity >= _humidityThreshold)
        {
            Console.WriteLine(_message);
            return true;
        }
        return false;
    }
}