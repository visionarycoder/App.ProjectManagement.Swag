using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ifx.Data.Models;

public abstract class Entity
{

    [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
    [Required] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Required] public string CreatedBy { get; set; } = "System";
    [Required] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    [Required] public string UpdatedBy { get; set; } = "System";
    [Required, Timestamp] public byte[] Version { get; set; }

}