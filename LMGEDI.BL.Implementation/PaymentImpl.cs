using System.Linq;
using LMGEDI.Core.Data;
using Omu.ValueInjecter;

using DLModel = LMGEDI.Core.Data.Model;
using BLModel = LMGEDI.Core.BL.Model;

namespace LMGEDI.Core.BL.Implementation
{
    public class PaymentImpl : IPayment
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceAdjustmentRepository _invoiceAdjustmentRepostory;
        private readonly IInvoice _IInvoiceImpl;
        public PaymentImpl(IPaymentRepository paymentRepository, IInvoiceRepository invoiceRepository, IInvoiceAdjustmentRepository invoiceAdjustmentRepostory)
        {
            _paymentRepository = paymentRepository;
            _invoiceRepository = invoiceRepository;
            _invoiceAdjustmentRepostory = invoiceAdjustmentRepostory;
            _IInvoiceImpl = new InvoiceImpl(_invoiceRepository,_invoiceAdjustmentRepostory);
        }
        public int AddPaymentRecord(DLModel.Payment modelPayment)
        {
          //  var PaymentSum = _paymentRepository.GetAll(Pay => Pay.InvoiceId == modelPayment.InvoiceId).ToList().Select(Pay => Pay.PaymentAmount == null ? 0 : Pay.PaymentAmount).Sum();
            //PaymentSum = PaymentSum + modelPayment.PaymentAmount;
            DLModel.Invoice InvoiceDetail = _IInvoiceImpl.GetInvoiceDetailsById(modelPayment.InvoiceId);

            //if (modelPayment.PaymentAmount <= InvoiceDetail.InvoiceBalanceAmt)
            //{
                InvoiceDetail.InvoiceBalanceAmt = (InvoiceDetail.InvoiceBalanceAmt - modelPayment.PaymentAmount).Value;
                var updInvoiceBalAmt = _invoiceRepository.Update(InvoiceDetail, inv => inv.InvoiceBalanceAmt);
                return _paymentRepository.Add((DLModel.Payment)new DLModel.Payment().InjectFrom(modelPayment)).PaymentId;
            //}
            //else
            //    return -1; // Payment Sum amout can't be greater then invoice amount

        }
        public int UpdatePaymentRecord(DLModel.Payment modelPayment)
        {
           // var PaymentSum = _paymentRepository.GetAll(Pay => Pay.InvoiceId == modelPayment.InvoiceId && Pay.PaymentId != modelPayment.PaymentId).ToList().Select(Pay => Pay.PaymentAmount == null ? 0 : Pay.PaymentAmount).Sum();
            //PaymentSum = PaymentSum + modelPayment.PaymentAmount;
            DLModel.Invoice InvoiceDetail=_IInvoiceImpl.GetInvoiceDetailsById(modelPayment.InvoiceId);
            var oldPaymentAmount = _paymentRepository.GetAll(pay => pay.PaymentId == modelPayment.PaymentId).Select(inv => new DLModel.Payment().InjectFrom(inv)).Cast<DLModel.Payment>().SingleOrDefault().PaymentAmount;
            //if (modelPayment.PaymentAmount <= (InvoiceDetail.InvoiceBalanceAmt + oldPaymentAmount))
            //{
                var newInvoiceAmount = (InvoiceDetail.InvoiceBalanceAmt + oldPaymentAmount) - modelPayment.PaymentAmount;
                InvoiceDetail.InvoiceBalanceAmt = newInvoiceAmount.Value;
                var updInvoiceBalAmt = _invoiceRepository.Update(InvoiceDetail, inv => inv.InvoiceBalanceAmt);
                return _paymentRepository.Update((DLModel.Payment)new DLModel.Payment().InjectFrom(modelPayment));
            //}
            //else
            //    return -1; // Payment Sum amout can't be greater then invoice amount

        }
        public BLModel.Paged.PaymentDetail GetPaymentRecordByInvoiceId(int InvoiceId, int Skip, int Take)
        {
            return new BLModel.Paged.PaymentDetail
            {
                PaymentDetails = _paymentRepository.GetAll(payment => payment.InvoiceId.Equals(InvoiceId)).Skip(Skip).Take(Take).Select(Ocr => new DLModel.Payment().InjectFrom(Ocr)).Cast<DLModel.Payment>().ToList(),
                TotalCount = _paymentRepository.GetAll(payment => payment.InvoiceId.Equals(InvoiceId)).Count()
            };
        }
    }
}
