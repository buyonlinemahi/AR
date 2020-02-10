using System;

namespace LMGEDI.Core.Data.Model
{
    public class PendingUpload
    {
        public int PendingUploadId { get; set; }
        public string PendingUploadName { get; set; }
        public DateTime PendingUploadDate { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; } 
        public Boolean? IsDeleted { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public Boolean? IsProcessed { get; set; }
        public int? ProcessedBy { get; set; }
        public DateTime? ProcessedOn { get; set; }
    }

    public class PendingUploadRecord
    {
        public int PendingUploadId { get; set; }
        public string PendingUploadName { get; set; }
        public DateTime PendingUploadDate { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public Boolean? IsDeleted { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Username { get; set; }
    }
}
