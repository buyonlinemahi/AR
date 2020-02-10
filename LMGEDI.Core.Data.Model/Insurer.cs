
namespace LMGEDI.Core.Data.Model
{
    public class Insurer
    {
        public int InsurerId   { get; set; }
        public string InsurerName  { get; set; }
        public string InsurerAddress { get; set; }
        public string InsurerCity { get; set; }
        public int InsurerStateID { get; set; }
        public string InsurerZip { get; set; }
        public string InsurerPhone { get; set; }
    }
}
