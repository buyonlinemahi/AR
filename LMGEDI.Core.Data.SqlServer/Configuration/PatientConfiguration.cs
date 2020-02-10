using LMGEDI.Core.Data.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace LMGEDI.Core.Data.SqlServer.Configuration
{
 
    class PatientConfiguration : EntityTypeConfiguration<Patient>
    {
        public PatientConfiguration()
            : base()
        {
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.global.tblPatient, Constant.Consts.Schema.GLOBAL);
            HasKey(patient => patient.PatientID);
            Property(patient => patient.PatientID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
        }
    }
}
