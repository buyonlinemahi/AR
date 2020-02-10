using LMGEDI.Core.Data.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMGEDI.Core.Data.SqlServer.Configuration
{
    public class CommissionConfiguration : EntityTypeConfiguration<Commission>
    {
        public CommissionConfiguration()
            : base()
        {
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.global.tblCommission, Constant.Consts.Schema.GLOBAL);
            HasKey(comm=> comm.CommissionID);
            Property(comm => comm.CommissionID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
        }
    }
}
