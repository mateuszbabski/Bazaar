namespace Shared.Abstractions.Events
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}
