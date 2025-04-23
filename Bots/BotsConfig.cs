namespace weather_bot_monitoring.Bots;
public class BotsConfig
{
    public RainBotConfig RainBot { get; set; }
    public SunBotConfig SunBot { get; set; }
    public SnowBotConfig SnowBot { get; set; }
}

public class RainBotConfig
{
    public bool Enabled { get; set; }
    public int HumidityThreshold { get; set; }
    public string Message { get; set; }
}

public class SunBotConfig
{
    public bool Enabled { get; set; }
    public int TemperatureThreshold { get; set; }
    public string Message { get; set; }
}

public class SnowBotConfig
{
    public bool Enabled { get; set; }
    public int TemperatureThreshold { get; set; }
    public string Message { get; set; }
}
