using AutoMapper;
using iVertion.Application.DTOs;
using iVertion.Application.Interfaces;
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;

namespace iVertion.Application.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IWeatherForecastRepository _weatherForecastRepository;
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public WeatherForecastService(IWeatherForecastRepository weatherForecastRepository,
                              IRepository repo,
                              IMapper mapper)
        {
            _weatherForecastRepository = weatherForecastRepository ??
                throw new ArgumentNullException(nameof(weatherForecastRepository));
            _repo = repo ??
                throw new ArgumentNullException(nameof(_repo));
            _mapper = mapper;
        }
        public async Task CreateWeatherForecastAsync(WeatherForecastDTO weatherForecastDto)
        {
            var weatherForecast = _mapper.Map<WeatherForecast>(weatherForecastDto);
            await _repo.CreateAsync(weatherForecast);
        }

        public async Task<ResultService<WeatherForecastDTO>> GetWeatherForecastByIdAsync(int id)
        {
            var weatherForecast = await _weatherForecastRepository.GetWeatherForecastByIdAsync(id);
            return ResultService.OK(_mapper.Map<WeatherForecastDTO>(weatherForecast));
        }

        public async Task<ResultService<PagedBaseResponseDTO<WeatherForecastDTO>>> GetWeatherForecastsAsync(WeatherForecastFilterDb weatherForecastFilterDb)
        {
            var weatherForecasts = await _weatherForecastRepository.GetWeatherForecastsAsync(weatherForecastFilterDb);
            var result = new PagedBaseResponseDTO<WeatherForecastDTO>(
                weatherForecasts.TotalRegisters,
                _mapper.Map<List<WeatherForecastDTO>>(weatherForecasts.Data)
                );
            return ResultService.OK(result);
        }

        public async Task RemoveWeatherForecastAsync(int id)
        {
            var weatherForecastEntity = _weatherForecastRepository.GetWeatherForecastByIdAsync(id).Result;
            await _repo.RemoveAsync(weatherForecastEntity);
        }

        public async Task UpdateWeatherForecastAsync(WeatherForecastDTO weatherForecastDto)
        {
            var weatherForecastEntity = _mapper.Map<WeatherForecast>(weatherForecastDto);
            await _repo.UpdateAsync(weatherForecastEntity);
        }
    }
}