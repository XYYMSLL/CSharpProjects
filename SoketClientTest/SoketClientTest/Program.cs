using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SoketClientTest
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip;

            if (IPAddress.TryParse("192.168.3.117", out ip))
            { }
            else
            {
                throw new Exception("Wrong IP");
            }

            int port = 8000;

            clientSocket.Connect(new IPEndPoint(ip, port));
            Console.WriteLine("Server Connect");

            while (true)
            {
                string msg = Console.ReadLine();
                byte[] msgArr = Encoding.UTF8.GetBytes(msg);

                clientSocket.Send(msgArr);

                byte[] newMsgArr = new byte[1024];
                int length = clientSocket.Receive(newMsgArr);

                msg = Encoding.UTF8.GetString(newMsgArr, 0, length);
                Console.WriteLine(msg);
            }
        }
    }
}
