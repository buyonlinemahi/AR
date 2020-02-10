using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.InsurerBranchModel
{
    public class InsurerBranchSearch
    {
        public int InsurerBranchId { get; set; }
        public int InsurerId { get; set; }
        public string InsurerBranchName { get; set; }
        public string InsurerBranchAddress { get; set; }
        public string InsurerBranchCity { get; set; }
        public string InsurerBranchStateName { get; set; }
        public string InsurerBranchZip { get; set; }
        public string InsurerBranchPhone { get; set; }
        public string DBNameInsurerBranch { get; set; }
    }
}
