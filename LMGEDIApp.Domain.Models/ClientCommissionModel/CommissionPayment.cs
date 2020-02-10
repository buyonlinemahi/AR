using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.ClientCommissionModel
{
    public class CommissionPayment
    {
        public int CommissionPaymentID { get; set; }
        public int CommissionID { get; set; }
        public decimal? CPPaymentPaidAmount { get; set; }
        public string CPCheckNumber { get; set; }
        public DateTime CPCheckSentOn { get; set; }
        public int InvoiceDateQuarter { get; set; }
        public bool? IsPaymentRecevied { get; set; }
    }
}
