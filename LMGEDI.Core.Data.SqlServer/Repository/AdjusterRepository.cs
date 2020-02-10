using Core.Base.Data.SqlServer;
using Core.Base.Data.SqlServer.Factory;
using Core.Base.Data.SqlServer.Repository;
using LMGEDI.Core.Data.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace LMGEDI.Core.Data.SqlServer.Repository
{
    public class AdjusterRepository : BaseRepository<Adjuster,LMGEDIDBContext>,IAdjusterRepository
    {
        public AdjusterRepository(IContextFactory<LMGEDIDBContext> contextFactory) :
            base(new BaseUnitOfWork<LMGEDIDBContext>(contextFactory),contextFactory)
        {
        }

        public IEnumerable<AdjusterSearch> GetAllAdjuster(string AdjusterName, int skip, int take)
        {
            SqlParameter _AdjusterName = new SqlParameter("@AdjusterName", AdjusterName);
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);
            return Context.Database.SqlQuery<AdjusterSearch>(Constant.StoredProcedureConst.AdjusterRepositoryProcedure.GetAllAdjuster,_AdjusterName, _skip, _take);
        }

        public int GetAllAdjusterCount(string AdjusterName)
        {
            SqlParameter _AdjusterName = new SqlParameter("@AdjusterName", AdjusterName);
            return Context.Database.SqlQuery<int>(Constant.StoredProcedureConst.AdjusterRepositoryProcedure.GetAllAdjusterCount, _AdjusterName).SingleOrDefault();
        }

        public int CheckAdjusterExist(string AdjusterFirstName, string AdjusterLastName)
        {
            SqlParameter _AdjusterFirstName = new SqlParameter("@AdjusterFirstName", AdjusterFirstName);
            SqlParameter _AdjusterLastName = new SqlParameter("@AdjusterLastName", AdjusterLastName);
            return Context.Database.SqlQuery<int>(Constant.StoredProcedureConst.AdjusterRepositoryProcedure.CheckAdjusterExist, _AdjusterFirstName, _AdjusterLastName).SingleOrDefault();
        }

        public AdjusterSearch GetLienAdjusterByAdjusterID(int AdjusterId)
        {
            SqlParameter _AdjusterId = new SqlParameter("@AdjusterId", AdjusterId);
            return Context.Database.SqlQuery<AdjusterSearch>(Constant.StoredProcedureConst.AdjusterRepositoryProcedure.GetLienAdjusterByAdjusterID, _AdjusterId).SingleOrDefault();
        }


        public AdjusterSearch GetAdjusterByAdjusterID(int AdjusterId)
        {
            SqlParameter _AdjusterId = new SqlParameter("@AdjusterId", AdjusterId);
            return Context.Database.SqlQuery<AdjusterSearch>(Constant.StoredProcedureConst.AdjusterRepositoryProcedure.GetAdjusterByAdjusterID, _AdjusterId).SingleOrDefault();
        }
    }
}
