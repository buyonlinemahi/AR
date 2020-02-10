using LMGEDIApp.Domain.Models.InvoiceModel;

namespace LMGEDIApp.Domain.Models.FileModel
{
    public class FileDetailViewModel
    {
        public InvoiceViewModel InvoiceViewModel { get; set; }
        public FileDetail FileDetail { get; set; }
    }
}
