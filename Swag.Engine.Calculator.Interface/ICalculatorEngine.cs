using System.ServiceModel;

namespace Swag.Engine.Calculator.Interface;

[ServiceContract] public interface ICalculatorEngine
{

    [OperationContract] Task<int> CalculateEstimate(int optimistic, int mostLikely, int pessimistic);

}