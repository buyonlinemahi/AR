using LMGEDI.Core.Data.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMGEDI.Core.Data.SqlServer.Configuration
{
   public class ICRecordDetailConfiguration:EntityTypeConfiguration<ICRecordDetail>
    {
       public ICRecordDetailConfiguration()
            : base()
        {
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.global.tblICDetailRecord, Constant.Consts.Schema.GLOBAL);
            HasKey(IC => IC.ICRecordDetailID);
            Property(IC => IC.ICRecordDetailID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();          

        }
    }
}
