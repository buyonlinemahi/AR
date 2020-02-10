using LMGEDI.Core.Data.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMGEDI.Core.Data.SqlServer.Configuration
{
    public class InsurerConfiguration : EntityTypeConfiguration<Insurer>
    {
        public InsurerConfiguration()
            : base()
        {
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.global.tblInsurer, Constant.Consts.Schema.GLOBAL);
            HasKey(insurer => insurer.InsurerId);
            Property(insurer => insurer.InsurerId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(insurer => insurer.InsurerName).IsRequired();
        }
    }
}
