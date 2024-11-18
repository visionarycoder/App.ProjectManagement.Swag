
var host = new HostBuilder()
    .ConfigureDefaults(args)
    .ConfigureHostConfiguration(builder =>
    {

        builder.AddEnvironmentVariables();

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
    .ConfigureServices((context, services) =>
    {

        var connectionString = context.Configuration.GetConnectionString("NumbersDB");
        services.AddDbContext<NumbersContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        services.AddScoped<INumbersAccess, NumbersAccess>();
        services.AddScoped<ICalculatingEngine, CalculatingEngine>();
        services.AddScoped<IContentManager, ContentManager>();

    })
    .Build();

var cache = new List<Swag>();
var contentManager = host.Services.GetRequiredService<ContentManager>();

try
{

    ConsoleHelper.Title = "ThreePointEstimate Helper";
    ConsoleHelper.Description = "ThreePointEstimate Calculator";
    ConsoleHelper.ShowHeader();

    var stop = false;
    do
    {
        ConsoleHelper.ShowUpdate("Enter Swags Values: ");
        Console.Write("Optimistic  : ");
        var optimistic = ConsoleHelper.GetIntegerInput();

        Console.Write("MostLikely  : ");
        var mostLikely = ConsoleHelper.GetIntegerInput();

        Console.Write("Pessimistic : ");
        var pessimistic = ConsoleHelper.GetIntegerInput();

        var cached = cache.FirstOrDefault(c => c.Optimistic == optimistic && c.MostLikely == mostLikely && c.Pessimistic == pessimistic);
        if (cached is not null)
        {
            Console.Write($"Calculated : {cached.Calculated}");
        }
        else
        {
            var swag = await contentManager.CalculateSwag(optimistic, mostLikely, pessimistic);
            cache.Add(swag);
            Console.Write($"Calculated : {swag.Calculated}");
        }

        bool invalidInput;
        do
        {

            ConsoleHelper.ShowUpdate("Do you want to calculate another estimate? (Y/N): ");
            (var exit, invalidInput) = ConsoleHelper.GetYesNoInput();
            if (invalidInput)
            {
                stop = exit;
            }

        } while (!invalidInput);

    } while (stop);

    ConsoleHelper.ShowAsTable(cache);
    ConsoleHelper.ShowFooter();
    ConsoleHelper.ShowExit();

}
catch (Exception ex)
{
    Console.WriteLine(ex);
}