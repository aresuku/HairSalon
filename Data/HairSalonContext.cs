using Microsoft.EntityFrameworkCore;
using Models;
namespace Data
{
    public class HairSalonContext:DbContext
    {
        public DbSet<Master> Masters { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Reception> Receptions { get; set; }
        public HairSalonContext()
        {
        }
        public HairSalonContext(DbContextOptions<HairSalonContext> options) : base(options)
        {
        }       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reception>()
                .HasIndex(a => new { a.MasterId, a.Time })
                .IsUnique();
            modelBuilder.Entity<Reception>()
                .HasOne(r => r.Master)
                .WithMany(m => m.Receptions)
                .HasForeignKey(a => a.MasterId);
            modelBuilder.Entity<Reception>()
                .HasOne(s => s.Service)
                .WithMany(m => m.Receptions)
                .HasForeignKey(a => a.ServiceId);
            DbSeeder.Seed(modelBuilder);
        }            
    }
}
