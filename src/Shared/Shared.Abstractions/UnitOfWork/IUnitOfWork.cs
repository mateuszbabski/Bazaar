﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain;

namespace Shared.Abstractions.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> CommitChangesAsync();
        Task CommitAndDispatchDomainEventsAsync<TEntity>(TEntity entity) 
            where TEntity : Entity;
    }

    public interface IUnitOfWork<T> : IUnitOfWork 
        where T : DbContext
    {
    }
}
