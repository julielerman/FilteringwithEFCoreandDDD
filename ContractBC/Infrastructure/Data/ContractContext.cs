using ContractBC.ContractAggregate;

using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data;

public class ContractContext : DbContext
{
  
    public ContractContext(DbContextOptions options) : base(options)
    {
  
    }

    

    public DbSet<Contract> Contracts => Set<Contract>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            modelBuilder.Entity<ContractVersion>().OwnsMany(
                v => v.Authors,
            a => { a.OwnsOne(a => a.Name);
                   a.ToTable("ContractVersion_Authors");
            });


        modelBuilder.Entity<ContractVersion>().OwnsOne(v => v.Specs, s =>
              s.Property(p => p.PriceForAddlAuthorCopiesUSD)
         );
        modelBuilder.Entity<Contract>().Property(c => c.ContractNumber).HasField("_contractNumber");
        modelBuilder.Entity<Contract>().Property(c => c.DateInitiated).HasField("_initiated");
        modelBuilder.Entity<ContractVersion>().Property("_hasRevisedSpecSet");
        modelBuilder.Entity<ContractVersion>().ToTable("ContractVersions");
        modelBuilder.Entity<ContractVersion>().Property(v => v.Id).ValueGeneratedNever();
     }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>().HaveColumnType("decimal(18, 2)");
    }
}