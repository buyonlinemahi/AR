
namespace LMGEDIApp.Domain.Models.FileModel
{
    public class FileSearchResult
    {
        //public string DBConnetion { get; set; }
        public int FileID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClaimNumber { get; set; }
        public int InsurerID { get; set; }
        public string InsurerName { get; set; }
        public int InsurerBranchID { get; set; }
        public string InsurerBranchName { get; set; }
        public int EmployerID { get; set; }
        public string EmployerName { get; set; }
        public int InvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
    }
}
