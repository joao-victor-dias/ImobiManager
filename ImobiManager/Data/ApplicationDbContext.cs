using ImobiManager.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImobiManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Apartament> Apartaments { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>().HasKey(c => c.Id);
            modelBuilder.Entity<Apartament>().HasKey(a => a.Id);
            modelBuilder.Entity<Reservation>().HasKey(r => r.Id);
            modelBuilder.Entity<Sale>().HasKey(s => s.Id);

            modelBuilder.Entity<Client>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Apartament>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Reservation>()
                .Property(r => r.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Sale>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();


            modelBuilder.Entity<Reservation>()
               .HasOne(r => r.Client)
               .WithMany()
               .HasForeignKey(r => r.ClientId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Apartament)
                .WithOne(a => a.Reservation)
                .HasForeignKey<Reservation>(r => r.ApartamentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sale>()
               .HasOne(s => s.Client)
               .WithMany()
               .HasForeignKey(s => s.ClientId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Apartament)
                .WithOne()
                .HasForeignKey<Sale>(s => s.ApartamentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
