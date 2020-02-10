using Core.Base.Data.SqlServer;
using Core.Base.Data.SqlServer.Factory;
using Core.Base.Data.SqlServer.Repository;
using LMGEDI.Core.Data.Model;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace LMGEDI.Core.Data.SqlServer.Repository
{
    public class PatientBillingValidationRepository : BaseRepository<PatientBillingValidation, LMGEDIDBContext>, IPatientBillingValidationRepository
    {
        public PatientBillingValidationRepository(IContextFactory<LMGEDIDBContext> contextFactory) :
            base(new BaseUnitOfWork<LMGEDIDBContext>(contextFactory), contextFactory)
        {
        }

        public IEnumerable<PatientBillingValidation> GetAllFromPatientBillingValidation(int skip, int take)
        {
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);
            return Context.Database.SqlQuery<PatientBillingValidation>(Constant.StoredProcedureConst.TempExcelDataRepositoryProcedure.GetAllFromPatientBillingValidation, _skip, _take); 
        }

        public int UpdateValidateDataFromBillingValidation(PatientBillingValidation patientBillingValidation)
        {
            SqlParameter _PatientFName = new SqlParameter("@PatientFName", patientBillingValidation.PatientFName);
            SqlParameter _PatientSSN = new SqlParameter("@PatientSSN", patientBillingValidation.PatientSSN);
            SqlParameter _BillingInsurance = new SqlParameter("@BillingInsurance", patientBillingValidation.BillingInsurance);
            SqlParameter _BillingID = new SqlParameter("@BillingID", patientBillingValidation.BillingID);
            return Context.Database.ExecuteSqlCommand(Constant.StoredProcedureConst.TempExcelDataRepositoryProcedure.UpdateValidateDataFromBillingValidation, _PatientFName, _PatientSSN, _BillingInsurance,_BillingID);
        }

        public int GetAllFromPatientBillingValidationCount()
        {
            return Context.Database.SqlQuery<int>(Constant.StoredProcedureConst.TempExcelDataRepositoryProcedure.GetAllFromPatientBillingValidationCount).SingleOrDefault();
        }
    }
}
