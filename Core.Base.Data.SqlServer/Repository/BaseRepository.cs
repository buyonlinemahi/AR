using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using Core.Base.Data.SqlServer.Factory;

namespace Core.Base.Data.SqlServer.Repository
{
    public class BaseRepository<T, C> : Base<T>
        where T : class
        where C : DbContext
    {
        private C dbContext;

        protected IContextFactory<C> ContextFactory { get; set; }

        protected C Context
        {
            get { return dbContext ?? (dbContext = ContextFactory.Get()); }
        }

        public BaseRepository(IContextFactory<C> contextFactory)
            : base(new BaseUnitOfWork<C>(contextFactory))
        {
            ContextFactory = contextFactory;
            this.dbset = Context.Set<T>();
        }

        public BaseRepository(IUnitOfWork unitOfWork, IContextFactory<C> contextFactory)
            : base(new BaseUnitOfWork<C>(contextFactory))
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            ContextFactory = contextFactory;
            //this.UnitOfWork = contextFactory; iunit needs to be in icontext for this to work.
            //this.UnitOfWork = unitOfWork;
            this.dbset = Context.Set<T>();
        }

        protected virtual void SetEntityState(object entity, EntityState entityState)
        {
            this.Context.Entry(entity).State = entityState;
        }

        public virtual int Update(T item)
        {
            this.dbset.Attach(item);
            dbContext.Entry(item).State = EntityState.Modified;
            //this.Context.SaveChanges();
            return this.UnitOfWork.Save();
        }

        public virtual int Update(T item, params Expression<Func<T, object>>[] properties)
        {
            this.dbset.Attach(item);
            DbEntityEntry<T> entry = dbContext.Entry(item);
            foreach (var property in properties)
            {
                entry.Property(property).IsModified = true;
            }
            return this.UnitOfWork.Save();
        }
    }
}