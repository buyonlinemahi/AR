using Core.Base.Data.SqlServer;
using Core.Base.Data.SqlServer.Factory;
using Core.Base.Data.SqlServer.Repository;
using LMGEDI.Core.Data.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace LMGEDI.Core.Data.SqlServer.Repository
{
    public class LienTempTableRepository : BaseRepository<LienTempTable,LMGEDIDBContext>,ILienTempTableRepository
    {
        public LienTempTableRepository(IContextFactory<LMGEDIDBContext> contextFactory) :
            base(new BaseUnitOfWork<LMGEDIDBContext>(contextFactory), contextFactory)
        {
        }
        public int UpdateLienTempClaimNumberByUploadID(LienTempTable _lienTempTable)
        {
            SqlParameter _UploadID = new SqlParameter("@UploadId", _lienTempTable.UploadId);
            SqlParameter _Claim = new SqlParameter("@Claim", _lienTempTable.Claim);
            SqlParameter _FirstName = new SqlParameter("@FirstName", _lienTempTable.FirstName);
            SqlParameter _LastName = new SqlParameter("@LastName", _lienTempTable.LastName);
            SqlParameter _Adjuster = new SqlParameter("@Adjuster", _lienTempTable.Adjuster);
            SqlParameter _Insurer = new SqlParameter("@Insurer", _lienTempTable.Insurer);
            SqlParameter _InsurerBranch = new SqlParameter("@InsurerBranch", _lienTempTable.InsurerBranch);
            SqlParameter _Employer = new SqlParameter("@Employer", _lienTempTable.Employer);
            return Context.Database.ExecuteSqlCommand(Constant.StoredProcedureConst.LienTempTableRepositoryProcedure.UpdateLienTempClaimNumberByUploadID, _Claim, _UploadID, _FirstName,_LastName,_Adjuster,_Insurer,_InsurerBranch,_Employer);
        }

        public ErrorLogFile ProcessedLienTempTable()
        {
            Context.Database.CommandTimeout = 36000;
            return Context.Database.SqlQuery<ErrorLogFile>(Constant.StoredProcedureConst.LienTempTableRepositoryProcedure.ProcessedLienTempTable).SingleOrDefault();
        }

        public IEnumerable<LienTempTableRecords> GetAllLienTempRecords(int skip, int take)
        {
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);
            return Context.Database.SqlQuery<LienTempTableRecords>(Constant.StoredProcedureConst.LienTempTableRepositoryProcedure.GetAllLienTempRecords, _skip, _take);
        }

    }
}
