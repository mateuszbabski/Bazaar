﻿using Microsoft.EntityFrameworkCore;
using Shared.Abstractions.DomainEvents;
using Shared.Domain;

namespace Shared.Infrastructure.DomainEvents
{
    public class DomainEventsTypeRegistry
    {
        private readonly Dictionary<string, Type> _types = new();

        public void Register<T>() where T : IDomainEventsAccessor => _types[GetKey<T>()] = typeof(T);

        public Type Resolve<T>() => _types.TryGetValue(GetKey<T>(), out var type) ? type : null;

        private static string GetKey<T>() => $"{typeof(T).Module.Name}";
    }
}