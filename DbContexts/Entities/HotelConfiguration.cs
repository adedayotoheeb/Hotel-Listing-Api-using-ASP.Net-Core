using HotelListing.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.DbContexts.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
             new Hotel
             {
                 Id = 1,
                 Name = "Continental Hotel",
                 Address = "Abeokuta Ogun State",
                 CountryId = 1,
                 Rating = 4.5,


             },
             new Hotel
             {
                 Id = 2,
                 Name = "Continental Hotel",
                 Address = "Belarus Europe",
                 CountryId = 3,
                 Rating = 3.5,

             },
             new Hotel
             {
                 Id = 3,
                 Name = "Putin Hotel",
                 Address = "Sts Petersburg",
                 CountryId = 2,
                 Rating = 5.0,

             }
             );
        }
    }
}
