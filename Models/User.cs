using Microsoft.AspNetCore.Identity;

namespace HotelListing.Models
{
    public class User : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
