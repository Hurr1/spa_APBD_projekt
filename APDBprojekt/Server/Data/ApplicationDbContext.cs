using APDBprojekt.Server.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APDBprojekt.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Branding> Brandings { get; set; }
        public virtual DbSet<ApplicationUserCompany> UserCompanies { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBulider)
        {
            base.OnConfiguring(optionsBulider);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>(c =>
            {
                c.HasKey(k => k.CompanyId);
            });

            modelBuilder.Entity<ApplicationUserCompany>(a =>
           {
               a.HasKey(k => k.IdApplicationUserCompany);

               a.HasOne(m => m.IdCompanyNavigation)
               .WithMany(n => n.Companies)
               .HasForeignKey(m => m.IdCompany)
               .OnDelete(DeleteBehavior.Cascade);

               a.HasOne(m => m.IdUserNavigation)
               .WithMany(n => n.Companies)
               .HasForeignKey(m => m.Id)
               .OnDelete(DeleteBehavior.Cascade);

           });

        }
        
    }
}
