using System.ServiceModel;
using Swag.Manager.Calculation.Interface.Models;

namespace Swag.Manager.Calculation.Interface;

[ServiceContract] public interface IContentManager
{

    [OperationContract] Task<Estimate> CalculateEstimate(int optimistic, int mostLikely, int pessimistic);

}