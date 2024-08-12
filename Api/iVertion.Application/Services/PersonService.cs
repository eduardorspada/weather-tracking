
using AutoMapper;
using iVertion.Application.DTOs;
using iVertion.Application.Interfaces;
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;

namespace iVertion.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
        
        public PersonService(IPersonRepository personRepository,
                             IRepository repo,
                             IMapper mapper)
        {
            _personRepository = personRepository ??
                throw new ArgumentNullException(nameof(personRepository));
            _repo = repo ??
                throw new ArgumentNullException(nameof(repo));
            _mapper = mapper;
        }

        public async Task CreatePersonAsync(PersonDTO PersonDto)
        {
            var PersonEntity = _mapper.Map<Person>(PersonDto);
            await _repo.CreateAsync(PersonEntity);
        }

        public async Task<ResultService<PersonDTO>> GetPersonByIdAsync(int id)
        {
            var Person = await _personRepository.GetPersonByIdAsync(id);
            return ResultService.OK(_mapper.Map<PersonDTO>(Person));
        }

        public async Task<ResultService<PagedBaseResponseDTO<PersonDTO>>> GetPersonsAsync(PersonFilterDb PersonFilterDb)
        {
            var Persons = await _personRepository.GetPersonAsync(PersonFilterDb);
            var result = new PagedBaseResponseDTO<PersonDTO>(
                Persons.TotalRegisters,
                _mapper.Map<List<PersonDTO>>(Persons.Data)
            );

            return ResultService.OK(result);
        }

        public async Task RemovePersonAsync(int id)
        {
            var PersonEntity = _personRepository.GetPersonByIdAsync(id).Result;
            await _repo.RemoveAsync(PersonEntity);
        }

        public async Task UpdatePersonAsync(PersonDTO PersonDto)
        {
            var PersonEntity = _mapper.Map<Person>(PersonDto);
            await _repo.UpdateAsync(PersonEntity);
        }

    }
}