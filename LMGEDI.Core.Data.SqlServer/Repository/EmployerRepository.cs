using Core.Base.Data.SqlServer;
using Core.Base.Data.SqlServer.Factory;
using Core.Base.Data.SqlServer.Repository;
using LMGEDI.Core.Data.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
namespace LMGEDI.Core.Data.SqlServer.Repository
{
    public class EmployerRepository : BaseRepository<Employer,LMGEDIDBContext>,IEmployerRepository
    {
        public EmployerRepository(IContextFactory<LMGEDIDBContext> contextFactory) :
            base(new BaseUnitOfWork<LMGEDIDBContext>(contextFactory), contextFactory)
        {
        }

        public IEnumerable<EmployerSearch> GetAllEmployer(string EmployerName, int skip, int take)
        {
            SqlParameter _EmployerName = new SqlParameter("@EmployerName", EmployerName);
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);
            return Context.Database.SqlQuery<EmployerSearch>(Constant.StoredProcedureConst.EmployerRepositoryProcedure.GetAllEmployer, _EmployerName, _skip, _take);
        }

        public int GetAllEmployerCount(string EmployerName)
        {
            SqlParameter _EmployerName = new SqlParameter("@EmployerName", EmployerName);
            return Context.Database.SqlQuery<int>(Constant.StoredProcedureConst.EmployerRepositoryProcedure.GetAllEmployerCount, _EmployerName).SingleOrDefault();
        }

        public int CheckEmployerExist(string EmployerName)
        {
            SqlParameter _EmployerName = new SqlParameter("@EmployerName", EmployerName);
            return Context.Database.SqlQuery<int>(Constant.StoredProcedureConst.EmployerRepositoryProcedure.CheckEmployerExist, _EmployerName).SingleOrDefault();
        }
    }
}
