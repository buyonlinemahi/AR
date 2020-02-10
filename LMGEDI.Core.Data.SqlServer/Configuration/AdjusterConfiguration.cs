using LMGEDI.Core.Data.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;


namespace LMGEDI.Core.Data.SqlServer.Configuration
{
    public class AdjusterConfiguration : EntityTypeConfiguration<Adjuster>
    {
        public AdjusterConfiguration()
            : base()
        {
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.global.tblAdjuster, Constant.Consts.Schema.GLOBAL);
            HasKey(adjuster => adjuster.AdjusterId);
            Property(adjuster => adjuster.AdjusterId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
        }
    }
}
