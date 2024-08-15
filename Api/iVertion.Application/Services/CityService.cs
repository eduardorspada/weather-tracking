using AutoMapper;
using iVertion.Application.DTOs;
using iVertion.Application.Interfaces;
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;

namespace iVertion.Application.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public CityService(ICityRepository cityRepository,
                           IRepository repo,
                           IMapper mapper)
        {
            _cityRepository = cityRepository ??
                throw new ArgumentNullException(nameof(cityRepository));
            _repo = repo ?? 
                throw new ArgumentNullException(nameof(_repo));
            _mapper = mapper;
        }
        public async Task CreateCityAsync(CityDTO cityDto)
        {
            var city = _mapper.Map<City>(cityDto);
            await _repo.CreateAsync(city);
        }

        public async Task<ResultService<PagedBaseResponseDTO<CityDTO>>> GetCitiesAsync(CityFilterDb cityFilterDb)
        {
            var cities = await _cityRepository.GetCityAsync(cityFilterDb);
            var result = new PagedBaseResponseDTO<CityDTO>(
                cities.TotalRegisters,
                _mapper.Map<List<CityDTO>>(cities.Data)
                );
            return ResultService.OK(result);
        }

        public async Task<ResultService<CityDTO>> GetCityByIdAsync(int id)
        {
            var city = await _cityRepository.GetCityById(id);
            return ResultService.OK(_mapper.Map<CityDTO>(city));
        }

        public async Task RemoveCityAsync(int id)
        {
            var cityEntity = _cityRepository.GetCityById(id).Result;
            await _repo.RemoveAsync(cityEntity);
        }

        public async Task UpdateCityAsync(CityDTO cityDto)
        {
            var cityEntity = _mapper.Map<CityDTO>(cityDto);
            await _repo.UpdateAsync(cityEntity);
        }
    }
}
