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
        /// <summary>
        /// Retuns persons information
        /// </summary>
        /// <param name="personService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public PersonController(IPersonService personService)
        {
            _personService = personService ??
                throw new ArgumentNullException(nameof(personService));
        }
        /// <summary>
        /// Retuns person information
        /// </summary>
        /// <param name="personFilterDb"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
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
        [Authorize]
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
        /// Create person information by PersonDTO
        /// </summary>
        /// <param name="personDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddPersonAsync([FromBody] PersonDTO personDto){
            try
            {
                var dateNow = DateTime.UtcNow;
                personDto.CreatedAt = dateNow;
                personDto.UpdatedAt = dateNow;
                await _personService.CreatePersonAsync(personDto);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}