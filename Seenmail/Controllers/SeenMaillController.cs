using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Seenmail.Databases;
using Seenmail.Databases.SeenMail;
using Seenmail.IServices.IUtilities;
using Seenmail.Request;

namespace Seenmail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeenMaillController : ControllerBase
    {
        private readonly SeedMailContext _context;
        private readonly IUserUtilities _userUtilities;
        public SeenMaillController(SeedMailContext context, IUserUtilities userUtilities)
        {
            _userUtilities = userUtilities;
            _context = context;
        }
        [HttpPost("SendMail")]
        public async Task<IActionResult> SeenMaill([FromBody]SeedMailRequest request)
        {
            try
            {
                var sendmail = await _userUtilities.SendMail(request);
                return Ok(sendmail);
            }
            catch (Exception ex) 
            { 
             return BadRequest(ex.Message);
            }
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser(Users users)
        {
            var obj = await _userUtilities.CreateUser(users);
            return Ok(obj);
        }
    }
}
