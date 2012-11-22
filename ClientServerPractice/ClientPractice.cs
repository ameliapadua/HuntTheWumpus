using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HuntTheWumpus.ClientServerPractice
{
	public class TcpTimeClient 
	{
    	private const int portNum = 3000;
    	private string ipString = "127.0.0.1";

    	public static void Main(String[] args) 
    	{
        	TcpTimeClient tcpTimeClient = new TcpTimeClient();
        	tcpTimeClient.ClientConnect();
    	}

    	public void ClientConnect()
    	{

    		try 
    		{
            	TcpClient client = new TcpClient(ipString, portNum);

            	NetworkStream ns = client.GetStream();
            
            	byte[] bytes = new byte[1024];
            	int bytesRead = ns.Read(bytes, 0, bytes.Length);

            	Console.WriteLine(Encoding.ASCII.GetString(bytes,0,bytesRead));

            	client.Close();

        	} catch (Exception e) 
        	{
            	Console.WriteLine(e.ToString());
        	}
    	}
	}
}
