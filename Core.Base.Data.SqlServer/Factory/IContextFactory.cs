using System.Data.Entity;

namespace Core.Base.Data.SqlServer.Factory
{
    public interface IContextFactory<T> where T : DbContext
    {
        T Get();
    }
}