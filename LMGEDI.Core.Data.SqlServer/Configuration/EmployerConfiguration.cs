using LMGEDI.Core.Data.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMGEDI.Core.Data.SqlServer.Configuration
{
    public class EmployerConfiguration : EntityTypeConfiguration<Employer>
    {
        public EmployerConfiguration()
            : base()
        {
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.global.tblEmployer, Constant.Consts.Schema.GLOBAL);
            HasKey(employer => employer.EmployerId);
            Property(employer => employer.EmployerId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
    }
    }
}
