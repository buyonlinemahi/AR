using LMGEDI.Core.Data.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMGEDI.Core.Data.SqlServer.Configuration
{
    public class PaymentRefundConfiguration : EntityTypeConfiguration<PaymentRefund>
    {
        public PaymentRefundConfiguration()
            : base()
        {
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.global.tblPaymentRefund, Constant.Consts.Schema.GLOBAL);
            HasKey(paymentRF => paymentRF.PaymentRefundID);
            Property(paymentRF => paymentRF.PaymentRefundID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(paymentRF => paymentRF.InvoiceId).IsRequired();
            Property(paymentRF => paymentRF.RefundAmount).IsRequired();
            Property(paymentRF => paymentRF.RefundReceived).IsRequired();
            Property(paymentRF => paymentRF.CheckNumber).IsRequired();
            Property(paymentRF => paymentRF.CheckUploadName);
        }
    }
}
