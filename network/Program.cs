using System.Net;
using System.Net.Sockets;
using System.Text;

namespace network;

class Program
{
    static void Main(string[] args)
    {
        Server("Hello");
    }

    
    public static void Server(string name)
    {
        UdpClient udpClient = new UdpClient(12345);
        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 0);
        // Console.Clear();
        Console.WriteLine("Сервер ждёт сообщения от клиента");
        
        while (true)
        {
            byte[] buffer = udpClient.Receive(ref ipEndPoint);
            var messageText = Encoding.UTF8.GetString(buffer);
            Message? message = Message.DeserializeFromJson(messageText);
            message?.Print();
            // отправка ответа от сервера            
            byte[] data = Encoding.UTF8.GetBytes(messageText);
            udpClient.Send(data, data.Length, ipEndPoint);


        }
    }
}