namespace Shared.Abstractions.Time
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
        DateTimeOffset UtcTimeNow { get; }
        DateTimeOffset LocalTimeNow { get; }
    }
}