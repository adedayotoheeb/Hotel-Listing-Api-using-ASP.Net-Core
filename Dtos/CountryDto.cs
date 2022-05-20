using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.Dtos
{
    public class CountryDto : CreateCountryDto
    {
        public int Id { get; set; }

        public IList<HotelDto> Hotels { get; set; }
    }
}

