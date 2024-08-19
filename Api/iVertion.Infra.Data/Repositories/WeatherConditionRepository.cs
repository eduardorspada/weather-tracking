using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;
using iVertion.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iVertion.Infra.Data.Repositories
{
    public class WeatherConditionRepository : IWeatherConditionRepository
    {
        private readonly ApplicationDbContext _context;
        public WeatherConditionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<WeatherCondition> GetWeatherConditionByIdAsync(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var weatherCondition = await _context.WeatherConditions.FindAsync(id);
            return weatherCondition;
        }

        public async Task<PagedBaseResponse<WeatherCondition>> GetWeatherConditionsAsync(WeatherConditionFilterDb request)
        {
            var weatherConditions = _context.WeatherConditions.AsQueryable();
            if(request.CityName != null)
                weatherConditions = weatherConditions.Where(wc =>  wc.CityName.Contains(request.CityName));
            if(request.Description != null)
                weatherConditions = weatherConditions.Where(wc => wc.Description.Contains(request.Description));
            if(request.CityId > 0)
                weatherConditions = weatherConditions.Where(wc => wc.CityId == request.CityId);
            if((request.IntialDate != null || request.FinalDate != null) || (request.IntialDate != null && request.FinalDate != null))
            {
                if (request.IntialDate != null && request.FinalDate != null)
                {
                    weatherConditions = weatherConditions.Where(wc => wc.CreatedAt <= request.IntialDate && wc.CreatedAt >= request.FinalDate);
                }
                else
                {
                    if(request.IntialDate != null)
                    {
                        weatherConditions = weatherConditions.Where(wc => wc.CreatedAt <= request.IntialDate);
                    }
                    else
                    {
                        weatherConditions = weatherConditions.Where(wc => wc.CreatedAt >= request.FinalDate);
                    }
                }

            }
            if (request.Active)
                weatherConditions = weatherConditions.Where(wc => wc.Active == request.Active);
            if (!request.Active)
                weatherConditions = weatherConditions.Where(wc => wc.Active == request.Active);


            return await PagedBaseResponseHelper
                        .GetResponseAsync<PagedBaseResponse<WeatherCondition>, WeatherCondition>(weatherConditions, request);
        }
    }
}
