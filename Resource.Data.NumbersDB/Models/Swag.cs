using Ifx.Data.Models;

namespace Resource.Data.NumbersDB.Models;

public class Swag : Entity
{

    public int Optimistic { get; set; }
    public int MostLikely { get; set; }
    public int Pessimistic { get; set; }
    public decimal Calculated { get; set; }

}