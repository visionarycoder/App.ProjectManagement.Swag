using Access.Numbers.Contract;
using Engine.Calculating.Contract;
using Engine.Calculating.Service.Helpers;
using Microsoft.Extensions.Logging;

namespace Engine.Calculating.Service;

public class CalculatingEngine(ILogger<CalculatingEngine> logger) : ICalculatingEngine
{

    private readonly SwagCalculator swagCalculator = new();

    public async Task<decimal> CalculateSwag(int optimistic, int mostLikely, int pessimistic)
    {
        logger.LogDebug($"{nameof(CalculatingEngine)}.{nameof(CalculateSwag)}() called.");
        logger.LogDebug("Calculating estimate for optimistic: {optimistic}, most likely: {mostLikely}, pessimistic: {pessimistic}", optimistic, mostLikely, pessimistic);
        var calculated = await swagCalculator.Calculate(optimistic, mostLikely, pessimistic);
        logger.LogDebug($"Calculated estimate: {calculated}");
        return calculated;
    }

}