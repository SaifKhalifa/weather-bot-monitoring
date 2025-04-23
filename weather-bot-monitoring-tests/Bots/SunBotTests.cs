using FluentAssertions;
using weather_bot_monitoring.Bots;
using weather_bot_monitoring.Models;
using Xunit.Abstractions;

namespace weather_bot_monitoring_tests.Bots;

public class SunBotTests
{
    private readonly ITestOutputHelper _output;

    public SunBotTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData(30, 35, true)]   // Above threshold
    [InlineData(30, 25, false)]  // Below threshold
    [InlineData(30, 30, true)]   // Exactly at threshold
    public void CheckAndActivate_ShouldReturnExpectedValue(
        int temperatureThreshold,
        int currentTemp,
        bool expectedResult)
    {
        // arrange
        string _message = "It's sunny!";
        var _sunBot = new SunBot(temperatureThreshold, _message);
        var _weatherData = new WeatherData { Temperature = currentTemp };

        _output.WriteLine($"Test Case:");
        _output.WriteLine($"- Temperature Threshold: {temperatureThreshold}");
        _output.WriteLine($"- Current Temp: {currentTemp}");
        _output.WriteLine($"- Expected Result: {expectedResult}");

        // act
        bool result = _sunBot.CheckAndActivate(_weatherData);

        // assert
        _output.WriteLine($"- Actual Result: {result}");
        result.Should().Be(expectedResult);
    }
}
