namespace Modules.Products.Contracts.Interfaces
{
    public interface IProductChecker
    {
        Task<bool> IsProductExisting(Guid userId, Guid? discountTargetId);
    }
}
