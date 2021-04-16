using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Linq;

namespace Mars_Rover_Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()     // This Should be set in the appsettings.json, There is a bug with the .net 5 implementation that prevents this https://github.com/serilog/serilog-settings-configuration#net-50-single-file-applications
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application Starting");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IRoverService, RoverService>();
                    services.AddTransient<IPosition, Position>();
                })
                .UseSerilog()
                .Build();

            var lines = File.ReadAllLines($"{Directory.GetCurrentDirectory()}\\RoverInstructions.txt").ToList(); // Reads Rover Instructions from text file

            var rover = ActivatorUtilities.CreateInstance<RoverService>(host.Services);
            rover.Run(lines);

            Log.Logger.Information("Application Stopping");

            Console.ReadLine(); // Prevent closing
        }
        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}
