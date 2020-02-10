using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.InsurerBranchModel
{
    public class InsurerBranchSearchViewViewModel
    {
        public IEnumerable<InsurerBranchSearch> InsurerBranchSearch { get; set; }
        public int InsurerBranchCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
