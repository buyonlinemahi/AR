using System.Data.Entity;
using LMGEDI.Core.Data.SqlServer.Configuration;

namespace LMGEDI.Core.Data.SqlServer
{
    public class LMGEDIDBContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(UserConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public virtual void Save()
        {
            base.SaveChanges();
        }
    }
}
