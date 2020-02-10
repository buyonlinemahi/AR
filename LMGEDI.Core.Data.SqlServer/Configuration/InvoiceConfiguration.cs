using LMGEDI.Core.Data.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMGEDI.Core.Data.SqlServer.Configuration
{
    public class InvoiceConfiguration : EntityTypeConfiguration<Invoice>
    {
        public InvoiceConfiguration()
            : base()
        {
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.global.tblInvoice, Constant.Consts.Schema.GLOBAL);
            HasKey(invoice => invoice.InvoiceID);
            Property(invoice => invoice.InvoiceID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(invoice => invoice.InvoiceAmt);
            Property(invoice => invoice.InvoiceNumber);
            Property(invoice => invoice.InvoiceDate);
            Property(invoice => invoice.InvoiceSent);
            Property(invoice => invoice.BillingWeek);
            Property(invoice => invoice.InvoiceBalanceAmt);
        }
    }
}
