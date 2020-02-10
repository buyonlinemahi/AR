using LMGEDI.Core.Data.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMGEDI.Core.Data.SqlServer.Configuration
{
    public class InsurerBranchConfiguration : EntityTypeConfiguration<InsurerBranch>
    {
        public InsurerBranchConfiguration()
            : base()
        {
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.global.tblInsurerBranch, Constant.Consts.Schema.GLOBAL);
            HasKey(insurerbranch => insurerbranch.InsurerBranchId);
            Property(insurerbranch => insurerbranch.InsurerBranchId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(insurerbranch => insurerbranch.InsurerId).IsRequired();
            Property(insurerbranch => insurerbranch.InsurerBranchName).IsRequired();
        }
    }
}
