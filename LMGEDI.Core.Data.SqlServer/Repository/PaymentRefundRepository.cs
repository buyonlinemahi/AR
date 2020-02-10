using Core.Base.Data.SqlServer;
using Core.Base.Data.SqlServer.Factory;
using Core.Base.Data.SqlServer.Repository;
using LMGEDI.Core.Data.Model;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace LMGEDI.Core.Data.SqlServer.Repository
{
    public class PaymentRefundRepository : BaseRepository<PaymentRefund,LMGEDIDBContext>,IPaymentRefundRepository
    {
        public PaymentRefundRepository(IContextFactory<LMGEDIDBContext> contextFactory) :
            base(new BaseUnitOfWork<LMGEDIDBContext>(contextFactory), contextFactory)
        {
        }


        public string GetNetworkingDays(string InvoiceDate, int DepartmentId)
        {
            SqlParameter _invoiceDate = new SqlParameter("@InvoiceDate", InvoiceDate);
            SqlParameter _departmentId = new SqlParameter("@DepartmentId", DepartmentId);
            return Context.Database.SqlQuery<string>(Constant.StoredProcedureConst.PaymentRefundRepositoryProcedure.GetNetworkingDays, _invoiceDate, _departmentId).SingleOrDefault();
        }
    }
}
