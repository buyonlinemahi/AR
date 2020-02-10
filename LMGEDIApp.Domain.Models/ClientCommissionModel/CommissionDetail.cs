using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.ClientCommissionModel
{
    public class CommissionDetail
    {
        public IEnumerable<CommissionSearch> CommissionDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
