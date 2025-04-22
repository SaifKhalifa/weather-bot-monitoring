namespace weather_bot_monitoring.Parsers.Factory;

interface IWeatherDataParserFactory
{
    IWeatherDataParser GetParser(string format);
}
