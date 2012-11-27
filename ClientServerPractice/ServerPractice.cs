using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

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
            IPAddress ipAddress = IPAddress.Parse(ipString);
            
            TcpListener listener = new TcpListener(ipAddress, portNum);

            listener.Start();

            
            Console.Write("Waiting for connection...");
            TcpClient client = listener.AcceptTcpClient();
            
            Console.WriteLine("Connection accepted.");
            NetworkStream networkStream = client.GetStream();
            StreamReader reader = new StreamReader(networkStream);
            StreamWriter writer = new StreamWriter(networkStream) { AutoFlush = true };

            string goodbyeMsg = "This would end play. Thank you, goodbye.";
            bool done = false;

            while (!done)
            {
                try 
                {
                    string clientInput = reader.ReadLine();
                    Console.WriteLine("Server reading from client: {0}", clientInput);
                    if (clientInput == "y")
                    {
                        writer.WriteLine("This would continue play.");
                    }
                    else if (clientInput == "n")
                    {
                        writer.WriteLine(goodbyeMsg);
                        done = true;
                    }
                    else
                    {
                        writer.WriteLine("Invalid entry.");
                    }                    
                    
                } catch (Exception exception) 
                {
                    Console.WriteLine(exception.ToString());
                } 
            }
                


                networkStream.Close();
                client.Close(); 

            listener.Stop();
        }  
    }    
}
