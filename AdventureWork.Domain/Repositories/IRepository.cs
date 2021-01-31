using AdventureWork.Domain.Entities;
using System;

namespace AdventureWork.Domain.Repositories
{
    public interface IRepository<T> : IDomainRepository, IDisposable where T : Entity, new()
    {
        
    }
}
