using System.ComponentModel.DataAnnotations;

namespace HotelListing.Dtos
{
    public class CreateHotelDto
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Hotel Name is to Long")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Address is to Long")]
        public string Address { get; set; }
        [Required]
        [Range(1, 5)]
        public double Rating { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}
