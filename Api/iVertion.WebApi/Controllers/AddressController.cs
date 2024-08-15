using iVertion.Application.DTOs;
using iVertion.Application.Interfaces;
using iVertion.Domain.FiltersDb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iVertion.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly ICityService _cityService;
        private readonly IStateService _stateService;
        private readonly ICountryService _countryService;
        private readonly IPersonAddressService _personAddressService;

        public AddressController(IAddressService addressService,
                                 ICityService cityService,
                                 IStateService stateService,
                                 ICountryService countryService,
                                 IPersonAddressService personAddressService)
        {
            _addressService = addressService ??
                throw new ArgumentNullException(nameof(addressService));
            _cityService = cityService ?? 
                throw new ArgumentNullException( nameof(cityService));
            _stateService = stateService ?? 
                throw new ArgumentNullException(nameof(stateService));
            _countryService = countryService ?? 
                throw new ArgumentNullException(nameof(countryService));
            _personAddressService = personAddressService ?? 
                throw new ArgumentNullException(nameof(personAddressService));
        }

        [HttpGet]
         [Authorize]
        public async Task<ActionResult> GetAddressesAsync([FromQuery] AddressFilterDb addressFilterDb)
        {
            var result = await _addressService.GetAddressesAsync(addressFilterDb);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> GetAddressByIdAsync(int id)
        {
            var result = await _addressService.GetAddressByIdAsync(id);
            if (result.Data == null)
                return NotFound();
            if (result.IsSuccess) { 
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddAddressAsync([FromBody] AddressDTO addressDto)
        {
            try
            {
                var userId = User.FindFirst("UID").Value;
                var dateNow = DateTime.UtcNow;
                addressDto.CreatedAt = dateNow;
                addressDto.UpdatedAt = dateNow;
                addressDto.UserId = userId;
                addressDto.Active = true;
                await _addressService.CreateAddressAsync(addressDto);
                return Ok("Address has been successfully added.");
            }
            catch (Exception ex) { 
                return BadRequest(ex);
            }
        }
        [HttpGet("City")]
        [Authorize]
        public async Task<ActionResult> GetCitiesAsync([FromQuery] CityFilterDb cityFilterDb)
        {
            var result = await _cityService.GetCitiesAsync(cityFilterDb);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("State")]
        [Authorize]
        public async Task<ActionResult> GetStatesAsync([FromQuery] StateFilterDb stateFilterDb)
        {
            var result = await _stateService.GetStatesAsync(stateFilterDb);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("Country")]
        [Authorize]
        public async Task<ActionResult> GetCountriesAsync([FromQuery] CountryFilterDb countryFilterDb)
        {
            var result = await _countryService.GetCountriesAsync(countryFilterDb);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
