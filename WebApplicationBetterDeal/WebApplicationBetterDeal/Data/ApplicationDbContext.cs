using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplicationBetterDeal.Models;
using WebApplicationBetterDeal.Data;

namespace WebApplicationBetterDeal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            // builder.Entity<Publication>().HasOne(u => u.ApplicationUser).WithMany(u => u.Publications);
            builder.Entity<ApplicationUser>().HasMany(u => u.Publications).WithOne(s => s.ApplicationUser);
            builder.Entity<Publication>().HasMany(p => p.Responses).WithOne(r => r.Publication);
            builder.Entity<ApplicationUser>().HasMany(u => u.Responses).WithOne(r => r.ApplicationUser);
        }

        public DbSet<WebApplicationBetterDeal.Models.Publication> Publication { get; set; }

        // ici supp pers

        public DbSet<WebApplicationBetterDeal.Data.ApplicationUser> ApplicationUser { get; set; }

        // ici supp pers

        public DbSet<WebApplicationBetterDeal.Models.Response> Response { get; set; }

        // ici supp pers

        public DbSet<WebApplicationBetterDeal.Models.InfoTest> InfoTest { get; set; }

       // public DbSet<WebApplicationBetterDeal.Models.Publication> Publication { get; set; }
    }
}
