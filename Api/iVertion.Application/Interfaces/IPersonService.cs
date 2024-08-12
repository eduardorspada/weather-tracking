
using iVertion.Application.DTOs;
using iVertion.Application.Services;
using iVertion.Domain.FiltersDb;

namespace iVertion.Application.Interfaces
{
    public interface IPersonService
    {
         Task<ResultService<PagedBaseResponseDTO<PersonDTO>>> GetPersonsAsync(PersonFilterDb PersonFilterDb);
         Task<ResultService<PersonDTO>> GetPersonByIdAsync(int id);
         Task CreatePersonAsync(PersonDTO PersonDto);
         Task UpdatePersonAsync(PersonDTO PersonDto);
         Task RemovePersonAsync(int id);
    }
}