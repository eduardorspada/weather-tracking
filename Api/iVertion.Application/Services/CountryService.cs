using AutoMapper;
using iVertion.Application.DTOs;
using iVertion.Application.Interfaces;
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;

namespace iVertion.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public CountryService(ICountryRepository countryRepository,
                              IRepository repo,
                              IMapper mapper)
        {
            _countryRepository = countryRepository ??
                throw new ArgumentNullException(nameof(countryRepository));
            _repo = repo ??
                throw new ArgumentNullException(nameof(_repo));
            _mapper = mapper;
        }
        public async Task CreateCountryAsync(CountryDTO countryDto)
        {
            var city = _mapper.Map<Country>(countryDto);
            await _repo.CreateAsync(city);
        }

        public async Task<ResultService<PagedBaseResponseDTO<CountryDTO>>> GetCountriesAsync(CountryFilterDb countryFilterDb)
        {
            var countries = await _countryRepository.GetCountryAsync(countryFilterDb);
            var result = new PagedBaseResponseDTO<CountryDTO>(
                countries.TotalRegisters,
                _mapper.Map<List<CountryDTO>>(countries.Data)
                );
            return ResultService.OK(result);
        }

        public async Task<ResultService<CountryDTO>> GetCountryByIdAsync(int id)
        {
            var country = await _countryRepository.GetCountryByIdAsync(id);
            return ResultService.OK(_mapper.Map<CountryDTO>(country));
        }

        public async Task RemoveCountryAsync(int id)
        {
            var countryEntity = _countryRepository.GetCountryByIdAsync(id).Result;
            await _repo.RemoveAsync(countryEntity);
        }

        public async Task UpdateCountryAsync(CountryDTO countryDto)
        {
            var countryEntity = _mapper.Map<Country>(countryDto);
            await _repo.UpdateAsync(countryEntity);
        }
    }
}
