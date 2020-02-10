
using System;
using System.Collections.Generic;
using LMGEDIApp.Domain.Models.DepartmentModel;
using LMGEDIApp.Domain.Models.StateModel;

namespace LMGEDIApp.Domain.Models.FileModel
{
    public class FileDetail
    {
        public char DBconnection { get; set; }
        public int FileID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClaimNumber { get; set; }
        public int InsurerId { get; set; }
        public int InsurerBranchId { get; set; }
        public int EmployerId { get; set; }
        public int AdjusterId { get; set; }
        public string InsurerName { get; set; }
        public string InsurerBranchName { get; set; }
        public string EmployerName { get; set; }
        public AdjusterModel.AdjusterSearch AdjusterDetails { get; set; }
        public int InvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public int DepartmentId { get; set; }
        public decimal? InvoiceAmt { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? InvoiceSent { get; set; }
        public DateTime? BillingWeek { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public string Notes { get; set; }

        public bool IsLienClaimNumber { get; set; }
        public bool IsLienInsurerID { get; set; }
        public bool IsLienInsurerBranchID { get; set; }
        public bool IsLienEmployerID { get; set; }
        public bool IsLienAdjusterID { get; set; }


        //ICRecordDetails 
        public int ICRecordDetailID { get; set; }
        public string ICRecordDetailAddress { get; set; }
        public string ICRecordDetailCity { get; set; }
        public int StateID { get; set; }
        public string ICRecordDetailZip { get; set; }
        public string ICRecordDetailTaxID { get; set; }
        public IEnumerable<State> States { get; set; }
    }
}
