using HotelListing.DbContexts.Entities;
using HotelListing.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace HotelListing.DbContexts
{
    public class DatabaseContext : IdentityDbContext <User>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) :base(options)
        {

        }
        public DbSet<Country> Countries { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new HotelConfiguration());
          
        }

      
       
    }

    
}
