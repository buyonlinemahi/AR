using Core.Base.Data.SqlServer;
using Core.Base.Data.SqlServer.Factory;
using Core.Base.Data.SqlServer.Repository;
using LMGEDI.Core.Data.Model;
using System.Collections.Generic;

namespace LMGEDI.Core.Data.SqlServer.Repository
{
    public class PatientTempRepository : BaseRepository<PatientTemp, LMGEDIDBContext>, IPatientTempRepository
    {
        public PatientTempRepository(IContextFactory<LMGEDIDBContext> contextFactory) :
            base(new BaseUnitOfWork<LMGEDIDBContext>(contextFactory), contextFactory)
        {
        }

        public IEnumerable<TempExcelDataColumns> GetAllColumnNameFromTempPatient()
        {
            return Context.Database.SqlQuery<TempExcelDataColumns>(Constant.StoredProcedureConst.TempExcelDataRepositoryProcedure.GetAllColumnNameFromTempPatient);
        }

        public int DuplicatePatientTempDataReplace()
        {
            return Context.Database.ExecuteSqlCommand(Constant.StoredProcedureConst.TempExcelDataRepositoryProcedure.DuplicatePatientTempDataReplace);
        }
    }
}
