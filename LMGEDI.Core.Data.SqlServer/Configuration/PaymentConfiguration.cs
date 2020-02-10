using LMGEDI.Core.Data.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMGEDI.Core.Data.SqlServer.Configuration
{
    public class PaymentConfiguration : EntityTypeConfiguration<Payment>
    {
        public PaymentConfiguration()
            : base()
        {
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.global.tblPayment, Constant.Consts.Schema.GLOBAL);
            HasKey(payment => payment.PaymentId);
            Property(payment => payment.PaymentId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(payment => payment.InvoiceId).IsRequired();
            Property(payment => payment.PaymentAmount).IsRequired();
            Property(payment => payment.PaymentReceived).IsRequired();
            Property(payment => payment.CheckNumber).IsRequired();
            Property(payment => payment.CheckUploadName);
        }
    }
}
