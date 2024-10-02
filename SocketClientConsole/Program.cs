using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketClient
{
    public class Program
    {
        private static readonly string serverIp = "192.168.80.221"; // Địa chỉ IP server
        private static readonly int port = 9000; // Cổng server

        public static async Task Main(string[] args)
        {
            using var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            await client.ConnectAsync(new IPEndPoint(IPAddress.Parse(serverIp), port));
            Console.WriteLine("Đã kết nối với máy chủ!");

            // Vòng lặp để người dùng gửi tin nhắn
            while (true)
            {
                Console.Write("Nhập tin nhắn (nhập 'close' để thoát): ");
                string message = Console.ReadLine();

                // Kiểm tra điều kiện thoát
                if (message.ToLower() == "close")
                {
                    break;
                }

                // Gửi tin nhắn
                await SendMessage(client, message);
            }

            client.Shutdown(SocketShutdown.Both);
            client.Close();
            Console.WriteLine("Connection closed.");
        }

        private static async Task SendMessage(Socket client, string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            await client.SendAsync(data, SocketFlags.None);
            Console.WriteLine($"Sent: {message}");

            // Nhận phản hồi từ server
            byte[] buffer = new byte[2048];
            int bytesReceived = await client.ReceiveAsync(buffer, SocketFlags.None);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
            Console.WriteLine($"Received: {response}");
        }
    }
}
