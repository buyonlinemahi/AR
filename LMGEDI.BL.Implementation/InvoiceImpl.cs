using System.Linq;
using LMGEDI.Core.Data;
using Omu.ValueInjecter;
using DLModel = LMGEDI.Core.Data.Model;
using BLModel = LMGEDI.Core.BL.Model;
using LMGEDI.Core.Data.Model;
using System.Collections.Generic;

namespace LMGEDI.Core.BL.Implementation
{
    public class InvoiceImpl : IInvoice
    {
        private readonly IInvoiceRepository _invoiceRepostory;
        private readonly IInvoiceAdjustmentRepository _invoiceAdjustmentRepostory;
        public InvoiceImpl(IInvoiceRepository invoiceRepository, IInvoiceAdjustmentRepository invoiceAdjustmentRepostory)
        {
            _invoiceRepostory = invoiceRepository;
            _invoiceAdjustmentRepostory = invoiceAdjustmentRepostory;
        }
        public int AddInvoiceRecord(DLModel.Invoice modelInvoice)
        { 
            if (_invoiceRepostory.GetAll(Inv => Inv.FileID == modelInvoice.FileID && Inv.InvoiceNumber == modelInvoice.InvoiceNumber).Count() > 0)
                return 0; // means Invoice number allready exist for same file Id
            else
                return _invoiceRepostory.Add((DLModel.Invoice)new DLModel.Invoice().InjectFrom(modelInvoice)).InvoiceID;
        }

        public int UpdateInvoiceRecord(DLModel.Invoice modelInvoice)
        {
            if (_invoiceRepostory.GetAll(Inv => (Inv.InvoiceID != modelInvoice.InvoiceID) && (Inv.FileID == modelInvoice.FileID) && (Inv.InvoiceNumber == modelInvoice.InvoiceNumber)).Count() > 0)
                return 0; // means Invoice number allready exist for same file Id
            else
                return _invoiceRepostory.Update((DLModel.Invoice)new DLModel.Invoice().InjectFrom(modelInvoice));
        }
        public BLModel.Paged.InvoiceDetail GetInvoiceRecordByFileId(int FileId, int Skip, int Take)
        {
            return new BLModel.Paged.InvoiceDetail
            {
                InvoiceDetails = _invoiceRepostory.GetAllInvoice(FileId, Skip, Take).Select(depat => new DLModel.InvoiceDetail().InjectFrom(depat)).Cast<DLModel.InvoiceDetail>().ToList(),
                TotalCount = _invoiceRepostory.GetAllInvoiceCountByFileId(invoice => invoice.FileID.Equals(FileId))
            };

            
        }
        public DLModel.Invoice GetInvoiceDetailsById(int InvoiceId)
        {
            return _invoiceRepostory.GetById(InvoiceId);
        }
        public int AddInvoiceAdjustment(DLModel.InvoiceAdjustment modelAdJustment)
        {
            var adjRecord = _invoiceAdjustmentRepostory.GetAll(Inv => Inv.InvoiceID == modelAdJustment.InvoiceID).OrderByDescending(aj => aj.AdjustmentID).FirstOrDefault();
           if(adjRecord == null)
           {
               return _invoiceAdjustmentRepostory.UpdateInvoiceAdjuster(modelAdJustment.InvoiceID, modelAdJustment.AdjustmentNotes, modelAdJustment.CreatedBy, modelAdJustment.AdjustmentAmt);
           }
           else if (adjRecord.AdjustmentAmt != modelAdJustment.AdjustmentAmt)
            {
                return _invoiceAdjustmentRepostory.UpdateInvoiceAdjuster(modelAdJustment.InvoiceID,modelAdJustment.AdjustmentNotes,modelAdJustment.CreatedBy,modelAdJustment.AdjustmentAmt);
          //      return _invoiceAdjustmentRepostory.Add(modelAdJustment).AdjustmentID;
            }
            else
                return 0;
        }

        public IEnumerable<DLModel.InvoiceICDetail> GetInvoiceICRecord(int FileId)
        {
            return _invoiceRepostory.GetAllInvoiceIC(FileId).Select(invoiceic => new DLModel.InvoiceICDetail().InjectFrom(invoiceic)).Cast<DLModel.InvoiceICDetail>().ToList();             
        }

        public int  UpdateInvoiceICAmt(IEnumerable<DLModel.InvoiceICDetail> objInvoiceICDetail)
        {            
            var _updateInvoiceICDetail = objInvoiceICDetail.ToList();
                
            foreach (var updateInvoieModel in _updateInvoiceICDetail)
            {
              var t =  _invoiceRepostory.UpdateInvoiceICAmtAccFileIdInvoiceID(updateInvoieModel);
            }

            return 1;
        }

        public void UpdateAssignedToInvoiceIDByInvoiceID(int InvoiceID, int SubInvoiceID)
        {
            _invoiceRepostory.UpdateAssignedToInvoiceIDByInvoiceID(InvoiceID, SubInvoiceID);
        }

        public IEnumerable<DLModel.InvoiceICDetail> GetAssignedInvoicedByInvoiceID(int InvoiceID)
        {
            return _invoiceRepostory.GetAssignedInvoicedByInvoiceID(InvoiceID);
        }
    }
}
 