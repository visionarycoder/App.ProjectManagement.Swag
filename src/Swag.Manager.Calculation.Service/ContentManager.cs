using Microsoft.Extensions.Logging;
using Swag.Access.Data.Interface;
using Swag.Engine.Calculator.Interface;
using Swag.Manager.Calculation.Interface;
using Swag.Manager.Calculation.Interface.Models;
using Swag.Manager.Calculation.Service.Helpers;

namespace Swag.Manager.Calculation.Service;

public class ContentManager(ILogger<ContentManager> logger, ICalculatorEngine calculatorEngine, IDataAccess dataAccess) : IContentManager
{
        
    public async Task<Estimate> CalculateEstimate(int optimistic, int mostLikely, int pessimistic)
    {
    
        logger.LogDebug($"{nameof(ContentManager)}.{nameof(CalculateEstimate)}() called.");
        logger.LogDebug("Calculating estimate for optimistic: {optimistic}, most likely: {mostLikely}, pessimistic: {pessimistic}", optimistic, mostLikely, pessimistic);
        var estimate = await dataAccess.GetEstimate(optimistic, mostLikely, pessimistic);

        var calculated = await calculatorEngine.CalculateEstimate(optimistic, mostLikely, pessimistic);
        var estimate = new Estimate
        {
            Optimistic = optimistic,
            MostLikely = mostLikely,
            Pessimistic = pessimistic,
            Calculated = calculated
        };
        return estimate;

    }

}