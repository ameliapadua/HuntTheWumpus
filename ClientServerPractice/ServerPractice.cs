using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace HuntTheWumpus
{
    public class TcpTimeServer 
    {
        private const int portNum = 3000;
        private string ipString = "10.36.12.40";

        
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
                    /*
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
                    */
                    

                    PlayHuntTheWumpus(reader, writer);

                } catch (Exception exception) 
                {
                    Console.WriteLine(exception.ToString());
                } 
            }
                
            networkStream.Close();
            client.Close(); 

            listener.Stop();
        }

        public static void PlayHuntTheWumpus(StreamReader reader, StreamWriter writer)
        {
            //Creating a player and a wumpus.
            Player player1 = new Player();
            Wumpus wumpus = new Wumpus();

            //Player initially starts in cave 1.
            EnterCave(player1, 1, writer);

            //Player is prompted for a next action.
            ChooseNextAction(player1, reader, writer);

        }

        //When a player enters a cave, which cave to move to is needed. The player is moved to that cave
        //and the caves the player can move to are printed. Also the available arrows are printed.
        public static void EnterCave(Player player, int caveNumber, StreamWriter writer)
        {
            player.playerCaveNumber = caveNumber;
            writer.WriteLine("You have now entered cave {0}.", player.GetPlayerCaveNumber());

            //Until we create a map, the adjacent caves are just the next numbers for simplicity.
            int adjacentCave1 = player.GetPlayerCaveNumber() + 1;
            int adjacentCave2 = player.GetPlayerCaveNumber() + 2;

            //Since the adjacent caves are just the next in sequence, these if statements make
            //sure the adjacent caves are within the 5 caves.
            if (adjacentCave1 > 5)
            {
                adjacentCave1 -= 5;
            }
            if (adjacentCave2 > 5)
            {
                adjacentCave2 -= 5;
            }

            //Printing where the player can move to next and how many arrows they have.
            writer.WriteLine("Tunnels lead to caves {0} and {1}", adjacentCave1, adjacentCave2);
            writer.WriteLine("You have {0} arrows.", player.GetPlayerArrows());
        }

        public static void ChooseNextAction(Player player1, StreamReader reader, StreamWriter writer)
        {
            //While the player has not been eaten by the wumpus, the player is prompted
            //for another action.
            while (player1.IsPlayerAlive() == true)
            {
                //Ask the player what they want to do. 
                writer.WriteLine("Shoot or Move (S/M): ");
                string input = reader.ReadLine();
                string playerChoice = input.Trim().ToUpper();

                //If player wants to shoot an arrow, they lose an arrow.
                if (playerChoice == "S")
                {
                    player1.ShootArrow();
                    writer.WriteLine("You have {0} arrows.", player1.GetPlayerArrows());
                }
                //If player wants to move, they are prompted for which room and are moved there.
                else if (playerChoice == "M")
                {
                    writer.WriteLine("Enter which cave would you like to move to:");
                    int caveNumber = int.Parse(reader.ReadLine());
                    EnterCave(player1, caveNumber, writer);
                }
                //If player does not choose shoot or move, there is no action and they 
                //will be prompted again.
                else
                {
                    writer.WriteLine("Invalid entry");
                }
            }
            
        }  
    }    
}
