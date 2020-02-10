using LMGEDI.Core.Data.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace LMGEDI.Core.Data.SqlServer.Configuration
{
    class PatientHistoryConfiguration : EntityTypeConfiguration<PatientHistory>
    {
        public PatientHistoryConfiguration()
            : base()
        {
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.global.tblPatientHistory, Constant.Consts.Schema.GLOBAL);
            HasKey(patientHistory => patientHistory.PatientHistoryID);
            Property(patientHistory => patientHistory.PatientHistoryID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(patientHistory => patientHistory.PatientID).IsRequired();
            Property(patientHistory => patientHistory.Description);
        }
    }
}
