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


                    try 
                    {
                        string clientInput = reader.ReadLine();
                        Console.WriteLine("Server reading from client: {0}", clientInput);
                        if (clientInput == "y")
                        {
                            writer.WriteLine("This would continue play.");
                        }
                        else
                        {
                            writer.WriteLine("This would end play");
                        }
                        writer.WriteLine("Test");
                        writer.WriteLine("Thank you, goodbye");
                        
                    } catch (Exception e) {
                        Console.WriteLine(e.ToString());
                    } 


                networkStream.Close();
                client.Close(); 

            listener.Stop();
        }  
    }    
}
