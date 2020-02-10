using LMGEDI.Core.BL.Implementation;
using LMGEDI.Core.BL;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBusinessTierTest
{
    [TestClass]
    public class InvoiceNoteTest
    {
        private IInvoiceNoteRepository _IInvoiceNoteRepository;
        private IInvoiceNote _IInvoiceNote; 
        private LMGEDI.Core.Data.Model.InvoiceNote DLModel = new LMGEDI.Core.Data.Model.InvoiceNote();

        [TestInitialize]
        public void InvoiceInitializer()
        {
            _IInvoiceNoteRepository = new InvoiceNoteRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _IInvoiceNote = new InvoiceNoteImpl(_IInvoiceNoteRepository);
        }
        [TestMethod]
        public void AddInvoiceNote()
        {
            DLModel.InvoiceID = 19;
            DLModel.InvoiceNotes = "klkdlsakdlksalkdlsakdlksalfdksa fakfd;sa kfd;ska ;fsafk safksaf; safa";
            DLModel.UserID = 1;
            int invoiceID = _IInvoiceNote.AddInvoiceNoteRecord(DLModel);
            Assert.IsTrue(true, "Unable to find");
        }
        [TestMethod]
        public void UpdateInvoiceNote()
        {
            DLModel.InvoiceID = 19;
            DLModel.InvoiceNotes = "klkdlssd fsfd fdsfsdf sa fsdksa fakfd;sa kfd;ska ;fsafk safksaf; safa";
            DLModel.UserID = 1;
            DLModel.InvoiceNotesID = 1;
            int invoiceID = _IInvoiceNote.UpdateInvoiceNoteRecord(DLModel);
            Assert.IsTrue(true, "Unable to find");
        }

        [TestMethod]
        public void Get_InvoiceNoteRecordByInvoiceID()
        {
            var getAllInvoices = _IInvoiceNote.GetInvoiceNoteRecordByInvoiceID(19, 0, 10);
            Assert.IsTrue(getAllInvoices != null, "Unable to find");
        }


        [TestMethod]
        public void Get_InvoiceNoteDetailsById()
        {
            var getAllInvoices = _IInvoiceNote.GetInvoiceNoteDetailsById(1);
            Assert.IsTrue(getAllInvoices != null, "Unable to find");
        }
    }
}
