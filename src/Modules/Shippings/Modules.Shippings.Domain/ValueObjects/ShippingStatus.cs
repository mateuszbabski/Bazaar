namespace Modules.Shippings.Domain.ValueObjects
{
    public enum ShippingStatus
    {
        ShippingNotRequired = 0,
        Pending = 10,
        PreparedToShipped = 20,
        Shipped = 30,
        Delivered = 40,
        Cancelled = 50
    }
}
