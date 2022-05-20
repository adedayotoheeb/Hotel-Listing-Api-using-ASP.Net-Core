using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage = "First Name is a required field")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is a required field")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
       
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is a required field")]
        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required (ErrorMessage = "Password is a compulsory field")]
        [StringLength(14, MinimumLength =8, ErrorMessage ="Password length must be between 8 and 14 digits")]
        [DataType(DataType.Password)]
        public string  Password { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
