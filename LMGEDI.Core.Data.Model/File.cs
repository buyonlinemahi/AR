using System;

namespace LMGEDI.Core.Data.Model
{
    public class File
    {
        public int FileID { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClaimNumber { get; set; }
        public int InsurerId { get; set; }
        public int InsurerBranchId { get; set; }
        public int EmployerId { get; set; }
        public int AdjusterId { get; set; }
        public bool IsLienClaimNumber { get; set; }
        public bool IsLienInsurerID { get; set; }
        public bool IsLienInsurerBranchID { get; set; }
        public bool IsLienEmployerID { get; set; }
        public bool IsLienAdjusterID { get; set; }
        public bool IsDeleted { get; set; }    
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Notes { get; set; }
        public int DepartmentID { get; set; }
    }
}
