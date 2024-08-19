using AutoMapper;
using iVertion.Application.DTOs;
using iVertion.Application.Interfaces;
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;

namespace iVertion.Application.Services
{
    public class WeatherAlertService : IWeatherAlertService
    {
        private readonly IWeatherAlertRepository _weatherAlertRepository;
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public WeatherAlertService(IWeatherAlertRepository weatherAlertRepository,
                              IRepository repo,
                              IMapper mapper)
        {
            _weatherAlertRepository = weatherAlertRepository ??
                throw new ArgumentNullException(nameof(weatherAlertRepository));
            _repo = repo ??
                throw new ArgumentNullException(nameof(_repo));
            _mapper = mapper;
        }
        public async Task CreateWeatherAlertAsync(WeatherAlertDTO weatherAlertDto)
        {
            var weatherAlert = _mapper.Map<WeatherAlert>(weatherAlertDto);
            await _repo.CreateAsync(weatherAlert);
        }

        public async Task<ResultService<WeatherAlertDTO>> GetWeatherAlertByIdAsync(int id)
        {
            var weatherAlert = await _weatherAlertRepository.GetWeatherAlertByIdAsync(id);
            return ResultService.OK(_mapper.Map<WeatherAlertDTO>(weatherAlert));
        }

        public async Task<ResultService<PagedBaseResponseDTO<WeatherAlertDTO>>> GetWeatherAlertsAsync(WeatherAlertFilterDb weatherAlertFilterDb)
        {
            var weatherAlerts = await _weatherAlertRepository.GetWeatherAlertsAsync(weatherAlertFilterDb);
            var result = new PagedBaseResponseDTO<WeatherAlertDTO>(
                weatherAlerts.TotalRegisters,
                _mapper.Map<List<WeatherAlertDTO>>(weatherAlerts.Data)
                );
            return ResultService.OK(result);
        }

        public async Task RemoveWeatherAlertAsync(int id)
        {
            var weatherAlertEntity = _weatherAlertRepository.GetWeatherAlertByIdAsync(id).Result;
            await _repo.RemoveAsync(weatherAlertEntity);
        }

        public async Task UpdateWeatherAlertAsync(WeatherAlertDTO weatherAlertDto)
        {
            var weatherAlertEntity = _mapper.Map<WeatherAlert>(weatherAlertDto);
            await _repo.UpdateAsync(weatherAlertEntity);
        }
    }
}