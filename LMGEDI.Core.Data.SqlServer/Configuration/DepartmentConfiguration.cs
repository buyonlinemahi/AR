using LMGEDI.Core.Data.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
namespace LMGEDI.Core.Data.SqlServer.Configuration
{
    public class DepartmentConfiguration : EntityTypeConfiguration<Department>
    {
        public DepartmentConfiguration()
            : base()
        {
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.global.tblDepartment, Constant.Consts.Schema.GLOBAL);
            HasKey(department => department.DepartmentId);
            Property(department => department.DepartmentId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
    }
    }
}
