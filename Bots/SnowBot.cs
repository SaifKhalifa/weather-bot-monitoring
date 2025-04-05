namespace weather_bot_monitoring.Bots
{
    using System;
    using weather_bot_monitoring.Models;

    public class SnowBot : IWeatherBot
    {
        private readonly double _temperatureThreshold;

        public SnowBot(double temperatureThreshold)
        {
            _temperatureThreshold = temperatureThreshold;
        }

        public bool CheckAndActivate(WeatherData data)
        {
            if (data.Temperature >= _temperatureThreshold)
            {
                Console.WriteLine("Temperature is pretty cold, SnowBot activated!\a");
                return true;
            }
            return false;
        }
    }
}