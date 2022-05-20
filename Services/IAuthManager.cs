using HotelListing.Dtos;
using System.Threading.Tasks;

namespace HotelListing.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUserAsync(LoginDto loginDto);
        Task<string> CreateToken();
    }
}
