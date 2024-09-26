using Seenmail.Databases;

namespace Seenmail.Request
{
    public class SeedMailRequest
    {
        public string email { get; set; }
        public SendMailStatus type { get; set; }
    }
}
