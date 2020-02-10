using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMGEDIApp.Domain.Models.AdjusterModel;
using LMGEDIApp.Domain.Models.EmployerModel;
using LMGEDIApp.Domain.Models.FileModel;
using LMGEDIApp.Domain.Models.InsurerModel;
using LMGEDIApp.Domain.Models.InsurerBranchModel;
using LMGEDIApp.Domain.Models.InvoiceModel;

namespace LMGEDIApp.Domain.Models.FileModel
{
    public class FileViewModel
    {
        public File Files { get; set; }

        public Adjuster Adjusters { get; set; }
        public IEnumerable<AdjusterSearch> AdjusterDetails { get; set; }

        public Employer Employers { get; set; }
        public IEnumerable<EmployerSearch> EmployerDetails { get; set; }


        public Insurer Insurers { get; set; }
        public IEnumerable<InsurerSearch> InsurerDetails { get; set; }


        public InsurerBranch InsurerBranches { get; set; }
        public IEnumerable<InsurerBranchSearch> InsurerBranchesDetails { get; set; }


        public Invoice Invoices { get; set; }
       
    }
}
