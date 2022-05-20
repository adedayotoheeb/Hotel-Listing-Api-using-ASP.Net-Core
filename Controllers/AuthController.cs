using AutoMapper;
using HotelListing.Dtos;
using HotelListing.Models;
using HotelListing.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager; 
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public AuthController(

            UserManager<User> userManager,
            ILogger<AuthController> logger,
            IMapper mapper,
            IAuthManager authManager

            )
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
           _logger.LogInformation($"Registration Attempt for {userDto.Email}");
           if (!ModelState.IsValid)
           {

               return BadRequest(ModelState);
           }
           try
           {
                var user = _mapper.Map<User>(userDto);
                user.UserName = userDto.Email;
                var result = await _userManager.CreateAsync(user,userDto.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    await _userManager.AddToRolesAsync(user, userDto.Roles);
                    return BadRequest(ModelState);

                }

                return Accepted();

            }
           catch (System.Exception ex)
            {

               _logger.LogError(ex, $"Something Went Wrong in the {nameof(Register)}");
                return Problem($"Something went wrong in the {nameof(Register)}",statusCode: 500);
            }
        }


        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
          _logger.LogInformation($"Login Attempt for {loginDto.email}");
            if (!ModelState.IsValid)
            {
               return BadRequest(ModelState);
            }
            try
            {

              

                if (!await _authManager.ValidateUserAsync(loginDto))
                {
                    return Unauthorized();

                }
                return Accepted( new { Token = await _authManager.CreateToken() });

            }
            catch (System.Exception ex)
            {

                _logger.LogError(ex, $"Something Went Wrong in the {nameof(Login)}");
                return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
           }
        }
    }
}
