using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Xml;
using weather_bot_monitoring.Parsers;
using Xunit.Abstractions;

namespace weather_bot_monitoring_tests.Parsers;

public class XmlWeatherDataParserTests
{
    private readonly ITestOutputHelper _output;

    public XmlWeatherDataParserTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Parse_WithValidXML_ShouldReturnCorrectWeatherData()
    {
        // Arrange
        var xml = "<WeatherData><Humidity>55</Humidity><Temperature>25</Temperature><Location>Nablus</Location></WeatherData>";
        var parser = new XmlWeatherDataParser();

        // Act
        var result = parser.Parse(xml);

        _output.WriteLine("Parsed result:");
        _output.WriteLine($"Humidity: {result.Humidity}");
        _output.WriteLine($"Temperature: {result.Temperature}");
        _output.WriteLine($"Location: {result.Location}");

        // Assert
        result.Should().NotBeNull();
        result.Humidity.Should().Be(55);
        result.Temperature.Should().Be(25);
    }

    [Fact]
    public void Parse_WithInvalidXML_ShouldThrowJsonException()
    {
        // Arrange
        var invalidXml = "<WeatherData><Humidty>55</Humidity>";
        var parser = new XmlWeatherDataParser();

        // Act
        Action act = () => parser.Parse(invalidXml);

        // Assert
        act.Should()
               .Throw<InvalidOperationException>()
               .WithInnerException<XmlException>();
    }
}
