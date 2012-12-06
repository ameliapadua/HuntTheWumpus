using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace HuntTheWumpus
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
    			string clientInput, gameResponse;
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
                        gameResponse = reader.ReadLine();
                        Console.WriteLine(gameResponse);                                                   

                        if (gameResponse.Contains(":") == true)
                        {
                            clientInput = Console.ReadLine();
                            writer.WriteLine(clientInput);
                        }
                        if (gameResponse == "Q")
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
