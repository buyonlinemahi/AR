using Core.Base.Data.SqlServer;
using Core.Base.Data.SqlServer.Factory;
using Core.Base.Data.SqlServer.Repository;
using LMGEDI.Core.Data.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace LMGEDI.Core.Data.SqlServer.Repository
{
    public class CommissionRepository : BaseRepository<Commission, LMGEDIDBContext>, ICommissionRepository
    {
        public CommissionRepository(IContextFactory<LMGEDIDBContext> contextFactory) :
            base(new BaseUnitOfWork<LMGEDIDBContext>(contextFactory), contextFactory)
        {
        }

        public IEnumerable<CommissionSearch> GetCommissionRecordByLienClientID(int lienClientID, int skip, int take)
        {
            SqlParameter _lienClientID = new SqlParameter("@LienClientID", lienClientID);
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);
            return Context.Database.SqlQuery<CommissionSearch>(Constant.StoredProcedureConst.CommissionRepositoryProcedure.GetCommissionRecordByLienClientID, _lienClientID, _skip, _take);

        }

        public int GetCommissionRecordByLienClientIDCount(int lienClientID)
        {
            SqlParameter _lienClientID = new SqlParameter("@LienClientID", lienClientID);
            return Context.Database.SqlQuery<int>(Constant.StoredProcedureConst.CommissionRepositoryProcedure.GetCommissionRecordByLienClientIDCount, _lienClientID).SingleOrDefault();
        }

        public IEnumerable<LienClientBilling> GetAllLienClientBilling()
        {
            return Context.Database.SqlQuery<LienClientBilling>(Constant.StoredProcedureConst.CommissionRepositoryProcedure.GetAllLienClientBilling);
        }

        public IEnumerable<CommissionSearch> GetCommissionDashboardReaord(int skip, int take)
        {
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);
            return Context.Database.SqlQuery<CommissionSearch>(Constant.StoredProcedureConst.CommissionRepositoryProcedure.GetCommissionDashboardReaord, _skip, _take);
        }
        public int GetCommissionDashboardRecordCount()
        {
            return Context.Database.SqlQuery<int>(Constant.StoredProcedureConst.CommissionRepositoryProcedure.GetCommissionDashboardRecordCount).SingleOrDefault();
        }
    }
}
