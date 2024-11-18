namespace Swag.Components.Engine.Calculating.Contract;

public interface ICalculatingEngine
{
    Task<double> Calculate(double optimistic, double mostLikely, double pessimistic);
}