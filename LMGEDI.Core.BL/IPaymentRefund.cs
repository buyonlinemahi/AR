using DLModel = LMGEDI.Core.Data.Model;
using BLModel = LMGEDI.Core.BL.Model;

namespace LMGEDI.Core.BL
{
    public interface IPaymentRefund
    {
        int AddPaymentRefundRecord(DLModel.PaymentRefund modelPaymentRefund);
        int UpdatePaymentRefundRecord(DLModel.PaymentRefund modelPaymentRefund);
        BLModel.Paged.PaymentRefundDetail GetPaymentRefundRecordByInvoiceId(int InvoiceId, int Skip, int Take);
        string GetNetworkingDays(string InvoiceDate, int DepartmentId);
    }
}
