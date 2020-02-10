using Core.Base.Data.SqlServer;
using Core.Base.Data.SqlServer.Factory;
using Core.Base.Data.SqlServer.Repository;
using LMGEDI.Core.Data.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


namespace LMGEDI.Core.Data.SqlServer.Repository
{
    public class OCRRepository : BaseRepository<OCR, LMGEDIDBContext>, IOCRRepository
    {
        public OCRRepository(IContextFactory<LMGEDIDBContext> contextFactory) :
            base(new BaseUnitOfWork<LMGEDIDBContext>(contextFactory), contextFactory)
        {
        }
        public IEnumerable<OCRFileInvoices> GetOCRFilesInvoiceRecords(string SearchText, int skip, int take)
        {
            SqlParameter _SearchText = new SqlParameter("@SearchText", SearchText);
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);
            return Context.Database.SqlQuery<OCRFileInvoices>(Constant.StoredProcedureConst.OCRRepositoryProcedure.GetOCRFilesInvoiceRecords, _SearchText, _skip, _take).ToList();
        }
        public int GetOCRFilesInvoiceRecordsCount(string SearchText)
        {
            SqlParameter _SearchText = new SqlParameter("@SearchText", SearchText);
            return Context.Database.SqlQuery<int>(Constant.StoredProcedureConst.OCRRepositoryProcedure.GetOCRFilesInvoiceRecordsCount, _SearchText).SingleOrDefault();
        }
        public IEnumerable<OCRPaymentDetails> GetOCRPaymentFilesRecords(string SearchText, int skip, int take)
        {
            SqlParameter _SearchText = new SqlParameter("@SearchText", SearchText);
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);
            return Context.Database.SqlQuery<OCRPaymentDetails>(Constant.StoredProcedureConst.OCRRepositoryProcedure.GetOCRPaymentFilesRecords, _SearchText, _skip, _take).ToList();
        }
        public int GetOCRPaymentFilesRecordsCount(string SearchText)
        {
            SqlParameter _SearchText = new SqlParameter("@SearchText", SearchText);
            return Context.Database.SqlQuery<int>(Constant.StoredProcedureConst.OCRRepositoryProcedure.GetOCRPaymentFilesRecordsCount, _SearchText).SingleOrDefault();
        }
        public OCRPayment AddOCRPaymentRecords(OCRPaymentSave oCRPaymentModel)
        {
            SqlParameter _OCRId = new SqlParameter("@OCRId", oCRPaymentModel.OCRId);
            SqlParameter _CreatedBy = new SqlParameter("@CreatedBy", oCRPaymentModel.CreatedBy);
            SqlParameter _InvoiceId = new SqlParameter("@InvoiceId", oCRPaymentModel.InvoiceID);
            SqlParameter _PaymentAmount = new SqlParameter("@PaymentAmount", oCRPaymentModel.PaymentAmount);
            SqlParameter _PaymentReceived = new SqlParameter("@PaymentReceived", oCRPaymentModel.PaymentReceived);
            SqlParameter _CheckNumber = new SqlParameter("@CheckNumber", oCRPaymentModel.CheckNumber);
            SqlParameter _CheckDate = new SqlParameter("@CheckDate", oCRPaymentModel.CheckDate);
            SqlParameter _CheckUploadName = new SqlParameter("@CheckUploadName", oCRPaymentModel.CheckUploadName);
            return Context.Database.SqlQuery<OCRPayment>(Constant.StoredProcedureConst.OCRRepositoryProcedure.AddOCRPaymentRecords, _OCRId, _CreatedBy, _InvoiceId, _PaymentAmount, _PaymentReceived, _CheckNumber, _CheckDate, _CheckUploadName).SingleOrDefault();
        }
    }
}
