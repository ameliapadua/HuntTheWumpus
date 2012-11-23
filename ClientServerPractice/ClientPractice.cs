using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

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

            	NetworkStream networkStream = client.GetStream();
            	StreamReader reader = new StreamReader(networkStream);
            	StreamWriter writer = new StreamWriter(networkStream) { AutoFlush = true };
            

				Console.WriteLine(reader.ReadLine());

				Console.WriteLine("Enter y/n: ");
				string input = Console.ReadLine();
				writer.WriteLine(input);
				Console.WriteLine("Msg from server: {0}", reader.ReadLine());
				Console.WriteLine(reader.ReadLine());

				while (serverReader.Peek() > -1) 
				{
    serverReader.ReadLine();
				}


            	client.Close();

        	} catch (Exception e) 
        	{
            	Console.WriteLine(e.ToString());
        	}
    	}
	}
}
