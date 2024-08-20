using FirebaseAdmin.Messaging;
using iVertion.Application.DTOs;
using iVertion.Application.Interfaces;
using iVertion.Domain.Account;
using iVertion.Domain.FiltersDb;
using iVertion.Infra.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iVertion.WebApi.Controllers
{
    /// <summary>
    /// Weather controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherConditionService _weatherConditionService;
        private readonly IWeatherForecastService _weatherForecastService;
        private readonly IWeatherAlertService _weatherAlertService;
        private readonly IWeatherNotificationService _weatherNotificationService;
        private readonly IUserInterface<ApplicationUser> _userService;
        private readonly IDeviceService _deviceService;
        private readonly IPersonService _personService;
        private readonly IPersonAddressService _personAddressService;
        private readonly IAddressService _addressService;

        /// <summary>
        /// Weather builder.
        /// </summary>
        /// <param name="weatherConditionService"></param>
        /// <param name="weatherForecastService"></param>
        /// <param name="weatherAlertService"></param>
        /// <param name="weatherNotificationService"></param>
        /// <param name="userSevice"></param>
        /// <param name="deviceService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public WeatherController(IWeatherConditionService weatherConditionService,
                                 IWeatherForecastService weatherForecastService,
                                 IWeatherAlertService weatherAlertService,
                                 IWeatherNotificationService weatherNotificationService,
                                 IUserInterface<ApplicationUser> userSevice,
                                 IDeviceService deviceService,
                                 IPersonService personService,
                                 IPersonAddressService personAddressService,
                                 IAddressService addressService)
        {
            _weatherConditionService = weatherConditionService ??
                throw new ArgumentNullException(nameof(weatherConditionService));
            _weatherForecastService = weatherForecastService ??
                throw new ArgumentNullException(nameof(_weatherForecastService));
            _weatherAlertService = weatherAlertService ??
                throw new ArgumentNullException(nameof(_weatherAlertService));
            _weatherNotificationService = weatherNotificationService ??
                throw new ArgumentNullException(nameof(_weatherNotificationService));
            _userService = userSevice ??
                throw new ArgumentNullException(nameof(userSevice));
            _deviceService = deviceService ??
                throw new ArgumentNullException(nameof(deviceService));
            _personService = personService ??
                throw new ArgumentNullException(nameof(personService));
            _personAddressService = personAddressService ??
                throw new ArgumentNullException(nameof(personAddressService));
            _addressService = addressService ??
                throw new ArgumentNullException(nameof(addressService));
        }
        /// <summary>
        /// Returns the weather condition.
        /// </summary>
        /// <param name="weatherConditionFilterDb"></param>
        /// <returns></returns>
        [HttpGet("GetWeatherConditionsAsync")]
        [Authorize]
        public async Task<ActionResult> GetWeatherConditionsAsync([FromQuery] WeatherConditionFilterDb weatherConditionFilterDb)
        {
            var result = await _weatherConditionService.GetWeatherConditionsAsync(weatherConditionFilterDb);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        /// <summary>
        /// Adds a weather condition.
        /// </summary>
        /// <param name="weatherConditionDto"></param>
        /// <returns></returns>
        [HttpPost("AddWeatherConditionsAsync")]
        [Authorize(Roles = "AddWeather")]
        public async Task<ActionResult> AddWeatherConditionsAsync([FromBody] WeatherConditionDTO weatherConditionDto)
        {
            try
            {
                var userId = User.FindFirst("UID").Value;
                var dateNow = DateTime.UtcNow;
                weatherConditionDto.CreatedAt = dateNow;
                weatherConditionDto.UpdatedAt = dateNow;
                weatherConditionDto.UserId = userId;

                await _weatherConditionService.CreateWeatherConditionAsync(weatherConditionDto);

                return Ok(weatherConditionDto);
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Updates a weather condition.
        /// </summary>
        /// <param name="weatherConditionDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateWeatherConditionsAsync")]
        [Authorize(Roles = "AddWeather")]
        public async Task<ActionResult> UpdateWeatherConditionsAsync([FromBody] WeatherConditionDTO weatherConditionDto)
        {
            try
            {
                var weatherCondition = await _weatherConditionService.GetWeatherConditionByIdAsync(weatherConditionDto.Id);
                if (weatherCondition.IsSuccess)
                {
                    var userId = User.FindFirst("UID").Value;
                    var dateNow = DateTime.UtcNow;
                    weatherConditionDto.CreatedAt = weatherCondition.Data.CreatedAt;
                    weatherConditionDto.UpdatedAt = dateNow;
                    weatherConditionDto.UserId = userId;
                    await _weatherConditionService.UpdateWeatherConditionAsync(weatherConditionDto);

                    return Ok(weatherConditionDto);
                } else
                {
                    return BadRequest($@"There is no weather condition with this id {weatherConditionDto.Id}.");
                }
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Returns the weather Forecast.
        /// </summary>
        /// <param name="weatherForecastFilterDb"></param>
        /// <returns></returns>
        [HttpGet("GetWeatherForecastsAsync")]
        [Authorize]
        public async Task<ActionResult> GetWeatherForecastsAsync([FromQuery] WeatherForecastFilterDb weatherForecastFilterDb)
        {
            var result = await _weatherForecastService.GetWeatherForecastsAsync(weatherForecastFilterDb);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        /// <summary>
        /// Adds a weather Forecast.
        /// </summary>
        /// <param name="weatherForecastDto"></param>
        /// <returns></returns>
        [HttpPost("AddWeatherForecastsAsync")]
        [Authorize(Roles = "AddWeather")]
        public async Task<ActionResult> AddWeatherForecastsAsync([FromBody] WeatherForecastDTO weatherForecastDto)
        {
            try
            {
                var userId = User.FindFirst("UID").Value;
                var dateNow = DateTime.UtcNow;
                weatherForecastDto.CreatedAt = dateNow;
                weatherForecastDto.UpdatedAt = dateNow;
                weatherForecastDto.UserId = userId;

                await _weatherForecastService.CreateWeatherForecastAsync(weatherForecastDto);

                return Ok(weatherForecastDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Updates a weather Forecast.
        /// </summary>
        /// <param name="weatherForecastDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateWeatherForecastsAsync")]
        [Authorize(Roles = "AddWeather")]
        public async Task<ActionResult> UpdateWeatherForecastsAsync([FromBody] WeatherForecastDTO weatherForecastDto)
        {
            try
            {
                var weatherForecast = await _weatherForecastService.GetWeatherForecastByIdAsync(weatherForecastDto.Id);
                if (weatherForecast.IsSuccess)
                {
                    var userId = User.FindFirst("UID").Value;
                    var dateNow = DateTime.UtcNow;
                    weatherForecastDto.CreatedAt = weatherForecast.Data.CreatedAt;
                    weatherForecastDto.UpdatedAt = dateNow;
                    weatherForecastDto.UserId = userId;
                    await _weatherForecastService.UpdateWeatherForecastAsync(weatherForecastDto);

                    return Ok(weatherForecastDto);
                }
                else
                {
                    return BadRequest($@"There is no weather Forecast with this id {weatherForecastDto.Id}.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Returns the weather Alert.
        /// </summary>
        /// <param name="weatherAlertFilterDb"></param>
        /// <returns></returns>
        [HttpGet("GetWeatherAlertsAsync")]
        [Authorize]
        public async Task<ActionResult> GetWeatherAlertsAsync([FromQuery] WeatherAlertFilterDb weatherAlertFilterDb)
        {
            var result = await _weatherAlertService.GetWeatherAlertsAsync(weatherAlertFilterDb);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        /// <summary>
        /// Adds a weather Alert.
        /// </summary>
        /// <param name="weatherAlertDto"></param>
        /// <returns></returns>
        [HttpPost("AddWeatherAlertsAsync")]
        [Authorize(Roles = "AddWeather")]
        public async Task<ActionResult> AddWeatherAlertsAsync([FromBody] WeatherAlertDTO weatherAlertDto)
        {
            try
            {
                var userId = User.FindFirst("UID").Value;
                var dateNow = DateTime.UtcNow;
                weatherAlertDto.CreatedAt = dateNow;
                weatherAlertDto.UpdatedAt = dateNow;
                weatherAlertDto.UserId = userId;

                await _weatherAlertService.CreateWeatherAlertAsync(weatherAlertDto);

                return Ok(weatherAlertDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Updates a weather Alert.
        /// </summary>
        /// <param name="weatherAlertDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateWeatherAlertsAsync")]
        [Authorize(Roles = "AddWeather")]
        public async Task<ActionResult> UpdateWeatherAlertsAsync([FromBody] WeatherAlertDTO weatherAlertDto)
        {
            try
            {
                var weatherAlert = await _weatherAlertService.GetWeatherAlertByIdAsync(weatherAlertDto.Id);
                if (weatherAlert.IsSuccess)
                {
                    var userId = User.FindFirst("UID").Value;
                    var dateNow = DateTime.UtcNow;
                    weatherAlertDto.CreatedAt = weatherAlert.Data.CreatedAt;
                    weatherAlertDto.UpdatedAt = dateNow;
                    weatherAlertDto.UserId = userId;
                    await _weatherAlertService.UpdateWeatherAlertAsync(weatherAlertDto);

                    return Ok(weatherAlertDto);
                }
                else
                {
                    return BadRequest($@"There is no weather Alert with this id {weatherAlertDto.Id}.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Returns the weather Notification.
        /// </summary>
        /// <param name="weatherNotificationFilterDb"></param>
        /// <returns></returns>
        [HttpGet("GetWeatherNotificationsAsync")]
        [Authorize]
        public async Task<ActionResult> GetWeatherNotificationsAsync([FromQuery] WeatherNotificationFilterDb weatherNotificationFilterDb)
        {
            var result = await _weatherNotificationService.GetWeatherNotificationsAsync(weatherNotificationFilterDb);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        /// <summary>
        /// Adds a weather Notification.
        /// </summary>
        /// <param name="weatherNotificationDto"></param>
        /// <returns></returns>
        [HttpPost("AddWeatherNotificationsAsync")]
        [Authorize(Roles = "AddWeather")]
        public async Task<ActionResult> AddWeatherNotificationsAsync([FromBody] WeatherNotificationDTO weatherNotificationDto)
        {
            try
            {
                var userId = User.FindFirst("UID").Value;
                var dateNow = DateTime.UtcNow;
                weatherNotificationDto.CreatedAt = dateNow;
                weatherNotificationDto.UpdatedAt = dateNow;
                weatherNotificationDto.UserId = userId;

                await _weatherNotificationService.CreateWeatherNotificationAsync(weatherNotificationDto);

                return Ok(weatherNotificationDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Updates a weather Notification.
        /// </summary>
        /// <param name="weatherNotificationDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateWeatherNotificationsAsync")]
        [Authorize(Roles = "AddWeather")]
        public async Task<ActionResult> UpdateWeatherNotificationsAsync([FromBody] WeatherNotificationDTO weatherNotificationDto)
        {
            try
            {
                var weatherNotification = await _weatherNotificationService.GetWeatherNotificationByIdAsync(weatherNotificationDto.Id);
                if (weatherNotification.IsSuccess)
                {
                    var userId = User.FindFirst("UID").Value;
                    var dateNow = DateTime.UtcNow;
                    weatherNotificationDto.CreatedAt = weatherNotification.Data.CreatedAt;
                    weatherNotificationDto.UpdatedAt = dateNow;
                    weatherNotificationDto.UserId = userId;
                    await _weatherNotificationService.UpdateWeatherNotificationAsync(weatherNotificationDto);

                    return Ok(weatherNotificationDto);
                }
                else
                {
                    return BadRequest($@"There is no weather Notification with this id {weatherNotificationDto.Id}.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Updates the logged user's time notification.
        /// </summary>
        /// <param name="weatherNotificationDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateMyWeatherNotificationsAsync")]
        [Authorize]
        public async Task<ActionResult> UpdateMyWeatherNotificationsAsync([FromBody] WeatherNotificationDTO weatherNotificationDto)
        {
            try
            {
                var userId = User.FindFirst("UID").Value;
                var device = await _deviceService.GetDeviceByIdAsync(weatherNotificationDto.DeviceId);
                var user = await _userService.GetUserByIdAsync(userId);
                if (device.IsSuccess)
                {
                    if (user.PersonId == device.Data.PersonId)
                    {
                        var weatherNotification = await _weatherNotificationService.GetWeatherNotificationByIdAsync(weatherNotificationDto.Id);
                        if (weatherNotification.IsSuccess)
                        {
                            var dateNow = DateTime.UtcNow;
                            weatherNotificationDto.CreatedAt = weatherNotification.Data.CreatedAt;
                            weatherNotificationDto.UpdatedAt = dateNow;
                            weatherNotificationDto.UserId = userId;
                            await _weatherNotificationService.UpdateWeatherNotificationAsync(weatherNotificationDto);

                            return Ok(weatherNotificationDto);
                        }
                        return BadRequest($@"There is no weather Notification with this id {weatherNotificationDto.Id}.");
                        
                    }
                    return BadRequest("The device does not belong to this user.");
                }
                return BadRequest($@"There is no device with this id {weatherNotificationDto.DeviceId}.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task RunSetNextNotificationsAsync(){
        
          await SetNextNotificationsAsync();
        
        }
        private async Task SetNextNotificationsAsync() {
            var dateNow = DateTime.UtcNow;
            var weatherAlertFilterDb = new WeatherAlertFilterDb(){
                Active = true,
                IntialAlertTime = dateNow.AddMinutes(-1)
            };
            
            
            var weatherAlerts = await _weatherAlertService.GetWeatherAlertsAsync(weatherAlertFilterDb);
            foreach (WeatherAlertDTO weatherAlertDto in weatherAlerts.Data.Data){
                int cityId = weatherAlertDto.CityId;
                int weatherAlertId = weatherAlertDto.Id;
                string? message = weatherAlertDto.Message;
                

                var addressFilterDb = new AddressFilterDb(){
                    Active = true,
                    CityId = cityId
                };
                
                var addresses = await _addressService.GetAddressesAsync(addressFilterDb);
                foreach(AddressDTO addressDto in addresses.Data.Data){
                    var personAddressFilterDb = new PersonAddressFilterDb(){
                        AddressId = addressDto.Id,
                        Active = true
                    };
                    
                    var personAddresses = await _personAddressService.GetPersonAddressesAsync(personAddressFilterDb);
                    foreach(PersonAddressDTO personAddressDto in personAddresses.Data.Data){
                        var deviceFilterDb = new DeviceFilterDb(){
                            Active = true,
                            AcceptNotifications = true,
                            PersonId = personAddressDto.PersonId
                        };
                        
                        var devices = await _deviceService.GetDevicesAsync(deviceFilterDb);
                        foreach(DeviceDTO deviceDto in devices.Data.Data){
                            var weatherNotificationDto = new WeatherNotificationDTO(){
                                IsRead = false,
                                RetryCount = 0,
                                SentAt = dateNow,
                                NextRetryAt = dateNow.AddMinutes(30),
                                WeatherAlertId = weatherAlertDto.Id,
                                DeviceId = deviceDto.Id,
                                Active = true
                            };
                            
                            await _weatherNotificationService.CreateWeatherNotificationAsync(weatherNotificationDto);
                        }
                    }
                }

            }
        }
        [HttpPost("RunSendNotificationAsync")]
        [Authorize]
        public async Task RunSendNotificationAsync(){
            var dateNow = DateTime.UtcNow;
            var weatherNotificationFilterDb = new WeatherNotificationFilterDb(){
                MaxRetryCount = 9,
                IsRead = false,
                Active = true,
                IntialNextRetryAt = dateNow.AddMinutes(-1),
                FinalNextRetryAt = dateNow.AddMinutes(1)
            };
            
            Console.WriteLine("Weather Notifications");

            var weatherNotifications = await _weatherNotificationService.GetWeatherNotificationsAsync(weatherNotificationFilterDb);
            foreach (WeatherNotificationDTO weatherNotificationDto in weatherNotifications.Data.Data){
                var device = await _deviceService.GetDeviceByIdAsync(weatherNotificationDto.DeviceId);
                var alert = await _weatherAlertService.GetWeatherAlertByIdAsync(weatherNotificationDto.WeatherAlertId);
                weatherNotificationDto.RetryCount++;
                weatherNotificationDto.NextRetryAt = dateNow.AddMinutes(10-weatherNotificationDto.RetryCount);
                Console.WriteLine(weatherNotificationDto.NextRetryAt);
                await _weatherNotificationService.UpdateWeatherNotificationAsync(weatherNotificationDto);
                await SendNotificationAsync(device.Data.Token, "Weather Tracking", alert.Data.Message);
            }
        }
        private async Task<string> SendNotificationAsync(string deviceToken, string title, string body){
            
            var message = new Message()
            {
                Token = deviceToken,
                Notification = new Notification()
                {
                    Title = title,
                    Body = body
                },
            };
            Console.WriteLine("Enviando notificação");
            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            Console.WriteLine("Notificação enviada");
            Console.WriteLine(response);
            return response;
        }
    }
}
