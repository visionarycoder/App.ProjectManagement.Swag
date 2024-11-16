namespace Swag.Engine.Calculator.Service.Helpers;

public class SwagCalculator
{

    /// <summary>
    /// SWAG stands for Scientific Wild-ass Guess.
    /// https://projectmanagers.net/swag-estimates-in-project-management/
    /// </summary>
    /// <param name="optimistic"></param>
    /// <param name="mostLikely"></param>
    /// <param name="pessimistic"></param>
    /// <returns>Calculated value</returns>
    public async Task<int> Calculate(int optimistic, int mostLikely, int pessimistic)
    {
        var numerator = (optimistic + (mostLikely * 4) + pessimistic);
        var result =  numerator / (decimal) 6;
        var calculated = (int) Math.Ceiling(result);
        return await Task.FromResult(calculated);
    }

}