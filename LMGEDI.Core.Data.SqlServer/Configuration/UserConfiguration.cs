using LMGEDI.Core.Data.Model;
using System.Data.Entity.ModelConfiguration;

namespace LMGEDI.Core.Data.SqlServer.Configuration
{
    class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration(): base()
        {
            //ToTable("tblUsers", Constant.Consts.Schema.GLOBAL);
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.global.tblUser, Constant.Consts.Schema.GLOBAL);
            HasKey(user => user.UserID);
        }
    }
}
