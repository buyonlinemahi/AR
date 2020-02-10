using LMGEDI.Core.Data.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMGEDI.Core.Data.SqlServer.Configuration
{
    class OCRConfiguration : EntityTypeConfiguration<OCR>
    {
        public OCRConfiguration()
            : base()
        {
            ToTable(LMGEDI.Core.Data.SqlServer.Constant.Tables.global.tblOCR, Constant.Consts.Schema.GLOBAL);
            HasKey(Ocr => Ocr.OcrId);
            Property(Ocr => Ocr.OcrId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
        }

    }
}
