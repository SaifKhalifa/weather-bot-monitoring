using Xunit;
using weather_bot_monitoring.Bots;
using weather_bot_monitoring.Models;
using FluentAssertions;
using Xunit.Abstractions;

namespace weather_bot_monitoring_tests.Bots;

public class RainBotTests
{
    private readonly ITestOutputHelper _output;

    public RainBotTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData(70, 80, true)]   // Above threshold
    [InlineData(90, 60, false)]  // Below threshold
    [InlineData(75, 75, true)]   // Exactly at threshold
    public void CheckAndActivate_ShouldReturnExpectedResult(
            int humidityThreshold,
            int currentHumidity,
            bool expectedResult)
    {
        // Arrange
        string message = "Rain alert!";
        var rainBot = new RainBot(humidityThreshold, message);
        var weatherData = new WeatherData { Humidity = currentHumidity };

        _output.WriteLine("Test Case:");
        _output.WriteLine($"- Humidity Threshold: {humidityThreshold}");
        _output.WriteLine($"- Current Humidity: {currentHumidity}");
        _output.WriteLine($"- Expected Result: {expectedResult}");

        // Act
        bool result = rainBot.CheckAndActivate(weatherData);

        // Assert
        _output.WriteLine($"- Actual Result: {result}");
        result.Should().Be(expectedResult);
    }    
}
