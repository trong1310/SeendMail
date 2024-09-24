using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using SeendMail.IServices;

namespace SeendMail.Services
{
	public class SeenMaillServices : ISeenMaillServices
	{

        public async Task<bool> SeenMail(string emailRequest)
		{
			try
			{

				//var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == emailRequest);
				//var bodyEmail = new BodyBuilder();
				//var email = "vantrongvt1310@gmail.com";
				//var appPass = "vjxv wmvz mokr zwer";
				//var emailMessage = new MimeMessage();
				//emailMessage.From.Add(new MailboxAddress("Từ: ", email));
				//emailMessage.To.Add(new MailboxAddress("Xin Chào : ", emailRequest));
				//emailMessage.Subject = "Chào bạn tôi là ... ";
				//bodyEmail.HtmlBody = @$"
				//<h1>Thông Báo Quan Trọng Từ NeonCinemas</h1>
				//<p>Chào bạn, {user.FullName}</p>
				//<p>Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi! Chúng tôi muốn thông báo rằng bạn ba chấm</p>
				//<p>Nếu bạn có bất kỳ câu hỏi nào, vui lòng liên hệ với chúng tôi qua thông tin dưới đây.</p>
				//<p>Trân trọng,<br>
				//Đội ngũ hỗ trợ khách hàng NeonCinemas</p>	
				//";
				//emailMessage.Body = bodyEmail.ToMessageBody();

				//using (var client = new MailKit.Net.Smtp.SmtpClient())
				//{
				//	client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
				//	client.Authenticate(email, appPass);
				//	client.Send(emailMessage);
				//	client.Disconnect(true);
				//}
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
