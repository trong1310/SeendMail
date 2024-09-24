using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Seenmail.Databases.SeenMail;
using Seenmail.Request;

namespace Seenmail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeenMaillController : ControllerBase
    {
        SeedMailContext _context;
        public SeenMaillController(SeedMailContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> SeenMaill(SeedMailRequest request)
        {
              
              

                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.email);
                var bodyEmail = new BodyBuilder();
                var email = "vantrongvt1310@gmail.com";
                var appPass = "vjxv wmvz mokr zwer";
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("Từ: ", email));
                emailMessage.To.Add(new MailboxAddress("Xin Chào : ", user.Email));
                emailMessage.Subject = "Chào bạn tôi là ... ";
                bodyEmail.HtmlBody = @$"
                <h1>Thông Báo Quan Trọng Từ NeonCinemas</h1>
                <p>Chào bạn, {user.FullName}</p>
                <p>Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi! Chúng tôi muốn thông báo rằng bạn ba chấm</p>
                <p>Nếu bạn có bất kỳ câu hỏi nào, vui lòng liên hệ với chúng tôi qua thông tin dưới đây.</p>
                <p>Trân trọng,<br>
                Đội ngũ hỗ trợ khách hàng NeonCinemas</p>	
                ";
                emailMessage.Body = bodyEmail.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    client.Authenticate(email, appPass);
                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
                return Ok(user);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser(Users users)
        {
            var userdata = new Users()
            {
                Id = users.Id,
                Address = users.Address,
                Age = users.Age,
                Email = users.Email,
                FullName = users.FullName,
                Gender = users.Gender,
                PassWord = users.PassWord,
                PhoneNumber = users.PhoneNumber,
                Status = users.Status,
            };
           await _context.Users.AddAsync(userdata);
            await _context.SaveChangesAsync();
            return Ok(users);
        }
    }
}
