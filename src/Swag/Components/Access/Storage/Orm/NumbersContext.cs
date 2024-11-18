using Microsoft.EntityFrameworkCore;
using Swag.Components.Access.Storage.Orm.Models;

namespace Swag.Components.Access.Storage.Orm;

public class NumbersContext(DbContextOptions<NumbersContext> options) : DbContext(options)
{

    public DbSet<ThreePointEstimate> Swags { get; set; } = default!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ThreePointEstimate>().ToTable("Swags");
    }

}