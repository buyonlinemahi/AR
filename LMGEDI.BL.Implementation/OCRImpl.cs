using LMGEDI.Core.Data;
using DLModel = LMGEDI.Core.Data.Model;
using System.Linq;
using BLModel = LMGEDI.Core.BL.Model;
using Omu.ValueInjecter;
using System.Collections.Generic;

namespace LMGEDI.Core.BL.Implementation
{
    public class OCRImpl : IOCR
    {
        private readonly IOCRRepository _iOCRRepository;
        private readonly IPaymentRepository _iPaymentRepository;
        private readonly IInvoiceRepository _iInvoiceRepository;


        public OCRImpl(IOCRRepository oCRRepository, IPaymentRepository paymentRepository, IInvoiceRepository invoiceRepository)
        {
            _iOCRRepository = oCRRepository;
            _iPaymentRepository = paymentRepository;
            _iInvoiceRepository = invoiceRepository;
        }
        public int AddOCRRecord(DLModel.OCR modelOCR)
        {
            return _iOCRRepository.Add((DLModel.OCR)new DLModel.OCR().InjectFrom(modelOCR)).OcrId;
        }

        public BLModel.Paged.OCRDetail GetAllOCRRecords(string Filename, int Skip, int Take)
        {
            if (Filename.Equals("%"))
            {
                return new BLModel.Paged.OCRDetail
                {
                    OCRDetails = _iOCRRepository.GetAll().Where(ocr => (ocr.IsOCRPaymentRecevied == false || ocr.IsOCRPaymentRecevied == null)).OrderByDescending(ocr => ocr.OcrId).Skip(Skip).Take(Take).Select(Ocr => new DLModel.OCR().InjectFrom(Ocr)).Cast<DLModel.OCR>().ToList(),
                    TotalCount = _iOCRRepository.GetAll().Where(ocr => (ocr.IsOCRPaymentRecevied == false || ocr.IsOCRPaymentRecevied == null)).Count()
                };
            }
            else
            {
                return new BLModel.Paged.OCRDetail
                {
                    OCRDetails = _iOCRRepository.GetAll(Ins => Ins.OcrFileName.Contains(Filename.ToString())).Where(ocr => (ocr.IsOCRPaymentRecevied == false || ocr.IsOCRPaymentRecevied == null)).OrderByDescending(ocr => ocr.OcrId).Skip(Skip).Take(Take).Select(Ocr => new DLModel.OCR().InjectFrom(Ocr)).Cast<DLModel.OCR>().ToList(),
                    TotalCount = _iOCRRepository.GetAll(Ins => Ins.OcrFileName.Contains(Filename.ToString())).Where(ocr => (ocr.IsOCRPaymentRecevied == false || ocr.IsOCRPaymentRecevied == null)).OrderByDescending(ocr => ocr.OcrId).Count()
                };
            }
        }
        public int DeleteOCRFileDetailsById(int OCRId, string Filename)
        {
            _iOCRRepository.Delete(_iOCRRepository.GetById(OCRId));
            return 1;
        }

        public IEnumerable<DLModel.OCRFileInvoices> GetOCRFilesInvoiceRecords(string SearchText, int Skip, int Take)
        {
            return _iOCRRepository.GetOCRFilesInvoiceRecords(SearchText, Skip, Take).Select(OCRs => new DLModel.OCRFileInvoices().InjectFrom(OCRs)).Cast<DLModel.OCRFileInvoices>().ToList();
        }

        public int GetOCRFilesInvoiceRecordsCount(string SearchText)
        {
            return _iOCRRepository.GetOCRFilesInvoiceRecordsCount(SearchText);
        }

        public IEnumerable<DLModel.OCRPaymentDetails> GetOCRPaymentFilesRecords(string SearchText, int Skip, int Take)
        {
            return _iOCRRepository.GetOCRPaymentFilesRecords(SearchText, Skip, Take).Select(OCRs => new DLModel.OCRPaymentDetails().InjectFrom(OCRs)).Cast<DLModel.OCRPaymentDetails>().ToList();
        }

        public int GetOCRPaymentFilesRecordsCount(string SearchText)
        {
            return _iOCRRepository.GetOCRPaymentFilesRecordsCount(SearchText);
        }

        public DLModel.OCRPayment AddOCRPaymentRecords(DLModel.OCRPaymentSave oCRPaymentModel)
        {
            return _iOCRRepository.AddOCRPaymentRecords(oCRPaymentModel);
        }

        public int CheckOCRPaymentDetailsByInvoiceId(DLModel.OCRPaymentDetails oCRPaymentDetails)
        {
            var _InvoiceBalanceAmt = _iInvoiceRepository.GetAll(ins => ins.InvoiceID == oCRPaymentDetails.InvoiceID).Select(Invoice => new DLModel.Invoice().InjectFrom(Invoice)).Cast<DLModel.Invoice>().SingleOrDefault().InvoiceBalanceAmt;
            var _PaymentSumAmount = oCRPaymentDetails.PaymentAmount;// _iPaymentRepository.GetAll(pmt => pmt.InvoiceId == oCRPaymentDetails.InvoiceID).ToList().Select(pmt => pmt.PaymentAmount == null ? 0 : pmt.PaymentAmount).Sum();
            //if (_PaymentSumAmount <= _InvoiceBalanceAmt)
            //    return 1;// WE CAN SAVE THE PAYMENT DETAILS
            //else
            //    return -1;// SUM OF PAYMENTS AMOUNT IS GREATER THEN INVOICE AMOUNT 
            return 1;
        }

        public DLModel.OCR GetOCRDetailById(int OCRId)
        {
            // Also need to update
            return _iOCRRepository.GetAll(ocr => ocr.OcrId == OCRId).Select(Ocr => new DLModel.OCR().InjectFrom(Ocr)).Cast<DLModel.OCR>().SingleOrDefault();
        }

        public int UpdateOCRRecord(DLModel.OCR modelOCR)
        {
            return _iOCRRepository.Update((DLModel.OCR)new DLModel.OCR().InjectFrom(modelOCR), ocr=> ocr.IsOCRPaymentRecevied);
        }
    }
}
