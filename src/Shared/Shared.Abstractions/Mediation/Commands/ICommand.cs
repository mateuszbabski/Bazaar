namespace Shared.Abstractions.Mediation.Commands
{
    public interface ICommand
    {
    }

    public interface IRequest<T> : ICommand
    {
    }
}
