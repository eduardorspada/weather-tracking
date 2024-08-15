using AutoMapper;
using iVertion.Application.DTOs;
using iVertion.Application.Interfaces;
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;

namespace iVertion.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository addressRepository,
                              IRepository repo,
                              IMapper mapper)
        {
            _addressRepository = addressRepository ?? 
                throw new ArgumentNullException(nameof(addressRepository));
            _repo = repo ?? 
                throw new ArgumentNullException(nameof(_repo));
            _mapper = mapper;
        }
        public async Task CreateAddressAsync(AddressDTO addressDto)
        {
            var address = _mapper.Map<Address>(addressDto);
            await _repo.CreateAsync(address);
        }

        public async Task<ResultService<AddressDTO>> GetAddressByIdAsync(int id)
        {
            var address = await _addressRepository.GetAddressByIdAsync(id);
            return ResultService.OK(_mapper.Map<AddressDTO>(address));
        }

        public async Task<ResultService<PagedBaseResponseDTO<AddressDTO>>> GetAddressesAsync(AddressFilterDb addressFilterDb)
        {
            var addresses = await _addressRepository.GetAddressAsync(addressFilterDb);
            var result = new PagedBaseResponseDTO<AddressDTO>(
                addresses.TotalRegisters,
                _mapper.Map<List<AddressDTO>>(addresses.Data)
                );
            return ResultService.OK(result);
        }

        public async Task RemoveAddressAsync(int id)
        {
            var addressEntity = _addressRepository.GetAddressByIdAsync(id).Result;
            await _repo.RemoveAsync(addressEntity);
        }

        public async Task UpdateAddressAsync(AddressDTO addressDto)
        {
            var addressEntity = _mapper.Map<Address>(addressDto);
            await _repo.UpdateAsync(addressEntity);
        }
    }
}
