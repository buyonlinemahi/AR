using LMGEDI.Core.Data.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;


namespace LMGEDI.Core.Data.SqlServer.Configuration
{
    public class InvoiceAdjustmentConfiguration : EntityTypeConfiguration<InvoiceAdjustment>
    {
        public InvoiceAdjustmentConfiguration()
            : base()
        {
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.global.tblInvoiceAdjustments, Constant.Consts.Schema.GLOBAL);
            HasKey(invoiceAdjustment => invoiceAdjustment.AdjustmentID);
            Property(invoiceAdjustment => invoiceAdjustment.AdjustmentID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
        }
    }
}
