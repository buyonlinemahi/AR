using LMGEDI.Core.Data.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMGEDI.Core.Data.SqlServer.Configuration
{
    public class StateConfiguration : EntityTypeConfiguration<State>
    {
        public StateConfiguration()
            : base()
        {
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.lookup.State, Constant.Consts.Schema.LOOKUP);
            HasKey(state => state.StateID);
            Property(state => state.StateID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
        }
    }
}
