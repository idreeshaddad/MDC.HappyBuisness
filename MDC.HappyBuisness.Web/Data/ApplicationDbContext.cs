using MDC.HappyBuisness.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MDC.HappyBuisness.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Classification> Classifications { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Pharmacist> Pharmacists { get; set; }
        public DbSet<Deal> Deals { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}