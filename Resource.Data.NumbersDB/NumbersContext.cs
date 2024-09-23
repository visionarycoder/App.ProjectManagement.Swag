using Microsoft.EntityFrameworkCore;
using Resource.Data.NumbersDB.Models;

namespace Resource.Data.NumbersDB;

public class NumbersContext(DbContextOptions<NumbersContext> options) : DbContext(options)
{
    public DbSet<Swag> Swags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Swag>().ToTable("Swags");
    }

}