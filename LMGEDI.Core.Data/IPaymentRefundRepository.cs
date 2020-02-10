using System;
using Core.Base.Data;
using LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.Data
{
    public interface IPaymentRefundRepository : IBaseRepository<PaymentRefund>
    {
        string GetNetworkingDays(string InvoiceDate, int DepartmentId);
    }
}
