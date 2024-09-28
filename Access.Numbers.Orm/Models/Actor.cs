using System.ComponentModel.DataAnnotations;

namespace Access.Numbers.Orm.Models
{
    public class Actor
    {
        [Key] public int Id { get; set; }
        [MaxLength(64)] public string Name { get; set; } = string.Empty;
        public int ComplexityPoints { get; set; }
    }
}