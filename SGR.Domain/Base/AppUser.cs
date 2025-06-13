
namespace SGR.Domain.Base
{
    public abstract class AppUser : AuditEntity
    {
        public required int IdUser { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public required string Email { get; set; }
        public required string Password { get; set; }

    }
}
