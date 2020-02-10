using Core.Base.Data.SqlServer;
using Core.Base.Data.SqlServer.Factory;
using Core.Base.Data.SqlServer.Repository;
using LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.Data.SqlServer.Repository
{
    public class CommissionPaymentRepository : BaseRepository<CommissionPayment, LMGEDIDBContext>, ICommissionPaymentRepository
    {
        public CommissionPaymentRepository(IContextFactory<LMGEDIDBContext> contextFactory) :
            base(new BaseUnitOfWork<LMGEDIDBContext>(contextFactory), contextFactory)
        {
        }
    }
}
