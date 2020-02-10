using LMGEDI.Core.Data.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMGEDI.Core.Data.SqlServer.Configuration
{
    public class LienTempTableConfiguration : EntityTypeConfiguration<LienTempTable>
    {
        public LienTempTableConfiguration()
            : base()
        {
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.global.tblLienTempTable, Constant.Consts.Schema.GLOBAL);
            HasKey(lienTempTable => lienTempTable.UploadId);
            Property(lienTempTable => lienTempTable.UploadId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
        }
    }
}
