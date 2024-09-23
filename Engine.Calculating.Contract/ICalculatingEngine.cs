namespace Engine.Calculating.Contract;

public interface ICalculatingEngine
{
    Task<decimal> CalculateSwag(int optimistic, int mostLikely, int pessimistic);
}