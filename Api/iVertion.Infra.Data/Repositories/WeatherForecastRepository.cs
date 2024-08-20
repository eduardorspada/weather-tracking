using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;
using iVertion.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iVertion.Infra.Data.Repositories
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly ApplicationDbContext _context;
        public WeatherForecastRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<WeatherForecast> GetWeatherForecastByIdAsync(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var weatherForecast = await _context.WeatherForecasts.FindAsync(id);
            return weatherForecast;
        }

        public async Task<PagedBaseResponse<WeatherForecast>> GetWeatherForecastsAsync(WeatherForecastFilterDb request)
        {
            var weatherForecasts = _context.WeatherForecasts.AsQueryable();

            if(request.CityName != null)
                weatherForecasts = weatherForecasts.Where(wf => wf.CityName.Contains(request.CityName));
            if(request.Description != null)
                weatherForecasts = weatherForecasts.Where(wf => wf.Description.Contains(request.Description));
            if(request.CityId > 0)
                weatherForecasts = weatherForecasts.Where(wf => wf.CityId == request.CityId);
            if (request.IntialDate != null || request.FinalDate != null)
            {
                if (request.IntialDate != null && request.FinalDate != null)
                {
                    weatherForecasts = weatherForecasts.Where(wc => wc.Date >= request.IntialDate && wc.Date <= request.FinalDate);
                }
                else
                {
                    if (request.IntialDate != null)
                    {
                        weatherForecasts = weatherForecasts.Where(wc => wc.Date >= request.IntialDate);
                    }
                    else
                    {
                        weatherForecasts = weatherForecasts.Where(wc => wc.Date <= request.FinalDate);
                    }
                }

            }
            
            weatherForecasts = weatherForecasts.Where(wc => wc.Active == request.Active);

            return await PagedBaseResponseHelper
                        .GetResponseAsync<PagedBaseResponse<WeatherForecast>, WeatherForecast>(weatherForecasts, request);
        }
    }
}
