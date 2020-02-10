using System.Linq;
using LMGEDI.Core.Data;
using Omu.ValueInjecter;

using DLModel = LMGEDI.Core.Data.Model;
using BLModel = LMGEDI.Core.BL.Model;

namespace LMGEDI.Core.BL.Implementation
{
    public class PaymentRefundImpl : IPaymentRefund
    {
        private readonly IPaymentRefundRepository _paymentRefundRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoice _IInvoiceImpl;
        private readonly IInvoiceAdjustmentRepository _invoiceAdjustmentRepostory;


        public PaymentRefundImpl(IPaymentRefundRepository paymentRefundRepository,
            IInvoiceRepository invoiceRepository,
            IInvoiceAdjustmentRepository invoiceAdjustmentRepostory)
        {
            _paymentRefundRepository = paymentRefundRepository;
            _invoiceRepository = invoiceRepository;
            _invoiceAdjustmentRepostory = invoiceAdjustmentRepostory;
            _IInvoiceImpl = new InvoiceImpl(_invoiceRepository, _invoiceAdjustmentRepostory);
        }

        public int AddPaymentRefundRecord(DLModel.PaymentRefund modelPaymentRefund)
        {
            DLModel.Invoice InvoiceDetail = _IInvoiceImpl.GetInvoiceDetailsById(modelPaymentRefund.InvoiceId);
            InvoiceDetail.InvoiceBalanceAmt = (InvoiceDetail.InvoiceBalanceAmt + modelPaymentRefund.RefundAmount).Value;
            var updInvoiceBalAmt = _invoiceRepository.Update(InvoiceDetail, inv => inv.InvoiceBalanceAmt);
            return _paymentRefundRepository.Add((DLModel.PaymentRefund)new DLModel.PaymentRefund().InjectFrom(modelPaymentRefund)).PaymentRefundID;
         
        }
        public int UpdatePaymentRefundRecord(DLModel.PaymentRefund modelPaymentRefund)
        {            
            DLModel.Invoice InvoiceDetail = _IInvoiceImpl.GetInvoiceDetailsById(modelPaymentRefund.InvoiceId);
            var oldPaymentAmount = _paymentRefundRepository.GetAll(pay => pay.PaymentRefundID == modelPaymentRefund.PaymentRefundID).Select(inv => new DLModel.PaymentRefund().InjectFrom(inv)).Cast<DLModel.PaymentRefund>().SingleOrDefault().RefundAmount;
            var newInvoiceAmount = (InvoiceDetail.InvoiceBalanceAmt - oldPaymentAmount) + modelPaymentRefund.RefundAmount;
            InvoiceDetail.InvoiceBalanceAmt = newInvoiceAmount.Value;
            var updInvoiceBalAmt = _invoiceRepository.Update(InvoiceDetail, inv => inv.InvoiceBalanceAmt);
            return _paymentRefundRepository.Update((DLModel.PaymentRefund)new DLModel.PaymentRefund().InjectFrom(modelPaymentRefund));         
        }


        public BLModel.Paged.PaymentRefundDetail GetPaymentRefundRecordByInvoiceId(int InvoiceId, int Skip, int Take)
        {
            return new BLModel.Paged.PaymentRefundDetail
            {
                PaymentRefundDetails = _paymentRefundRepository.GetAll(paymentRefund => paymentRefund.InvoiceId.Equals(InvoiceId)).Skip(Skip).Take(Take).Select(paymentRefund => new DLModel.PaymentRefund().InjectFrom(paymentRefund)).Cast<DLModel.PaymentRefund>().ToList(),
                TotalCount = _paymentRefundRepository.GetAll(payment => payment.InvoiceId.Equals(InvoiceId)).Count()
            };
        }

        public string GetNetworkingDays(string InvoiceDate, int DepartmentId)
        {
            return _paymentRefundRepository.GetNetworkingDays(InvoiceDate,DepartmentId);
        }
    }
}
