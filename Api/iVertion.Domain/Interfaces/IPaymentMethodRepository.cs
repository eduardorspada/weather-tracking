
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;

namespace iVertion.Domain.Interfaces
{
    public interface IPaymentMethodRepository
    {
        Task<PagedBaseResponse<PaymentMethod>> GetPaymentMethodAsync(PaymentMethodFilterDb request);
        Task<PaymentMethod> GetPaymentMethodByIdAsync(int id);
    }
}