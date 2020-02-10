using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.ClientCommissionModel
{
    public class CommissionPaymentDetail
    {
        public IEnumerable<CommissionPayment> CommissionPaymentDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
