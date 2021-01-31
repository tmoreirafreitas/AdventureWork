using Microsoft.Extensions.Options;
using System;
using System.Data;

namespace AdventureWork.Infra.Data.Context
{
    public class DatabaseContext : IDatabaseContext
    {
        public IDbConnection Connection { get; }

        public DatabaseContext(IDbConnection dbConnection, IOptions<ConnectionStringsSetting> settings)
        {
            Connection = dbConnection;
            Connection.ConnectionString = settings.Value.DefaultConnection;
            Connection.Open();
        }

        public void Fechar()
        {
            if (Connection.State == ConnectionState.Open)
                Connection.Close();
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
