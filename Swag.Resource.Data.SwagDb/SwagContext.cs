using Swag.Resource.Data.SwagDb.Models;

namespace Swag.Resource.Data.SwagDb;

public class SwagContext
{
        
    private static readonly List<Estimate> estimates = [];

    public List<Estimate> Estimates => estimates;

}