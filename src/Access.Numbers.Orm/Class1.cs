using Access.Numbers.Orm.Models;
using Microsoft.EntityFrameworkCore;

namespace Access.Numbers.Orm
{
    public class NumbersContext(DbContextOptions<NumbersContext> options) : DbContext(options)
    {

        public DbSet<UseCase> UseCases { get; set; } = default!;
        public DbSet<Actor> Actors { get; set; } = default!;
        public DbSet<TechnicalFactor> TechnicalFactors { get; set; } = default!;
        public DbSet<EnvironmentalFactor> EnvironmentalFactors { get; set; } = default!;
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UseCase>().ToTable(nameof(NumbersContext.UseCases));
            modelBuilder.Entity<Actor>().ToTable(nameof(NumbersContext.Actors));
            modelBuilder.Entity<TechnicalFactor>().ToTable(nameof(TechnicalFactors));
            modelBuilder.Entity<EnvironmentalFactor>().ToTable(nameof(EnvironmentalFactors));

        }

    }

}
