namespace RepositoryLayer.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private SqlDbContext dataContext;

        public SqlDbContext Get()
        {
            return dataContext ?? (dataContext = new SqlDbContext());
        }

        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}