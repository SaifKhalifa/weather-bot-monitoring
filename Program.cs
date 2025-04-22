namespace weather_bot_monitoring;

using System.Text.Json;
using weather_bot_monitoring.Bots;
using weather_bot_monitoring.Models;
using weather_bot_monitoring.Parsers;

internal static class Program
{
#if false
    static void Main(string[] args)
    {
        Console.WriteLine("=== Weather Bot Monitoring ===");

        // Ask for format
        string format;
        while (true)
        {
            Console.WriteLine("Choose format: (1) JSON, (2) XML");
            format = Console.ReadLine()?.Trim();
            if (format == "1" || format == "2") break;
            Console.WriteLine("Invalid input. Please enter 1 or 2.");
        }

        // Ask for weather data input (string or file path)
        Console.WriteLine("Paste weather data string or file path:");
        string input = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Input cannot be empty.");
            return;
        }

        string dataString;
        if (File.Exists(input))
        {
            try
            {
                dataString = File.ReadAllText(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return;
            }
        }
        else
        {
            dataString = input;
        }

        // Choose parser
        IWeatherDataParser parser = format == "1"
            ? new JsonWeatherDataParser()
            : new XmlWeatherDataParser();

        // Parse weather data
        WeatherData data;
        try
        {
            data = parser.Parse(dataString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing data: {ex.Message}");
            return;
        }

        // Load bot config from JSON file
        BotsConfig config;
        try
        {
            string configContent = File.ReadAllText("WeatherBotsConfig.json");
            config = JsonSerializer.Deserialize<BotsConfig>(configContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            if (config == null)
            {
                Console.WriteLine("Config is null after deserialization. Please check the JSON format.");
                return;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load bot config: {ex.Message}");
            return;
        }        

        // Set up bots
        var bots = new List<IWeatherBot>();        

        if (config.RainBot?.Enabled == true)
            bots.Add(new RainBot(config.RainBot.HumidityThreshold, config.RainBot.Message));
        if (config.SunBot?.Enabled == true)
            bots.Add(new SunBot(config.SunBot.TemperatureThreshold, config.SunBot.Message));
        if (config.SnowBot?.Enabled == true)
            bots.Add(new SnowBot(config.SnowBot.TemperatureThreshold, config.SnowBot.Message));        

        // Run bots
        bool anyBotActivated = bots.Any(bot => bot.CheckAndActivate(data));

        if (!anyBotActivated)
        {
            Console.WriteLine("No bot was activated.");
        }
        else
        {
            Console.WriteLine("At least one bot was activated.");
        }
    }
#endif
}