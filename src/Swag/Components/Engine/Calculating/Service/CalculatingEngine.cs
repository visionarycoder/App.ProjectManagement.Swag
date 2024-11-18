using Microsoft.Extensions.Logging;
using Swag.Components.Engine.Calculating.Contract;
using Swag.Components.Engine.Calculating.Service.Helpers;

namespace Swag.Components.Engine.Calculating.Service;

public class CalculatingEngine(ILogger<CalculatingEngine> logger) : ICalculatingEngine
{

    private readonly ThreePointCalculator threePointCalculator = new();

    public async Task<decimal> Calculate(int optimistic, int mostLikely, int pessimistic)
    {
        logger.LogDebug($"{nameof(CalculatingEngine)}.{nameof(Calculate)}() called.");
        logger.LogDebug("Calculating estimate for optimistic: {optimistic}, most likely: {mostLikely}, pessimistic: {pessimistic}", optimistic, mostLikely, pessimistic);
        var calculated = await threePointCalculator.Calculate(optimistic, mostLikely, pessimistic);
        logger.LogDebug($"Calculated estimate: {calculated}");
        return calculated;
    }

}