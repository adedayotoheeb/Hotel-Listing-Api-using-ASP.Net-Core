using System.ComponentModel.DataAnnotations;

namespace HotelListing.Dtos
{
    public class CreateCountryDto 
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Country Name is to Long", MinimumLength =1)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 3, ErrorMessage = "Short Name is to Long")]
        public string ShortName { get; set; }
    }
}

