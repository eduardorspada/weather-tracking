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
    /// User device controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        private readonly IUserInterface<ApplicationUser> _userService;
        /// <summary>
        /// Users device builder.
        /// </summary>
        /// <param name="deviceService"></param>
        /// <param name="userService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public DeviceController(IDeviceService deviceService,
                                IUserInterface<ApplicationUser> userService)
        {
            _deviceService = deviceService ?? 
                throw new ArgumentNullException(nameof(deviceService));
            _userService = userService ?? 
                throw new ArgumentNullException( nameof(userService));
        }
        /// <summary>
        /// Returns all devices of the logged in user.
        /// </summary>
        /// <param name="deviceFilterDb"></param>
        /// <returns></returns>
        [HttpGet("GetMyDeviceAsync")]
        [Authorize]
        public async Task<ActionResult> GetMyDevicesAsync([FromQuery] DeviceFilterDb deviceFilterDb)
        {
            var userId = User.FindFirst("UID").Value;
            var user = await _userService.GetUserByIdAsync(userId);
            var personId = user.PersonId;
            deviceFilterDb.PersonId = personId;
            var result = await _deviceService.GetDevicesAsync(deviceFilterDb);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        /// <summary>
        /// Creates a device for the logged in user.
        /// </summary>
        /// <param name="deviceDto"></param>
        /// <returns></returns>
        [HttpPost("AddMyDevice")]
        [Authorize]
        public async Task<ActionResult> AddMyDevice([FromBody] DeviceDTO deviceDto)
        {
            try
            {
                var userId = User.FindFirst("UID").Value;
                var user = await _userService.GetUserByIdAsync(userId);
                var personId = user.PersonId;
                deviceDto.PersonId = personId;
                var dateNow = DateTime.UtcNow;
                deviceDto.CreatedAt = dateNow;
                deviceDto.UpdatedAt = dateNow;
                deviceDto.UserId = userId;

                await _deviceService.CreateDeviceAsync(deviceDto);

                return Ok(deviceDto);

            }
            catch (Exception ex) { 
                return BadRequest(ex);
            }
            

        }
    }
}
