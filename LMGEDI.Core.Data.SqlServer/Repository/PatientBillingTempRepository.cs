using Core.Base.Data.SqlServer;
using Core.Base.Data.SqlServer.Factory;
using Core.Base.Data.SqlServer.Repository;
using LMGEDI.Core.Data.Model;
using System.Collections.Generic;

namespace LMGEDI.Core.Data.SqlServer.Repository
{
    public class PatientBillingTempRepository : BaseRepository<PatientBillingTemp, LMGEDIDBContext>, IPatientBillingTempRepository
    {
        public PatientBillingTempRepository(IContextFactory<LMGEDIDBContext> contextFactory) :
            base(new BaseUnitOfWork<LMGEDIDBContext>(contextFactory),contextFactory)
        {
        }

        public IEnumerable<TempExcelDataColumns> GetAllColumnNameFromTempPatientBilling()
        {
            return Context.Database.SqlQuery<TempExcelDataColumns>(Constant.StoredProcedureConst.TempExcelDataRepositoryProcedure.GetAllColumnNameFromTempPatientBilling);
        }
        public int DuplicatePatientBillingTempDataReplace()
        {
            return Context.Database.ExecuteSqlCommand(Constant.StoredProcedureConst.TempExcelDataRepositoryProcedure.DuplicatePatientBillingTempDataReplace);
        }
    }
}
