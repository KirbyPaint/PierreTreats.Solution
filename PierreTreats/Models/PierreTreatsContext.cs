using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PierreTreats.Models
{
  public class PierreTreatsContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<Flavor> Flavors { get; set; }
    public virtual DbSet<Treat> Treats { get; set; }
    public virtual DbSet<FlavorTreat> FlavorTreat { get; set; }
    public PierreTreatsContext(DbContextOptions options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}