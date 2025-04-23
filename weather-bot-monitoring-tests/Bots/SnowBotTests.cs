using FluentAssertions;
using weather_bot_monitoring.Bots;
using weather_bot_monitoring.Models;
using Xunit.Abstractions;

namespace weather_bot_monitoring_tests.Bots;

public class SnowBotTests
{
    private readonly ITestOutputHelper _output;
    public SnowBotTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData(0, 2, false)]   // Above threshold
    [InlineData(0, -2, true)]  // Below threshold
    [InlineData(0, 0, true)]   // Exactly at threshold
    public void CheckAndActivate_ShouldReturnExpectedResult(
        int temperatureThreshold,
        int currentTemp,
        bool expectedResult)
    {
        // Arrange
        string _message = "It's snowing!";
        var _snowBot = new SnowBot(temperatureThreshold, _message);
        var _weatherData = new WeatherData { Temperature = currentTemp };

        _output.WriteLine($"Test Case:");
        _output.WriteLine($"- Temperature Threshold: {temperatureThreshold}");
        _output.WriteLine($"- Current Temp: {currentTemp}");
        _output.WriteLine($"- Expected Result: {expectedResult}");

        // Act
        bool result = _snowBot.CheckAndActivate(_weatherData);

        // Assert
        _output.WriteLine($"- Actual Result: {result}");
        result.Should().Be(expectedResult);
    }
}
