using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;
using iVertion.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iVertion.Infra.Data.Repositories
{
    public class WeatherNotificationRepository : IWeatherNotificationRepository
    {
        private readonly ApplicationDbContext _context;
        public WeatherNotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<WeatherNotification> GetWeatherNotificationByIdAsync(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var weatherNotification = await _context.WeatherNotifications.FindAsync(id);
            return weatherNotification;
        }

        public async Task<PagedBaseResponse<WeatherNotification>> GetWeatherNotificationsAsync(WeatherNotificationFilterDb request)
        {
            var weatherNotifications = _context.WeatherNotifications.AsQueryable();

            if(request.IsRead != null)
                weatherNotifications = weatherNotifications.Where(wn => wn.IsRead == request.IsRead);
            if(request.RetryCount >= 0)
                weatherNotifications = weatherNotifications.Where(wn => wn.RetryCount == request.RetryCount);
            if ((request.IntialSentAt != null || request.FinalSentAt != null) || (request.IntialSentAt != null && request.FinalSentAt != null))
            {
                if (request.IntialSentAt != null && request.FinalSentAt != null)
                {
                    weatherNotifications = weatherNotifications.Where(wc => wc.SentAt <= request.IntialSentAt && wc.SentAt >= request.FinalSentAt);
                }
                else
                {
                    if (request.IntialSentAt != null)
                    {
                        weatherNotifications = weatherNotifications.Where(wc => wc.SentAt <= request.IntialSentAt);
                    }
                    else
                    {
                        weatherNotifications = weatherNotifications.Where(wc => wc.SentAt >= request.FinalSentAt);
                    }
                }

            }
            if ((request.IntialNextRetryAt != null || request.FinalNextRetryAt != null) || (request.IntialNextRetryAt != null && request.FinalNextRetryAt != null))
            {
                if (request.IntialNextRetryAt != null && request.FinalNextRetryAt != null)
                {
                    weatherNotifications = weatherNotifications.Where(wc => wc.NextRetryAt <= request.IntialNextRetryAt && wc.NextRetryAt >= request.FinalNextRetryAt);
                }
                else
                {
                    if (request.IntialNextRetryAt != null)
                    {
                        weatherNotifications = weatherNotifications.Where(wc => wc.NextRetryAt <= request.IntialNextRetryAt);
                    }
                    else
                    {
                        weatherNotifications = weatherNotifications.Where(wc => wc.NextRetryAt >= request.FinalNextRetryAt);
                    }
                }

            }
            if(request.WeatherAlertId > 0)
                weatherNotifications = weatherNotifications.Where(wn => wn.WeatherAlertId == request.WeatherAlertId);
            if(request.DeviceId > 0)
                weatherNotifications = weatherNotifications.Where(wn => wn.DeviceId == request.DeviceId);
            if (request.Active != null)
                weatherNotifications = weatherNotifications.Where(wc => wc.Active == request.Active);
            return await PagedBaseResponseHelper
                        .GetResponseAsync<PagedBaseResponse<WeatherNotification>, WeatherNotification>(weatherNotifications, request);
        }
    }
}
