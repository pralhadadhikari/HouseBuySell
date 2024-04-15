using HouseBuySell.Infrastructure.Entity_Configuration;
using HouseBuySell.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace HouseBuySell.Infrastructure
{
    public class HouseLandContext : DbContext
    {
        public HouseLandContext()
        {

        }
        public HouseLandContext(DbContextOptions<HouseLandContext> options)
            : base(options)
        {
        }

        public DbSet<HouseLandDetail> HouseLandDetail { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PropertyTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());

        }
    }
}
