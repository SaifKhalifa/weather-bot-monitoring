namespace weather_bot_monitoring;

using System.Text.Json;
using weather_bot_monitoring.Bots;
using weather_bot_monitoring.Models;
using weather_bot_monitoring.Parsers;

internal static class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== Weather Bot Monitoring ===");

        // Ask for format
        string format = GetFormat();

        // Ask for weather data input (string or file path)
        string? dataString = await GetWeatherDataInputAsync();
        if (dataString == null) return;

        var parser = GetParser(format);
        if (parser == null) return;

        // async parsing
        WeatherData data;
        try
        {
            data = await parser.ParseAsync(dataString);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Parsing failed: " + ex.Message);
            return;
        }


        // Load bot config from JSON file
        BotsConfig config;
        try
        {
            string configContent = await File.ReadAllTextAsync("WeatherBotsConfig.json");
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
        var activatedBots = bots
            .Where(bot => bot.CheckAndActivate(data))
            .Select(bot => bot.GetType().Name)
            .ToList();

        if (!activatedBots.Any())
        {
            Console.WriteLine("No bot was activated.");
        }
        else
        {
            Console.WriteLine("Activated bot(s): " + string.Join(", ", activatedBots));
        }

    }

    static string GetFormat()
    {
        string format;
        while (true)
        {
            Console.WriteLine("Choose format: (1) JSON, (2) XML");
            format = Console.ReadLine()?.Trim();

            if (format == "1" || format == "2") 
                break;

            Console.WriteLine("Invalid input. Please enter 1 or 2.");
        }
        return format;
    }

    static async Task<string?> GetWeatherDataInputAsync()
    {
        Console.WriteLine("Paste weather data string or file path:");
        string input = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Input cannot be empty.");
            return null;
        }

        if (File.Exists(input))
        {
            try
            {
                return await File.ReadAllTextAsync(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return null;
            }
        }

        return input;
    }

    static IWeatherDataParser? GetParser(string format)
    {
        var factory = new WeatherDataParserFactory();
        try
        {
            return factory.GetParser(format);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Parser error: " + ex.Message);
            return null;
        }
    }
}