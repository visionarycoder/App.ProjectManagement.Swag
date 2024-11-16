using Microsoft.EntityFrameworkCore;
using Resource.Data.NumbersDB.Models;

namespace Resource.Data.NumbersDB;

public class NumbersContext(DbContextOptions<NumbersContext> options) : DbContext(options)
{

    public DbSet<ThreePointEstimate> Swags { get; set; } = default!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ThreePointEstimate>().ToTable("Swags");
    }

}