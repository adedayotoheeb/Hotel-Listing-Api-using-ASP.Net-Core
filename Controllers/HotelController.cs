using AutoMapper;
using HotelListing.Core.UnitOfWork;
using HotelListing.Dtos;
using HotelListing.Models;
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
    public class HotelController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HotelController> _logger;
        private readonly IMapper _mapper;


        public HotelController(IUnitOfWork unitOfWork, ILogger<HotelController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotels()
        {
            
                var hotels = await _unitOfWork.Hotels.GetAllAsync();
                var result = _mapper.Map<IList<HotelDto>>(hotels);
                return Ok(result);
           
        }

        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotelById(int id)
        {
            
                var hotel = await _unitOfWork.Hotels.GetAsync(q => q.Id == id, new List<string> { "Country"});
                if (hotel == null)
                {
                    _logger.LogError($"Invalid id was passed in {nameof(GetHotelById)} function");
                    return BadRequest("Invalid Id");
                }
                var result = _mapper.Map<HotelDto>(hotel);
                return Ok(result);
           
        }
        [Authorize(Roles = "Adminstrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDto hotelDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Inavlid Post attempt in {nameof(CreateHotel)}");
                return BadRequest(ModelState);
            }
           
                var hotel = _mapper.Map<Hotel>(hotelDto);
                await _unitOfWork.Hotels.InsertAsync(hotel);
                await _unitOfWork.SaveAsync();

                return CreatedAtRoute("GetHotel", new {id = hotel.Id}, hotel);
          

        }

        [Authorize]
        [HttpPut("{id: int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateHotel (int id , [FromBody] UpdateHotelDto hotelDto)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalida Update attempt in { nameof(UpdateHotel)}");
                return BadRequest(ModelState);
            }
           
                var hotel = await _unitOfWork.Hotels.GetAsync(x => x.Id == id);
                if (hotel == null)
                {
                    _logger.LogError($"Invalid Update attempt in {nameof(UpdateHotel)}");
                    return BadRequest("Sumitted an invalid Id");
                }
                _mapper.Map(hotelDto, hotel);   
                _unitOfWork.Hotels.Update(hotel);
                await _unitOfWork.SaveAsync();

                return NoContent();
          
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"An Invalid delete attempt in {nameof(DeleteHotel)}");
                return BadRequest("ID should be greater than 1");
            }
            
                var hotel = await _unitOfWork.Hotels.GetAsync(x => x.Id == id);
                if (hotel == null)
                {
                    _logger.LogError($"An Invalid delete attempt in {nameof(DeleteHotel)}");
                    return BadRequest("Invalid Id");
                }
                await _unitOfWork.Hotels.Delete(id);
                await _unitOfWork.SaveAsync();
                return NoContent();
           
        }
    }
}
