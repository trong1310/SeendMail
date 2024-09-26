using Microsoft.AspNetCore.Mvc;
using Seenmail.Databases.SeenMail;
using Seenmail.Request;

namespace Seenmail.IServices.IUtilities
{
    public interface IUserUtilities
    {
        public Task<HttpResponseMessage> SendMail(SeedMailRequest request);
        public  Task<bool> CreateUser(Users newUser);

    }
}
