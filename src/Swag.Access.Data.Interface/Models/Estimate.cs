using System.Runtime.Serialization;

namespace Swag.Access.Data.Interface.Models;

[DataContract] public class Estimate 
{
    
    [DataMember] public int Id { get; set; }
    [DataMember] public int Optimistic { get; set; }
    [DataMember] public int MostLikely { get; set; }
    [DataMember] public int Pessimistic { get; set; }
    [DataMember] public int Calculated { get; set; }

}