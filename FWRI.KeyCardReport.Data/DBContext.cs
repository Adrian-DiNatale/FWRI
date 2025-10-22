using FWRI.KeyCardReport.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FWRI.KeyCardReport.Data
{
    /// <summary>
    /// Database Context using Entities matching JSON files
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Image> Images => Set<Image>();
        public DbSet<KeyCardEntry> KeyCardEntries => Set<KeyCardEntry>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Setup the Entities' Foriegn Keys and define mappings.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Image>()
               .HasOne(i => i.Category)
               .WithMany(c => c.Images)
               .HasForeignKey(i => i.ImageCategoryId);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.ProfilePicture)
                .WithMany(i => i.Employees)
                .HasForeignKey(e => e.ProfilePictureId);

            modelBuilder.Entity<Employee>()
                .HasAlternateKey(e => e.KeyCardId);

            modelBuilder.Entity<KeyCardEntry>(k =>
            {
                k.HasOne(k => k.Employee)
                .WithMany(e => e.KeyCardEntries)
                .HasForeignKey(k => k.KeyCardId)
                .HasPrincipalKey(e => e.KeyCardId);

                k.HasIndex(ki => new { ki.KeyCardId, ki.EntryDateTime }).IsUnique();
            });

        }
    }
}