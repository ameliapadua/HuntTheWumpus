using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HuntTheWumpus.ClientServerPractice
{
    public class TcpTimeServer 
    {
        private const int portNum = 3000;
        private string ipString = "127.0.0.1";

        public static void Main(String[] args) 
        {
            TcpTimeServer tcpTimeServer = new TcpTimeServer();
            tcpTimeServer.ServerConnect();
        }

        public void ServerConnect()
        {
            bool done = false;
            IPAddress ipAddress = IPAddress.Parse(ipString);
            
            TcpListener listener = new TcpListener(ipAddress, portNum);

            listener.Start();

            while (!done) {
                Console.Write("Waiting for connection...");
                TcpClient client = listener.AcceptTcpClient();
                
                Console.WriteLine("Connection accepted.");
                NetworkStream ns = client.GetStream();

                byte[] byteTime = Encoding.ASCII.GetBytes(DateTime.Now.ToString());

                try {
                    ns.Write(byteTime, 0, byteTime.Length);
                    ns.Close();
                    client.Close();
                } catch (Exception e) {
                    Console.WriteLine(e.ToString());
                }
            }

            listener.Stop();
        }  
    }    
}
