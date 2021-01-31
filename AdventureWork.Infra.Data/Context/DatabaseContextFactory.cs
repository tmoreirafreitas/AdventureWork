namespace AdventureWork.Infra.Data.Context
{
    public class DatabaseContextFactory : IDatabaseContextFactory
    {
        private readonly IDatabaseContext _dataContext;

        public DatabaseContextFactory(IDatabaseContext context)
        {
            _dataContext = context;
        }

        public IDatabaseContext Context()
        {
            return _dataContext;
        }

        public void Dispose()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }
    }
}
