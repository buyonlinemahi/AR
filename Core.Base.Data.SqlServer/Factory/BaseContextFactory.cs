using System;
using System.Data.Entity;

namespace Core.Base.Data.SqlServer.Factory
{
    public class BaseContextFactory<T> : Disposable, IContextFactory<T>
        where T : DbContext, new()
    {
        private string connName;
        private T dataContext;

        public T Get()
        {
            return dataContext ?? (string.IsNullOrEmpty(connName) ? dataContext = new T() : dataContext = Activator.CreateInstance(typeof(T), connName) as T);
        }

        public BaseContextFactory()
        {
        }

        /// <summary>
        /// Will use the value of connection string name inside app.config/web.config to connect to target db. Should have a context constructor that accepts connection string name(string).
        /// </summary>
        /// <param name="connStringName"></param>
        public BaseContextFactory(string connStringName)
        {
            connName = connStringName;
        }

        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}