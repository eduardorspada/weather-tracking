using iVertion.Domain.Account;
using iVertion.Infra.Data.Identity;
using iVertion.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using iVertion.Application.Interfaces;
using iVertion.Domain.FiltersDb;
using iVertion.Application.DTOs;

namespace iVertion.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IUserInterface<ApplicationUser> _userService;
        /// <summary>
        /// Retuns persons information
        /// </summary>
        /// <param name="personService"></param>
        /// <param name="userService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public PersonController(IPersonService personService,
            IUserInterface<ApplicationUser> userService)
        {
            _personService = personService ??
                throw new ArgumentNullException(nameof(personService));
            _userService = userService ?? 
                throw new ArgumentNullException(nameof(userService));
        }
        /// <summary>
        /// Retuns person information
        /// </summary>
        /// <param name="personFilterDb"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "GetPersons")]
        public async Task<ActionResult> GetPersonsAsync([FromQuery] PersonFilterDb personFilterDb){
            var result = await _personService.GetPersonsAsync(personFilterDb);
            if(result.IsSuccess)
                return Ok(result);
            
            return BadRequest(result);
        }
        /// <summary>
        /// Retuns person information by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "GetPersons")]
        public async Task<ActionResult> GetPersonByIdAsync(int id){
            var result = await _personService.GetPersonByIdAsync(id);
            if (result.Data == null)
                return NotFound();
            if (result.IsSuccess){
                return Ok(result);
            }
            return BadRequest(result);
        }
        /// <summary>
        /// Retuns person information by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("MyPersonInformation")]
        [Authorize]
        public async Task<ActionResult> GetMyPersonInformationAsync(){
            var userId = User.FindFirst("UID").Value;
            var user = await _userService.GetUserByIdAsync(userId);
            var personId = user.PersonId;
            var result = await _personService.GetPersonByIdAsync(personId);
            if (result.Data == null)
                return NotFound();
            if (result.IsSuccess){
                return Ok(result);
            }
            return BadRequest(result);
        }
        /// <summary>
        /// Create person information by PersonDTO
        /// </summary>
        /// <param name="personDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddPersonAsync([FromBody] PersonDTO personDto){
            try
            {
                var userId = User.FindFirst("UID").Value;
                var user = await _userService.GetUserByIdAsync(userId);
                Console.WriteLine(user.PersonId);

                if (user.PersonId <= 0)
                {
                    var dateNow = DateTime.UtcNow;
                    personDto.UserId = userId;
                    personDto.CreatedAt = dateNow;
                    personDto.UpdatedAt = dateNow;
                    await _personService.CreatePersonAsync(personDto);

                    var personFilterDb = new PersonFilterDb();
                    personFilterDb.UserId = userId;

                    var personDtoResult = await _personService.GetPersonsAsync(personFilterDb);
                    var personId = personDtoResult.Data.Data[0].Id;
                    user.PersonId = personId;
                    await _userService.UpdateUserAsync(user);

                    return Ok(personDto);
                }
                return BadRequest("This user already has a user profile created.");

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}