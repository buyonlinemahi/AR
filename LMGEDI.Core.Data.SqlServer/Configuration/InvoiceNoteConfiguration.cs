using LMGEDI.Core.Data.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace LMGEDI.Core.Data.SqlServer.Configuration
{
    class InvoiceNoteConfiguration : EntityTypeConfiguration<InvoiceNote>
    {
        public InvoiceNoteConfiguration()
            : base()
        {
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.global.tblInvoiceNote, Constant.Consts.Schema.GLOBAL);
            HasKey(invoiceNote => invoiceNote.InvoiceNotesID);
            Property(invoiceNote => invoiceNote.InvoiceNotesID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
        }
    }
}
