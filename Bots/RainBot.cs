namespace weather_bot_monitoring.Bots
{
    using System;
    using weather_bot_monitoring.Models;

    public class RainBot : IWeatherBot
    {
        private readonly double _humidityThreshold;

        public RainBot(double humidityThreshold)
        {
            _humidityThreshold = humidityThreshold;
        }

        public void CheckAndActivate(WeatherData data)
        {
            if (data.Humidity >= _humidityThreshold)
            {
                Console.WriteLine("Humidity is high, RainBot activated!\a");
            }
        }
    }
}
