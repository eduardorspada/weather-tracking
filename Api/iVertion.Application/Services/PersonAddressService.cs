using AutoMapper;
using iVertion.Application.DTOs;
using iVertion.Application.Interfaces;
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;

namespace iVertion.Application.Services
{
    public class PersonAddressService : IPersonAddressService
    {
        private readonly IPersonAddressRepository _personAddressRepository;
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public PersonAddressService(IPersonAddressRepository personAddressRepository,
                                    IRepository repo,
                                    IMapper mapper)
        {
            _personAddressRepository = personAddressRepository ??
                throw new ArgumentNullException(nameof(personAddressRepository));
            _repo = repo ?? 
                throw new ArgumentNullException(nameof(_repo));
            _mapper = mapper;
        }
        public async Task CreatePersonAddressAsync(PersonAddressDTO personAddressDto)
        {
            var personAddress = _mapper.Map<PersonAddress>(personAddressDto);
            await _repo.CreateAsync(personAddress);
        }

        public async Task<ResultService<PersonAddressDTO>> GetPersonAddressByIdAsync(int id)
        {
            var personAddress = await _personAddressRepository.GetPersonAddressByIdAsync(id);
            return ResultService.OK(_mapper.Map<PersonAddressDTO>(personAddress));
        }

        public async Task<ResultService<PagedBaseResponseDTO<PersonAddressDTO>>> GetPersonAddressesAsync(PersonAddressFilterDb personAddressFilterDb)
        {
            var personAddresses = await _personAddressRepository.GetPersonAddressesAsync(personAddressFilterDb);
            var result = new PagedBaseResponseDTO<PersonAddressDTO>(
                personAddresses.TotalRegisters,
                _mapper.Map<List<PersonAddressDTO>>(personAddresses.Data)
                );
            return ResultService.OK(result);
        }

        public async Task RemovePersonAddressAsync(int id)
        {
            var personAddressEntity = _personAddressRepository.GetPersonAddressByIdAsync(id).Result;
            await _repo.RemoveAsync(personAddressEntity);
        }

        public async Task UpdatePersonAddressAsync(PersonAddressDTO personAddressDto)
        {
            var personAddresEntity = _mapper.Map<PersonAddress>(personAddressDto);
            await _repo.UpdateAsync(personAddresEntity);
        }
    }
}
