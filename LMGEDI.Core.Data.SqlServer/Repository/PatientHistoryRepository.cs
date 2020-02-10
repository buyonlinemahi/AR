using Core.Base.Data.SqlServer;
using Core.Base.Data.SqlServer.Factory;
using Core.Base.Data.SqlServer.Repository;
using LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.Data.SqlServer.Repository
{
  public  class PatientHistoryRepository : BaseRepository<PatientHistory, LMGEDIDBContext>,IPatientHistoryRepository
    {
        public PatientHistoryRepository(IContextFactory<LMGEDIDBContext> contextFactory) :
            base(new BaseUnitOfWork<LMGEDIDBContext>(contextFactory), contextFactory)
        {

        }


       
    }
}
   