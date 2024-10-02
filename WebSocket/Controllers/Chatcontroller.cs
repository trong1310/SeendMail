using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WebSocket.Request;


namespace WebSocket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly string _serverIp = "127.0.0.1"; // Địa chỉ IP server
        private readonly int _port = 9000; // Cổng server

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] MessageRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.message))
            {
                return BadRequest("Tin nhắn không hợp lệ.");
            }

            try
            {
                using var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                await client.ConnectAsync(new IPEndPoint(IPAddress.Parse(_serverIp), _port));

                // Gửi tin nhắn
                byte[] data = Encoding.UTF8.GetBytes(request.message);
                await client.SendAsync(data, SocketFlags.None);

                // Nhận phản hồi từ server
                byte[] buffer = new byte[2048];
                int bytesReceived = await client.ReceiveAsync(buffer, SocketFlags.None);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

                return Ok(new { Response = response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi gửi tin nhắn: {ex.Message}");
            }
        }
    }
}
