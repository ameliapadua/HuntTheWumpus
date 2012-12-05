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
    	private string ipString = "10.36.12.40";

    	public static void Main(String[] args) 
    	{
        	TcpTimeClient tcpTimeClient = new TcpTimeClient();
        	tcpTimeClient.ClientConnect();
    	}

    	public void ClientConnect()
    	{
    		try 
    		{
    			bool done = false;
    			string clientInput;
                string goodbyeMsg = "This would end play. Thank you, goodbye.";
    			string serverReadLine;

    			TcpClient client = new TcpClient(ipString, portNum);

	            NetworkStream networkStream = client.GetStream();
	            StreamReader reader = new StreamReader(networkStream);
	            StreamWriter writer = new StreamWriter(networkStream) { AutoFlush = true };

    			while (!done)
    			{
                    try
                    {
                        Console.WriteLine("Enter y/n: ");
                        clientInput = Console.ReadLine();           
                        writer.WriteLine(clientInput);

                        serverReadLine = reader.ReadLine();
                        Console.WriteLine("Msg from server: {0}", serverReadLine);
                        
                        
                        if (serverReadLine == goodbyeMsg)
                        {
                            done = true;
                        } 
                    } catch (Exception exception)
                    {
                        Console.WriteLine(exception.ToString());
                    }
    				
    			}
   
            	client.Close();

        	} catch (Exception e) 
        	{
            	Console.WriteLine(e.ToString());
        	}
    	}
	}
}
