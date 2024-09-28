using System.ServiceModel;
using Swag.Access.Data.Interface.Models;

namespace Swag.Access.Data.Interface;

[ServiceContract] public interface IDataAccess 
{

    [OperationContract] Task<IEnumerable<Estimate>> GetEstimates();
    [OperationContract] Task<Estimate?> GetEstimate(int optimistic, int mostLikely, int pessimistic);
    [OperationContract] Task<bool> AddEstimate(Estimate estimate);
    [OperationContract] Task<bool> UpdateEstimate(Estimate estimate);
    [OperationContract] Task<bool> DeleteEstimate(int id);

}