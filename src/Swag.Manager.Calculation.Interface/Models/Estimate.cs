using System.Runtime.Serialization;

namespace Swag.Manager.Calculation.Interface.Models;

[DataContract] public class Estimate
{
    [DataMember(Order = 1)] public int Id { get; set; }
    [DataMember(Order = 2)] public int Optimistic { get; set; }
    [DataMember(Order = 3)] public int MostLikely { get; set; }
    [DataMember(Order = 4)] public int Pessimistic { get; set; }
    [DataMember(Order = 5)] public decimal Calculated { get; set; }
}