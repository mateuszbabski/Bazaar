namespace Modules.Discounts.Domain.ValueObjects
{
    public enum DiscountType
    {
        AssignedToOrderTotal = 10,
        AssignedToProduct = 20,
        AssignedToVendors = 40,
        AssignedToShipping = 50,
        AssignedToAllProducts = 60,
        AssignedToCustomer = 70
    }
}
