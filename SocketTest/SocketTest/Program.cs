using System;
using System.Text;
using System.Net;
using System.Net.Sockets;


namespace SocketTest
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ip;
            if (IPAddress.TryParse("192.168.3.113", out ip))
            { }
            else
            {
                throw new Exception("Wrong IP");
            }

            int port = 8000;

            serverSocket.Bind(new IPEndPoint(ip, port));

            serverSocket.Listen(10);

            Console.WriteLine("Server Started");

            while (true)
            {
                try
                {
                    Socket client = serverSocket.Accept();
                    //Console.WriteLine(client.RemoteEndPoint.ToString());
                    byte[] msgArr = new byte[1024];
                    int length = client.Receive(msgArr);

                    string msg = Encoding.UTF8.GetString(msgArr, 0, length);

                    Console.WriteLine(msg);

                    string returnMsg = "Message Recieved";
                    client.Send(Encoding.UTF8.GetBytes(returnMsg));
                }
                catch (Exception err)
                {
                    if (err is SocketException || err is ArgumentException || err is InvalidOperationException || err is ObjectDisposedException)
                    {
                        Console.WriteLine("Socket Error: " + err);
                    }
                    else if(err is System.Security.SecurityException)
                    {
                        Console.WriteLine("Recieve Error" + err);
                    }
                }
            }
            //Console.ReadLine();
        }
    }
}
