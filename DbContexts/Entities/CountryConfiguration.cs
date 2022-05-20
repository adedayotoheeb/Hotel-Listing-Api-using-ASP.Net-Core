using HotelListing.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.DbContexts.Entities
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                 new Country
                 {
                     Id = 1,
                     Name = "Nigeria",
                     ShortName = "NGN"

                 },
                 new Country
                 {
                     Id = 2,
                     Name = "Russia",
                     ShortName = "RUS"
                 },
                 new Country
                 {
                     Id = 3,
                     Name = "Belarus",
                     ShortName = "BUS"
                 }
                 );
        }
    }
}
