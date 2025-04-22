using System;
using weather_bot_monitoring.Parsers.Factory;

namespace weather_bot_monitoring.Parsers;

public class WeatherDataParserFactory : IWeatherDataParserFactory
{
    public IWeatherDataParser GetParser(string format)
    {
        if (format == "1")
        {
            return new JsonWeatherDataParser();
        }
        else if (format == "2")
        {
            return new XmlWeatherDataParser();
        }
        else
        {
            throw new Exception("Unsupported format. Use 1 for JSON or 2 for XML.");
        }
    }
}
