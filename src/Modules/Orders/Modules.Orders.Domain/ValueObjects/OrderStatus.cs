namespace Modules.Orders.Domain.ValueObjects
{
    public enum OrderStatus
    {
        Created = 0,
        Preparing = 10,
        PreparedToBeShipped = 20,
        Shipped = 30,
        Delivered = 40,
        Cancelled = 50
    }
}
