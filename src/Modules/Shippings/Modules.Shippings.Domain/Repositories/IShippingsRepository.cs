using Modules.Shippings.Domain.Entities;

namespace Modules.Shippings.Domain.Repositories
{
    public interface IShippingsRepository
    {
        Task<Shipping> CreateShipping(Shipping shipping);
        Task DeleteShipping(Shipping shipping);
        Task<Shipping> GetShippingById(Guid id);
        Task<IEnumerable<Shipping>> GetShippings();
        Task<Shipping> GetShippingByTrackingNumber(string trackingNumber);
        Task<Shipping> GetShippingByOrderId(Guid orderId);


    }
}
