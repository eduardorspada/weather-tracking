using iVertion.Application.DTOs;
using iVertion.Application.Interfaces;
using iVertion.Domain.Account;
using iVertion.Domain.FiltersDb;
using iVertion.Infra.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
                                 IDeviceService deviceService)
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

    }
}
