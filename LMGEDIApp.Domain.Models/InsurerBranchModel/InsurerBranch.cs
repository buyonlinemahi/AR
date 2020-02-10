using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.InsurerBranchModel
{
    public class InsurerBranch
    {
        public int InsurerBranchId { get; set; }
        public int InsurerId { get; set; }
        public string InsurerBranchName { get; set; }
    }
}
