using System;
using System.Data;

namespace AdventureWork.Infra.Data.Context
{
    public interface IDatabaseContext : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; }
        void BeginTransaction(IsolationLevel level = IsolationLevel.Snapshot);
        void EndTransaction(Exception exception = null);
    }
}
