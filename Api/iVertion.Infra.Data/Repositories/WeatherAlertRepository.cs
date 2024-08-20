using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;
using iVertion.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iVertion.Infra.Data.Repositories
{
    public class WeatherAlertRepository : IWeatherAlertRepository
    {
        private readonly ApplicationDbContext _context;
        public WeatherAlertRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<WeatherAlert> GetWeatherAlertByIdAsync(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var weatherAlert = await _context.WeatherAlerts.FindAsync(id);
            return weatherAlert;
        }

        public async Task<PagedBaseResponse<WeatherAlert>> GetWeatherAlertsAsync(WeatherAlertFilterDb request)
        {
            var weatherAlerts = _context.WeatherAlerts.AsQueryable();

            if(request.CityName != null)
                weatherAlerts = weatherAlerts.Where(wa => wa.CityName.Contains(request.CityName));
            if(request.Message != null)
                weatherAlerts = weatherAlerts.Where(wa => wa.Message.Contains(request.Message));
            if(request.CityId > 0)
                weatherAlerts = weatherAlerts.Where(wa => wa.CityId == request.CityId);
            if (request.IntialAlertTime != null || request.FinalAlertTime != null)
            {
                if (request.IntialAlertTime != null && request.FinalAlertTime != null)
                {
                    weatherAlerts = weatherAlerts.Where(wc => wc.AlertTime >= request.IntialAlertTime && wc.AlertTime <= request.FinalAlertTime);
                }
                else
                {
                    if (request.IntialAlertTime != null)
                    {
                        weatherAlerts = weatherAlerts.Where(wc => wc.AlertTime >= request.IntialAlertTime);
                    }
                    else
                    {
                        weatherAlerts = weatherAlerts.Where(wc => wc.AlertTime <= request.FinalAlertTime);
                    }
                }

            }
            if (request.Active != null)
                weatherAlerts = weatherAlerts.Where(wc => wc.Active == request.Active);
            return await PagedBaseResponseHelper
                        .GetResponseAsync<PagedBaseResponse<WeatherAlert>, WeatherAlert>(weatherAlerts, request);
        }
    }
}
