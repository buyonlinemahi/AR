using DLModel = LMGEDI.Core.Data.Model;
using BLModel = LMGEDI.Core.BL.Model;

namespace LMGEDI.Core.BL
{
    public interface IPayment
    {
        int AddPaymentRecord(DLModel.Payment modelPayment);
        int UpdatePaymentRecord(DLModel.Payment modelPayment);
        BLModel.Paged.PaymentDetail GetPaymentRecordByInvoiceId(int InvoiceId, int Skip, int Take);
    }
}
