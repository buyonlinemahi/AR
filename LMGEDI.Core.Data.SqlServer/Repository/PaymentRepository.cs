using Core.Base.Data.SqlServer;
using Core.Base.Data.SqlServer.Factory;
using Core.Base.Data.SqlServer.Repository;
using LMGEDI.Core.Data.Model;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace LMGEDI.Core.Data.SqlServer.Repository
{
    public class PaymentRepository : BaseRepository<Payment, LMGEDIDBContext>, IPaymentRepository
    {
        public PaymentRepository(IContextFactory<LMGEDIDBContext> contextFactory) :
            base(new BaseUnitOfWork<LMGEDIDBContext>(contextFactory), contextFactory)
        {
        }

    }
}
