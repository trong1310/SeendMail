
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WebSocket.SocketServer
{
    public class SocketServers : BackgroundService
    {
        private readonly ILogger _logger;
        public SocketServers(ILogger logger)
        {
             _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Vui lòng chờ trong giây lát ......");
            var ip = IPAddress.Any; // chap nhan tat ca ip yeu cau ket noi
            var port = 9000; // cổng lắng nghe kết nối của  tcp
            var socket = new Socket(ip.AddressFamily, SocketType.Stream,ProtocolType.Tcp);
            try
            {
                socket.Bind(new IPEndPoint(ip, port));  // gắn kết nối ip và cổng 
                socket.Listen(8); // giới hạn số người trong hàng chờ kết nối
                _logger.LogInformation($"Bạn đang trong hàng đợi vui lòng chờ tới lượt của mình");
                while (!cancellationToken.IsCancellationRequested) // vòng lặp chạy liên tục nhận yêu cầu kết nối từ người dùng
                                                                   // , khi nhận được yêu cầu dừng từ cancellationToken sẽ dừng vòng lặp
                {
                    Socket handler = socket.Accept();
                    byte[] bytes = new byte[2048];
                    int bytereceive = await handler.ReceiveAsync(bytes, SocketFlags.None); // nhận meesage từ người dùng và lưu vào mảng byte có kích thước tối đa là 2048  
                    string message = Encoding.UTF8.GetString(bytes,0,bytereceive); // convert mảng byte qua chuỗi string
                    _logger.LogInformation($"Client : {message}"); // lưu lại nội dung nhận từ client
                        
                }
            }
            catch (Exception ex) 
            {
                
            }
        }
    }
}
