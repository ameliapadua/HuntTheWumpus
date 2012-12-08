using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace HuntTheTerrorist
{
	public class HuntTheTerroristClient 
	{
    	private const int portNum = 3000;
    	//private string ipString = "10.36.12.40";
        private string ipString = "127.0.0.1";

    	public static void Main(String[] args) 
    	{
        	HuntTheTerroristClient huntTheTerroristClient = new HuntTheTerroristClient();
        	huntTheTerroristClient.ClientConnect();
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
                        if (gameResponse == "GAME OVER")
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
