using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

using Swag.Access.Data.Interface;
using Swag.Access.Data.Service;
using Swag.Engine.Calculator.Interface;
using Swag.Engine.Calculator.Service;
using Swag.Framework.Helpers;
using Swag.Manager.Calculation.Interface;
using Swag.Manager.Calculation.Interface.Models;
using Swag.Manager.Calculation.Service;

var host = new HostBuilder()
    .ConfigureDefaults(args)
    .ConfigureHostConfiguration(builder =>
    {
        builder.AddCommandLine(args);
    })
    .ConfigureAppConfiguration((context, builder) =>
    {
        builder.AddJsonFile("AppSettings.json", optional: true);
        builder.AddJsonFile($"AppSettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true);
    })
    .ConfigureLogging(builder =>
    {
        builder.ClearProviders();
        builder.AddSimpleConsole(options =>
        {
            options.IncludeScopes = true;
            options.SingleLine = true;
            options.TimestampFormat = "[HH:mm:ss] ";
            options.ColorBehavior = LoggerColorBehavior.Enabled;
            options.UseUtcTimestamp = false;
        });
    })
    .ConfigureServices((_, services) =>
    {

        services.AddScoped<IDataAccess, DataAccess>();
        services.AddScoped<ICalculatorEngine, CalculatorEngine>();
        services.AddScoped<IContentManager, ContentManager>();

    })
    .Build();

var contentManager = host.Services.GetRequiredService<IContentManager>();
var logger = host.Services.GetRequiredService<ILogger<Program>>();
var estimates = new List<Estimate>();

logger.LogInformation("Starting Console Client");
try
{

    ConsoleHelper.Title = "Swag Helper";
    ConsoleHelper.Description = "Estimate Calculator";
    ConsoleHelper.ShowHeader();

    await GetUserInputs(contentManager);

    ConsoleHelper.ShowAsTable(estimates);
    ConsoleHelper.ShowFooter();
    ConsoleHelper.ShowExit();
    logger.LogInformation("Console Client Finished");
    return await Task.FromResult(0);
}
catch (Exception ex)
{
    logger.LogError(ex, "Error in Console Client");
    return await Task.FromResult(1);
}

async Task GetUserInputs(IContentManager manager)
{

    while(true)
    {
        
        ConsoleHelper.ShowUpdate("Enter Estimates Values: ");
        Console.Write("Optimistic:  ");
        var optimistic = ConsoleHelper.GetIntegerInput();

        Console.Write("MostLikely:  ");
        var mostLikely = ConsoleHelper.GetIntegerInput();

        Console.Write("Pessimistic: ");
        var pessimistic = ConsoleHelper.GetIntegerInput();

        var estimate = await manager.CalculateEstimate(optimistic, mostLikely, pessimistic);
        estimates.Add(estimate);

        Console.Write($"Calculated: {estimate.Calculated}");
        
        bool invalidInput;
        do
        {
            ConsoleHelper.ShowUpdate("Do you want to calculate another estimate? (Y/N): ");
            (var exit, invalidInput) = ConsoleHelper.GetYesNoInput();
            if (invalidInput && exit)
            {
                return;
            }
        } while (! invalidInput);

    } 

}