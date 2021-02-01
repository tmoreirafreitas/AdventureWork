using Microsoft.Extensions.Options;
using System;
using System.Data;

namespace AdventureWork.Infra.Data.Context
{
    public class DatabaseContext : IDatabaseContext
    {
        public IDbConnection Connection { get; private set; }
        public IDbTransaction Transaction { get; private set; }

        public DatabaseContext(IDbConnection dbConnection, IOptions<ConnectionStringsSetting> settings)
        {
            Connection = dbConnection;
            Connection.ConnectionString = settings.Value.DefaultConnection;
            Connection.Open();
        }

        public void BeginTransaction(IsolationLevel level = IsolationLevel.Snapshot)
        {
            Transaction = Connection.BeginTransaction(level);
        }

        public void EndTransaction(Exception exception = null)
        {
            if (exception == null)
            {
                Transaction.Commit();
            }
            else
            {
                Transaction.Rollback();
            }
        }

        private void Fechar()
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
                Transaction = null;
            }

            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
                Connection.Dispose();
                Connection = null;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Fechar();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
