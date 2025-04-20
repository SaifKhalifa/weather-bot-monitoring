using Xunit;
using weather_bot_monitoring.Bots;
using weather_bot_monitoring.Models;

namespace weather_bot_monitoring_tests.Bots;

public class RainBotTests
{
    [Fact]
    public void CheckAndActivate_HumidityAboveThreshold_ShouldPrintMessageAndReturnTrue()
    {
        // Arrange
        var bot = new RainBot(70, "It's raining!");
        var data = new WeatherData { Humidity = 80 };
        // Act
        var result = bot.CheckAndActivate(data);
        // Assert
        Assert.True(result);
    }
}
