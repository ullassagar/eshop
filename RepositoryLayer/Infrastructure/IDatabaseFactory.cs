using System;

namespace RepositoryLayer.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        SqlDbContext Get();
    }
}