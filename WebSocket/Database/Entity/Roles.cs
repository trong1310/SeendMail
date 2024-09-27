namespace WebSocket.Database.Entity
{
    public class Roles
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<Users>? Users { get; set; }
    }
}
