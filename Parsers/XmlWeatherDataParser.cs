namespace weather_bot_monitoring.Parsers;
using System.Xml.Serialization;
using weather_bot_monitoring.Models;
public class XmlWeatherDataParser : IWeatherDataParser
{
    public WeatherData Parse(string input)
    {
        var serializer = new XmlSerializer(typeof(WeatherData));
        using var reader = new StringReader(input);
        return (WeatherData)serializer.Deserialize(reader);
    }
}
