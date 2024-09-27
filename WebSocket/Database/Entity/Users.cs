using System.ComponentModel.DataAnnotations;
using WebSocket.Database.Enum;

namespace WebSocket.Database.Entity
{
    public class Users
    {
        [Key]
        public Guid Id { get; set; }

        public string Email { get; set; } = null!;

        public string? FullName { get; set; }

        public string? PassWord { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public int? Age { get; set; }

        public string? Gender { get; set; }

        public UserStatus? Status { get; set; }
        public Guid RoleID { get; set; }
        public virtual Roles? Roles { get; set; }
    }
}
