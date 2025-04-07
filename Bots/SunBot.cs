namespace weather_bot_monitoring.Bots;

using System;
using weather_bot_monitoring.Models;

public class SunBot : IWeatherBot
{
    private readonly int _temperatureThreshold;
    private readonly string _message;

    public SunBot(int temperatureThreshold, string message)
    {
        _temperatureThreshold = temperatureThreshold;
        _message = message;
    }

    public bool CheckAndActivate(WeatherData data)
    {
        if (data.Temperature >= _temperatureThreshold)
        {
            Console.WriteLine(_message);
            return true;
        }
        return false;
    }
}