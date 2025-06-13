
namespace SGR.Domain.Base
{
    public abstract class AuditEntity
    {
        public DateTime CreatedAt { get; set ; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime DeletedAt { get; set; }



    }
}
