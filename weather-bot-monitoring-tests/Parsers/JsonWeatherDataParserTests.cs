using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using weather_bot_monitoring.Parsers;
using Xunit.Abstractions;

namespace weather_bot_monitoring_tests.Parsers;

public class JsonWeatherDataParserTests
{
    private readonly ITestOutputHelper _output;

    public JsonWeatherDataParserTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Parse_WithValidJson_ShouldReturnCorrectWeatherData()
    {
        // Arrange
        var json = "{\"Humidity\": 55, \"Temperature\" : 25, \"Location\": \"Nablus\"}";
        var parser = new JsonWeatherDataParser();

        // Act
        var result = parser.Parse(json);

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
    public void Parse_WithInvalidJson_ShouldThrowJsonException()
    {
        // Arrange
        var invalidJson = "{ qwerty }";
        var parser = new JsonWeatherDataParser();

        // Act
        Action act = () => parser.Parse(invalidJson);

        // Assert
        act.Should().Throw<JsonException>();
    }
}
