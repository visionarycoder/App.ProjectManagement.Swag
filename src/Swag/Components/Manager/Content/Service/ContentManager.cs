using Swag.Components.Access.Storage.Contract;
using Swag.Components.Engine.Calculating.Contract;
using Swag.Components.Manager.Content.Contract;

namespace Swag.Components.Manager.Content.Service;

public class ContentManager(ILogger<ContentManager> logger, ICalculatingEngine calculatorEngine, INumbersAccess numbersAccess) : IContentManager
{
        
    public async Task<Swag> CalculateSwag(int optimistic, int mostLikely, int pessimistic)
    {
    
        logger.LogDebug($"{nameof(ContentManager)}.{nameof(CalculateSwag)}() called.");
        logger.LogDebug("Calculating estimate for optimistic: {optimistic}, most likely: {mostLikely}, pessimistic: {pessimistic}", optimistic, mostLikely, pessimistic);
        var existing = await numbersAccess.GetSwag(optimistic, mostLikely, pessimistic);
        if (existing.EntryFound)
        {
            logger.LogDebug("Swag found in database");
            return existing.Swag.Convert();
        }

        var calculated = await calculatorEngine.CalculateSwag(optimistic, mostLikely, pessimistic);
        var swag = new Swag
        {
            Optimistic = optimistic,
            MostLikely = mostLikely,
            Pessimistic = pessimistic,
            Calculated = calculated
        };
        var results = await numbersAccess.AddSwag(swag.Convert());
        swag = results.Swag.Convert();
        return swag;

    }

}