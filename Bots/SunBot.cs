namespace weather_bot_monitoring.Bots
{
    using System;
    using weather_bot_monitoring.Models;

    public class SunBot : IWeatherBot
    {
        private readonly double _temperatureThreshold;

        public SunBot(double temperatureThreshold)
        {
            _temperatureThreshold = temperatureThreshold;
        }

        public bool CheckAndActivate(WeatherData data)
        {
            if (data.Temperature >= _temperatureThreshold)
            {
                Console.WriteLine("Temperature is high, SunBot activated!\a");
                return true;
            }
            return false;
        }
    }
}