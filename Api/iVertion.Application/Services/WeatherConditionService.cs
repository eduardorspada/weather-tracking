using AutoMapper;
using iVertion.Application.DTOs;
using iVertion.Application.Interfaces;
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;

namespace iVertion.Application.Services
{
    public class WeatherConditionService : IWeatherConditionService
    {
        private readonly IWeatherConditionRepository _weatherConditionRepository;
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public WeatherConditionService(IWeatherConditionRepository weatherConditionRepository,
                              IRepository repo,
                              IMapper mapper)
        {
            _weatherConditionRepository = weatherConditionRepository ??
                throw new ArgumentNullException(nameof(weatherConditionRepository));
            _repo = repo ??
                throw new ArgumentNullException(nameof(_repo));
            _mapper = mapper;
        }
        public async Task CreateWeatherConditionAsync(WeatherConditionDTO weatherConditionDto)
        {
            var weatherCondition = _mapper.Map<WeatherCondition>(weatherConditionDto);
            await _repo.CreateAsync(weatherCondition);
        }

        public async Task<ResultService<WeatherConditionDTO>> GetWeatherConditionByIdAsync(int id)
        {
            var weatherCondition = await _weatherConditionRepository.GetWeatherConditionByIdAsync(id);
            return ResultService.OK(_mapper.Map<WeatherConditionDTO>(weatherCondition));
        }

        public async Task<ResultService<PagedBaseResponseDTO<WeatherConditionDTO>>> GetWeatherConditionsAsync(WeatherConditionFilterDb weatherConditionFilterDb)
        {
            var weatherConditions = await _weatherConditionRepository.GetWeatherConditionsAsync(weatherConditionFilterDb);
            var result = new PagedBaseResponseDTO<WeatherConditionDTO>(
                weatherConditions.TotalRegisters,
                _mapper.Map<List<WeatherConditionDTO>>(weatherConditions.Data)
                );
            return ResultService.OK(result);
        }

        public async Task RemoveWeatherConditionAsync(int id)
        {
            var weatherConditionEntity = _weatherConditionRepository.GetWeatherConditionByIdAsync(id).Result;
            await _repo.RemoveAsync(weatherConditionEntity);
        }

        public async Task UpdateWeatherConditionAsync(WeatherConditionDTO weatherConditionDto)
        {
            var weatherConditionEntity = _mapper.Map<WeatherCondition>(weatherConditionDto);
            await _repo.UpdateAsync(weatherConditionEntity);
        }
    }
}
