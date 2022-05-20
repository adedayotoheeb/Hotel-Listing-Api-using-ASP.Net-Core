using HotelListing.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.Dtos
{
   

    public class HotelDto : CreateHotelDto
    {
        public int Id { get; set; } 
        public CountryDto Country { get; set; }

    }
}
