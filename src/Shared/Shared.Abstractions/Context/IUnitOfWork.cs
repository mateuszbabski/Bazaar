namespace Shared.Abstractions.Context
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
