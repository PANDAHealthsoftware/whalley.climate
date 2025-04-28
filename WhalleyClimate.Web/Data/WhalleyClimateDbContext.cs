// File: Data/WhalleyClimateDbContext.cs
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WhalleyClimate.Web.Models; // Adjust this if your Models are in a different namespace

namespace WhalleyClimate.Web.Data
{
    // Inherit from IdentityDbContext to support ASP.NET Core Identity out-of-the-box
    public class WhalleyClimateDbContext : IdentityDbContext
    {
        public WhalleyClimateDbContext(DbContextOptions<WhalleyClimateDbContext> options)
            : base(options)
        {
        }

        // =====================
        // Define your DbSets here
        // =====================

        public DbSet<Image> Images { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<ServiceItem> Services { get; set; }

        // (Optional) Customize table names or relationships if needed
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Example: Configure custom table names if you want
            builder.Entity<Image>().ToTable("Images");
            builder.Entity<Feedback>().ToTable("Feedbacks");
            builder.Entity<ServiceItem>().ToTable("Services");

            // Example: Add default values, constraints, indexes here if needed
            builder.Entity<Feedback>()
                .Property(f => f.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Entity<Image>()
                .Property(i => i.UploadedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}