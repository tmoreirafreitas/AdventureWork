using System;

namespace AdventureWork.Infra.Data.Context
{
    public interface IDatabaseContextFactory : IDisposable
    {
        IDatabaseContext Context();
    }
}
