using LMGEDI.Core.Data.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMGEDI.Core.Data.SqlServer.Configuration
{
    public class PendingUploadConfiguration : EntityTypeConfiguration<PendingUpload>
    {
        public PendingUploadConfiguration()
            : base()
        {
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.global.tblPendingUpload, Constant.Consts.Schema.GLOBAL);
            HasKey(pendingUpload => pendingUpload.PendingUploadId);
            Property(pendingUpload => pendingUpload.PendingUploadId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
             
        }
    }
}
