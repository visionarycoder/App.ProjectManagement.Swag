namespace Engine.Calculating.Service.Helpers;

public class ThreePointCalculator
{

    /// <summary>
    /// SWAG stands for Scientific Wild-ass Guess.
    /// https://projectmanagers.net/swag-estimates-in-project-management/
    /// </summary>
    /// <param name="optimistic"></param>
    /// <param name="mostLikely"></param>
    /// <param name="pessimistic"></param>
    /// <param name="precision"></param>
    /// <returns></returns>
    public async Task<decimal> Calculate(int optimistic, int mostLikely, int pessimistic, int precision = 4)
    {
        var numerator = (optimistic + (mostLikely * 4) + pessimistic);
        var result =  numerator / (decimal) 6;
        result = Math.Round(result, precision);
        return await Task.FromResult(result);
    }

}

public class UseCasePointsCalculator
{
    public int CalculateUUCP(List<UseCase> useCases, List<Actor> actors)
    {
        int totalUseCasePoints = 0;
        foreach (var useCase in useCases)
        {
            totalUseCasePoints += useCase.ComplexityPoints;
        }

        int totalActorPoints = 0;
        foreach (var actor in actors)
        {
            totalActorPoints += actor.ComplexityPoints;
        }

        return totalUseCasePoints + totalActorPoints;
    }

    public double CalculateTCF(List<TechnicalFactor> technicalFactors)
    {
        int sumTechnicalFactors = 0;
        foreach (var factor in technicalFactors)
        {
            sumTechnicalFactors += factor.Rating;
        }
        return 0.6 + (0.01 * sumTechnicalFactors);
    }

    public double CalculateEF(List<EnvironmentalFactor> environmentalFactors)
    {
        int sumEnvironmentalFactors = 0;
        foreach (var factor in environmentalFactors)
        {
            sumEnvironmentalFactors += factor.Rating;
        }
        return 1.4 + (-0.03 * sumEnvironmentalFactors);
    }

    public double CalculateUCP(int uucp, double tcf, double ef)
    {
        return uucp * tcf * ef;
    }
}