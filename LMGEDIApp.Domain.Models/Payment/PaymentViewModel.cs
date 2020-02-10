using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.PaymentModel
{
    public class PaymentViewModel
    {
        public IEnumerable<Payment> PaymentDetails { get; set; }
        public int PaymentCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
