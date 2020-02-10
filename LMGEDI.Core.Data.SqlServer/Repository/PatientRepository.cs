using Core.Base.Data.SqlServer;
using Core.Base.Data.SqlServer.Factory;
using Core.Base.Data.SqlServer.Repository;
using LMGEDI.Core.Data.Model;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace LMGEDI.Core.Data.SqlServer.Repository
{
    public class PatientRepository : BaseRepository<Patient,LMGEDIDBContext>,IPatientRepository
    {
        public PatientRepository(IContextFactory<LMGEDIDBContext> contextFactory) : 
            base(new BaseUnitOfWork<LMGEDIDBContext>(contextFactory),contextFactory)
        {
            
        }

        public IEnumerable<Patient> GetAllPatient(int skip, int take)
        {
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);
            return Context.Database.SqlQuery<Patient>(Constant.StoredProcedureConst.PatientRepositoryProcedure.GetAllPatients,_skip,_take);
        }
        public IEnumerable<Patient> GetAllPatientByName(string FirstName,int skip, int take)
        {
            SqlParameter _firstName = new SqlParameter("@FirstName",FirstName);
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);
            return Context.Database.SqlQuery<Patient>(Constant.StoredProcedureConst.PatientRepositoryProcedure.GetPatientsByName,_firstName, _skip, _take);
        }
        public IEnumerable<Patient> GetAllPatientByLastName(string LastName,int skip, int take)
        {
            SqlParameter _lastName = new SqlParameter("@LastName", LastName);
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);
            return Context.Database.SqlQuery<Patient>(Constant.StoredProcedureConst.PatientRepositoryProcedure.GetPatientByLastName,_lastName, _skip, _take);
        }


        //---------------------------------Count Call----------------------------------------------------------------------//
        //-----------------------------------------------------------------------------------------------------------------

        public int GetAllPatientCount()
        {           
            return Context.Database.SqlQuery<int>(Constant.StoredProcedureConst.PatientRepositoryProcedure.GetPatientsCount).SingleOrDefault();
        }
        public int GetAllPatientByNameCount(string FirstName)
        {
            SqlParameter _firstName = new SqlParameter("@FirstName", FirstName);
            return Context.Database.SqlQuery<int>(Constant.StoredProcedureConst.PatientRepositoryProcedure.GetPatientsByNameCount,_firstName).SingleOrDefault();
        }
        public int GetAllPatientByLastNameCount(string LastName)
        {
            SqlParameter _lastName = new SqlParameter("@LastName", LastName);
            return Context.Database.SqlQuery<int>(Constant.StoredProcedureConst.PatientRepositoryProcedure.GetPatientByLastNameCount,_lastName).SingleOrDefault();
        }
        public int UpdatePatient(Patient patient)
        {
            SqlParameter _PatientID = new SqlParameter("@PatientID", patient.PatientID);
            SqlParameter _PatientAccount = new SqlParameter("@PatientAccount", patient.PatientAccount);
            SqlParameter _PatientFirstName = new SqlParameter("@PatientFirst", patient.PatientFirst);
            SqlParameter _PatientLast = new SqlParameter("@PatientLast", patient.PatientLast);
            SqlParameter _PatientGender = new SqlParameter("@PatientGender", patient.PatientGender);
            SqlParameter _PatientDOB = new SqlParameter("@PatientDOB", patient.PatientDOB);
            SqlParameter _PatientClaim = new SqlParameter("@PatientClaim", patient.PatientClaim);
            SqlParameter _PatientSSN = new SqlParameter("@PatientSSN", patient.PatientSSN);
            SqlParameter _PatientWCAB = new SqlParameter("@PatientWCAB", patient.PatientWCAB);
            SqlParameter _PatientEmployer = new SqlParameter("@PatientEmployer", patient.PatientEmployer);
            SqlParameter _PatientInsurance = new SqlParameter("@PatientInsurance", patient.PatientInsurance);
            return Context.Database.ExecuteSqlCommand(Constant.StoredProcedureConst.PatientRepositoryProcedure.UpdatePatient, _PatientID, _PatientAccount, _PatientFirstName, _PatientLast, _PatientGender, _PatientDOB, _PatientClaim, _PatientSSN, _PatientWCAB, _PatientEmployer, _PatientInsurance);
        }
        public Patient GetPatientByID(int PatientID)
        {
            SqlParameter _PatientID = new SqlParameter("@PatientID", PatientID);
            return Context.Database.SqlQuery<Patient>(Constant.StoredProcedureConst.PatientRepositoryProcedure.GetPatientByID, _PatientID).FirstOrDefault();
        }
    }
}
