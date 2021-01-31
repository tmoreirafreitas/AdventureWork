using System;
using System.Data;

namespace AdventureWork.Infra.Data.Context
{
    public interface IDatabaseContext : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
