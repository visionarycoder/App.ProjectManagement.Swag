namespace Engine.Calculating.Contract.Models;

public class ThreePointEstimate
{
    public int Id { get; set; }
    public int Optimistic { get; set; }
    public int MostLikely { get; set; }
    public int Pessimistic { get; set; }
    public decimal Calculated { get; set; }

}

