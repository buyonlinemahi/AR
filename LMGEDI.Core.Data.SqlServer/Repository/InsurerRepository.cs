using Core.Base.Data.SqlServer;
using Core.Base.Data.SqlServer.Factory;
using Core.Base.Data.SqlServer.Repository;
using LMGEDI.Core.Data.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


namespace LMGEDI.Core.Data.SqlServer.Repository
{
    public class InsurerRepository : BaseRepository<Insurer,LMGEDIDBContext>,IInsurerRepository
    {
        public InsurerRepository(IContextFactory<LMGEDIDBContext> contextFactory) :
            base(new BaseUnitOfWork<LMGEDIDBContext>(contextFactory),contextFactory)
        {
        }

        public IEnumerable<InsurerSearch> GetAllInsurer(string InsurerName, int skip, int take)
        {
            SqlParameter _InsurerName = new SqlParameter("@InsurerName", InsurerName);
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);
            return Context.Database.SqlQuery<InsurerSearch>(Constant.StoredProcedureConst.InsurerRepositoryProcedure.GetAllInsurer, _InsurerName, _skip, _take);
        }

        public int GetAllInsurerCount(string InsurerName)
        {
            SqlParameter _InsurerName = new SqlParameter("@InsurerName", InsurerName);
            return Context.Database.SqlQuery<int>(Constant.StoredProcedureConst.InsurerRepositoryProcedure.GetAllInsurerCount, _InsurerName).SingleOrDefault();
        }

        public int CheckInsurerExist(string InsurerName)
        {
            SqlParameter _InsurerName = new SqlParameter("@InsurerName", InsurerName);
            return Context.Database.SqlQuery<int>(Constant.StoredProcedureConst.InsurerRepositoryProcedure.CheckInsurerExist, _InsurerName).SingleOrDefault();
        }
    }
}
