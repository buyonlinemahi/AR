
namespace LMGEDI.Core.Data.Model
{
    public class InsurerBranch
    {
        public int InsurerBranchId { get; set; }
        public int InsurerId { get; set; }
        public string InsurerBranchName { get; set; }
        public string InsurerBranchAddress { get; set; }
        public string InsurerBranchCity { get; set; }
        public int InsurerBranchStateID { get; set; }
        public string InsurerBranchZip { get; set; }
        public string InsurerBranchPhone { get; set; }
    }
}
