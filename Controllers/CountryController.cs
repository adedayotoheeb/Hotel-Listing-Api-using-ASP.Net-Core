using AutoMapper;
using HotelListing.Core.UnitOfWork;
using HotelListing.Dtos;
using HotelListing.Helper;
using HotelListing.Models;
using HotelListing.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;


        public CountryController(IUnitOfWork unitOfWork,
            ILogger<CountryController> logger, 
            IMapper mapper,
            IAuthManager authManager)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;

        }

        [HttpGet]
        public async Task<IActionResult> GetCountries([FromQuery] RequestParams requestParams)
        {
            try
            {
                var countries = await _unitOfWork.Countries.GetAllPagedAsync(requestParams);
                var result = _mapper.Map<IEnumerable<CountryDto>>(countries);
                return Ok(result);
            }
            catch (System.Exception ex)
            {

                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetCountries)}");
                return StatusCode(500, "Internal Server Eror. Please Try Again Later.");
            }
        }

        [HttpGet("{id:int}",Name ="GetCountry")]

        public async Task<IActionResult> GetCountryById(int id)
        {
            try
            {
                var country = await _unitOfWork.Countries.GetAsync(q => q.Id == id, new List<string> { "Hotels" });
                var result = _mapper.Map<CountryDto>(country);
                return Ok(result);
            }
            catch (System.Exception ex)
            {

                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetCountryById)}");
                return StatusCode(500, "Internal Server Erorr. Please Try Again Later.");
            }
        }

        [Authorize(Roles = "Adminstrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDto countryDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Inavlid Post attempt in {nameof(CreateCountry)}");
                return BadRequest(ModelState);
            }
            try
            {
                var country = _mapper.Map<Country>(countryDto);
                await _unitOfWork.Countries.InsertAsync(country);
                await _unitOfWork.SaveAsync();

                return CreatedAtRoute("GetCountry", new { id = country.Id }, country);
            }
            catch (System.Exception ex)
            {

                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateCountry)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }

        }
        [Authorize]
        [HttpPut("{id: int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDto countryDto)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalida Update attempt in { nameof(UpdateCountry)}");
                return BadRequest(ModelState);
            }
            try
            {
                var country = await _unitOfWork.Countries.GetAsync(x => x.Id == id);
                if (country == null)
                {
                    _logger.LogError($"Invalid Update attempt in {nameof(UpdateCountry)}");
                    return BadRequest("Sumitted an invalid Id");
                }
                _mapper.Map(countryDto, country);
                _unitOfWork.Countries.Update(country);
                 await  _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"An Invalid delete attempt in {nameof(DeleteCountry)}");
                return BadRequest("ID should be greater than 1");
            }
            try
            {
                var country = await _unitOfWork.Countries.GetAsync(x => x.Id == id);
                if (country == null)
                {
                    _logger.LogError($"An Invalid delete attempt in {nameof(DeleteCountry)}");
                    return BadRequest("Invalid Id");
                }
                await _unitOfWork.Countries.Delete(id);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }
            catch (System.Exception ex)
            {

                _logger.LogError(ex, $"An Invalid delete attempt in {nameof(DeleteCountry)}");
                return StatusCode(500, "internal Server Error . Please try Again Later.");
            }

        }


    }
}
