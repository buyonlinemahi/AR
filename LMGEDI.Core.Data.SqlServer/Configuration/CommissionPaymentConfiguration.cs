using LMGEDI.Core.Data.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMGEDI.Core.Data.SqlServer.Configuration
{
    public class CommissionPaymentConfiguration : EntityTypeConfiguration<CommissionPayment>
    {
        public CommissionPaymentConfiguration()
            : base()
        {
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.global.tblCommissionPayment, Constant.Consts.Schema.GLOBAL);
            HasKey(commpmt => commpmt.CommissionPaymentID);
            Property(commpmt => commpmt.CommissionPaymentID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
        }
    }
}
