using Swag.Ifx.Data.Models;

namespace Swag.Components.Access.Storage.Orm.Models;

public class ThreePointEstimate : Entity
{

    public double Optimistic { get; set; }
    public double MostLikely { get; set; }
    public double Pessimistic { get; set; }
    public double Calculated { get; set; }

}

