using iVertion.Application.DTOs;
using iVertion.Application.Interfaces;
using iVertion.Domain.Account;
using iVertion.Domain.FiltersDb;
using iVertion.Infra.Data.Identity;
using iVertion.WebApi.Models;
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
        private readonly IUserInterface<ApplicationUser> _userService;

        public AddressController(IAddressService addressService,
                                 ICityService cityService,
                                 IStateService stateService,
                                 ICountryService countryService,
                                 IPersonAddressService personAddressService,
                                 IUserInterface<ApplicationUser> userservice)
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
            _userService = userservice ??
                throw new ArgumentNullException(nameof(userservice));
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

        [HttpPost("AddMyAddressAsync")]
        [Authorize]
        public async Task<ActionResult> AddMyAddressAsync([FromBody] AddressRegister addressRegister)
        {
            try
            {
                int cityId = 0;
                int stateId = 0;
                int countryId = 0;
                var dateNow = DateTime.UtcNow;
                var userId = User.FindFirst("UID").Value;
                var user = await _userService.GetUserByIdAsync(userId);
                var cityFilterDb = new CityFilterDb(){
                    Name = addressRegister.CityName,
                    Active = true
                };
                var city = await _cityService.GetCitiesAsync(cityFilterDb);
                if (city.IsSuccess){
                    if (city.Data.TotalRegisters > 0) {
                        cityId = city.Data.Data[0].Id;
                    } else {
                        var cityDto = new CityDTO(){
                            Name = addressRegister.CityName,
                            Active = true,
                            CreatedAt = dateNow,
                            UpdatedAt = dateNow,
                            UserId = userId
                        };
                        await _cityService.CreateCityAsync(cityDto);
                        Console.WriteLine("Passei aqui");
                        city = await _cityService.GetCitiesAsync(cityFilterDb);
                        cityId = city.Data.Data[0].Id;
                    }
                }
                var stateFilterDb = new StateFilterDb(){
                    Name = addressRegister.StateFull,
                    Active = true,
                    Acronym = addressRegister.State,
                };
                var state = await _stateService.GetStatesAsync(stateFilterDb);
                if (state.IsSuccess){
                    if (state.Data.TotalRegisters > 0) {
                        stateId = state.Data.Data[0].Id;
                    } else {
                        var stateDto = new StateDTO(){
                            Name = addressRegister.StateFull,
                            Acronym = addressRegister.State,
                            Active = true,
                            CreatedAt = dateNow,
                            UpdatedAt = dateNow,
                            UserId = userId
                        };
                        await _stateService.CreateStateAsync(stateDto);
                        state = await _stateService.GetStatesAsync(stateFilterDb);
                        stateId = state.Data.Data[0].Id;
                    }
                }
                var countryFilterDb = new CountryFilterDb(){
                    Name = addressRegister.CountryFull,
                    Active = true,
                    Acronym = addressRegister.Country,
                };
                var country = await _countryService.GetCountriesAsync(countryFilterDb);
                if (country.IsSuccess){
                    if (country.Data.TotalRegisters > 0) {
                        countryId = country.Data.Data[0].Id;
                    } else {
                        var countryDto = new CountryDTO(){
                            Name = addressRegister.CountryFull,
                            Acronym = addressRegister.Country,
                            Active = true,
                            CreatedAt = dateNow,
                            UpdatedAt = dateNow,
                            UserId = userId
                        };
                        await _countryService.CreateCountryAsync(countryDto);
                        country = await _countryService.GetCountriesAsync(countryFilterDb);
                        countryId = country.Data.Data[0].Id;
                    }
                }
                var addressFilterDb = new AddressFilterDb(){
                    CityId = cityId,
                    StateId = stateId,
                    CountryId = countryId,
                    UserId = userId,
                    Active = true,
                };
                var addresses = await _addressService.GetAddressesAsync(addressFilterDb);
                if(addresses.Data.TotalRegisters == 0){
                    
                    var addressDto = new AddressDTO(){
                        CityId = cityId,
                        StateId = stateId,
                        CountryId = countryId,
                        CreatedAt = dateNow,
                        UpdatedAt = dateNow,
                        UserId = userId,
                        Active = true,
                    };
                    await _addressService.CreateAddressAsync(addressDto);
                    addresses = await _addressService.GetAddressesAsync(addressFilterDb);
                    var personAddressDto = new PersonAddressDTO(){
                        PersonId = user.PersonId,
                        AddressId = addresses.Data.Data[0].Id,
                        UserId = userId,
                        CreatedAt = dateNow,
                        UpdatedAt = dateNow,
                        Active = true
                    };
                    await _personAddressService.CreatePersonAddressAsync(personAddressDto);
                    return Ok("Address has been successfully added.");
                }
                return Ok("Address aready has added.");
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message);
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
        [HttpGet("PersonAddress")]
        [Authorize]
        public async Task<ActionResult> GetPersonAddressesAsync([FromQuery]  PersonAddressFilterDb personAddressFilterDb)
        {
            var result = await _personAddressService.GetPersonAddressesAsync(personAddressFilterDb);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        //[HttpPost]
        //public async Task<ActionResult> AddPersonAddressAsync()
    }
}
