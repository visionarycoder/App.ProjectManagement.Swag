using Ifx.Data.Models;

namespace Resource.Data.NumbersDB.Models;

public class ThreePointEstimate : Entity
{

    public double Optimistic { get; set; }
    public double MostLikely { get; set; }
    public double Pessimistic { get; set; }
    public double Calculated { get; set; }

}

