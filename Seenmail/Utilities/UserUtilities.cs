using MailKit.Security;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Seenmail.Databases.SeenMail;
using Seenmail.IServices.IUtilities;
using Seenmail.Request;

namespace Seenmail.Utilities
{
    public class UserUtilities : IUserUtilities
    {
        SeedMailContext _context;
        private static Dictionary<string, DateTime> emailSendLog = new Dictionary<string, DateTime>();

        public UserUtilities(SeedMailContext context )
        {
           _context = context;
        }

        public async Task<HttpResponseMessage> SendMail(SeedMailRequest request)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.email);
                // nếu user chưa tồn tại gửi
                if (user == null)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Email không chính xác vui lòng thử lại")
                    };
                }

                else
                {
                    if (emailSendLog.ContainsKey(request.email)) // kiem tra xem email nay co yeu cau gui mail gan day k
                    {
                        var timeSend = emailSendLog[request.email];// lay thoi gian gui mail gan nhat
                        if (DateTime.Now - timeSend < TimeSpan.FromMinutes(10))
                        {

                            return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                            {
                                Content = new StringContent($"Vui lòng thử lại sau {DateTime.Now - timeSend} phút nữa")
                            };
                        }
                       
                    }
                        emailSendLog[request.email] = DateTime.Now;
                        Random rd = new Random();
                        var codeRd = rd.Next(000000, 999999); //tạo mã random  6 số ngẫu nhiên

                        var bodyEmail = new BodyBuilder();
                        var email = "vantrongvt1310@gmail.com";
                        var appPass = "vjxv wmvz mokr zwer";
                        var emailMessage = new MimeMessage();
                    // gui mail khi dang ki thanh cong

                    if (request.type == Databases.SendMailStatus.welcome)
                    {
                        if (user != null)
                        {
                            return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                            {
                                Content = new StringContent("Tài khoản đã tồn tại vui lòng đăng nhập")
                            };
                        }
                        emailMessage.From.Add(new MailboxAddress("Từ: ", email));
                        emailMessage.To.Add(new MailboxAddress("Xin Chào : ", user.Email));
                        emailMessage.Subject = "Chào bạn tôi là ... ";
                        bodyEmail.HtmlBody = @$"
                            <h1>Thông Báo Quan Trọng Từ NeonCinemas</h1>
                            <p>Chào mừng , {user.FullName} đã đến với website của chúng tôi </p>
                            <p>Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi! Chúng tôi muốn thông báo rằng bạn ba chấm</p>
                            <p>Nếu bạn có bất kỳ câu hỏi nào, vui lòng liên hệ với chúng tôi qua thông tin dưới đây.</p>
                            <p>Trân trọng,<br>
                            Đội ngũ hỗ trợ khách hàng NeonCinemas</p>	
                            ";
                    
                        emailMessage.Body = bodyEmail.ToMessageBody();
                    }
                        if (request.type == Databases.SendMailStatus.changepass)
                        {
                            // sendmail code doi mk

                            emailMessage.From.Add(new MailboxAddress("Từ: ", email));
                            emailMessage.To.Add(new MailboxAddress("Xin Chào : ", user.Email));
                            emailMessage.Subject = "Chào bạn tôi là ... ";
                            bodyEmail.HtmlBody = @$"
                                <h1>Thông Báo </h1>
                                <h3>Yêu cầu đổi mật khẩu</h3>
                                <p>Chào : {user.FullName}</p>
                                <p>Đây là mã xác nhận của bạn vui lòng không cung cấp cho ai kể cả nhân viên của rạp : {codeRd.ToString()}</p>
                                <button> Đổi mật khẩu </button>
                                <p>Trân trọng,<br>
                                Đội ngũ hỗ trợ khách hàng NeonCinemas</p>	
                                ";
                            emailMessage.Body = bodyEmail.ToMessageBody();
                        }
                        if (request.type == Databases.SendMailStatus.resetpass)
                        {
                            emailMessage.From.Add(new MailboxAddress("Từ: ", email));
                            emailMessage.To.Add(new MailboxAddress("Xin Chào : ", user.Email));
                            emailMessage.Subject = "Chào bạn tôi là ... ";
                            bodyEmail.HtmlBody = @$"
                            <h1>Thông Báo : </h1>
                            <p>Bạn vừa quên mật khẩu ?Bấm vào nút bên dưới để đổi lại mật khẩu  </p>
                            a href=""https://localhost:7211/api/Login/Login?code={codeRd.ToString()}"">
                            <button style=""""background-color: #4CAF50; color: white; padding: 10px 20px; border: none; font-size: 16px;"""">Kích Hoạt</button>
                            </a>"";
                            <p>Trân trọng,<br>
                            Đội ngũ hỗ trợ khách hàng NeonCinemas</p>	
                            ";
                            emailMessage.Body = bodyEmail.ToMessageBody();
                        }
                        using (var client = new MailKit.Net.Smtp.SmtpClient())
                        {
                            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                            client.Authenticate(email, appPass);
                            client.Send(emailMessage);
                            client.Disconnect(true);
                        }
                        return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                        {
                            Content = new StringContent("Thành Công ! Bạn vui lòng kiểm tra email để biết thêm chi tiết")

                        };

                    

                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Có lỗi xảy ra " + ex.Message)

                };

            }
        }


        public async Task<bool> CreateUser(Users newUser)
        {
            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == newUser.Email);
                if (existingUser != null)
                {
                    return false;
                }
                var user = new Users()
                {
                    Address = newUser.Address,
                    Email = newUser.Email,
                    Age = newUser.Age,
                    FullName = newUser.FullName,
                    Gender = newUser.Gender,
                    Id = newUser.Id,
                    PassWord = newUser.PassWord,
                    PhoneNumber = newUser.PhoneNumber,
                    Status = newUser.Status,
                };
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                // Gửi email xác nhận
                var mailRequest = new SeedMailRequest
                {
                    email = user.Email,
                    type = Databases.SendMailStatus.welcome
                };

                var emailResponse = await SendMail(mailRequest);
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

    }
}
