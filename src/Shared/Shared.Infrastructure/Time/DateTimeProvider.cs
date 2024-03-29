﻿using Shared.Abstractions.Time;

namespace Shared.Infrastructure.Time
{
    internal sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
        public DateTimeOffset UtcTimeNow => DateTimeOffset.UtcNow;
        public DateTimeOffset LocalTimeNow => DateTimeOffset.Now;
    }
}
