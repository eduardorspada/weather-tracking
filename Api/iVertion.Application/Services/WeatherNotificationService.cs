using AutoMapper;
using iVertion.Application.DTOs;
using iVertion.Application.Interfaces;
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;

namespace iVertion.Application.Services
{
    public class WeatherNotificationService : IWeatherNotificationService
    {
        private readonly IWeatherNotificationRepository _weatherNotificationRepository;
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public WeatherNotificationService(IWeatherNotificationRepository weatherNotificationRepository,
                              IRepository repo,
                              IMapper mapper)
        {
            _weatherNotificationRepository = weatherNotificationRepository ??
                throw new ArgumentNullException(nameof(weatherNotificationRepository));
            _repo = repo ??
                throw new ArgumentNullException(nameof(_repo));
            _mapper = mapper;
        }
        public async Task CreateWeatherNotificationAsync(WeatherNotificationDTO weatherNotificationDto)
        {
            var weatherNotification = _mapper.Map<WeatherNotification>(weatherNotificationDto);
            await _repo.CreateAsync(weatherNotification);
        }

        public async Task<ResultService<WeatherNotificationDTO>> GetWeatherNotificationByIdAsync(int id)
        {
            var weatherNotification = await _weatherNotificationRepository.GetWeatherNotificationByIdAsync(id);
            return ResultService.OK(_mapper.Map<WeatherNotificationDTO>(weatherNotification));
        }

        public async Task<ResultService<PagedBaseResponseDTO<WeatherNotificationDTO>>> GetWeatherNotificationsAsync(WeatherNotificationFilterDb weatherNotificationFilterDb)
        {
            var weatherNotifications = await _weatherNotificationRepository.GetWeatherNotificationsAsync(weatherNotificationFilterDb);
            var result = new PagedBaseResponseDTO<WeatherNotificationDTO>(
                weatherNotifications.TotalRegisters,
                _mapper.Map<List<WeatherNotificationDTO>>(weatherNotifications.Data)
                );
            return ResultService.OK(result);
        }

        public async Task RemoveWeatherNotificationAsync(int id)
        {
            var weatherNotificationEntity = _weatherNotificationRepository.GetWeatherNotificationByIdAsync(id).Result;
            await _repo.RemoveAsync(weatherNotificationEntity);
        }

        public async Task UpdateWeatherNotificationAsync(WeatherNotificationDTO weatherNotificationDto)
        {
            var weatherNotificationEntity = _mapper.Map<WeatherNotification>(weatherNotificationDto);
            await _repo.UpdateAsync(weatherNotificationEntity);
        }
    }
}