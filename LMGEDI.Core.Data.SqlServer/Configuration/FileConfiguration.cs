using LMGEDI.Core.Data.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMGEDI.Core.Data.SqlServer.Configuration
{
    public class FileConfiguration : EntityTypeConfiguration<File>
    {
        public FileConfiguration()
            : base()
        {
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.global.tblFile, Constant.Consts.Schema.GLOBAL);
            HasKey(file => file.FileID);
            Property(file => file.FileID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            

        }
    }
}
