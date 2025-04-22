namespace weather_bot_monitoring.Parsers;
using System.Xml.Serialization;
using weather_bot_monitoring.Models;
public class XmlWeatherDataParser : IWeatherDataParser
{
    public Task<WeatherData> ParseAsync(string input)
    {
        var serializer = new XmlSerializer(typeof(WeatherData));
        using var reader = new StringReader(input);
        var result = (WeatherData)serializer.Deserialize(reader);
        return Task.FromResult(result);
    }
}
