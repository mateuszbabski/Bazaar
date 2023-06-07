namespace Shared.Abstractions.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        //Task ExecuteAsync(Func<Task> action);
    }
}
