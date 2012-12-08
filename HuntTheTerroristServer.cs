using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace HuntTheTerrorist
{
    public class HuntTheTerroristServer 
    {
        private const int portNum = 3000;
        //private string ipString = "10.36.12.40";
        private string ipString = "127.0.0.1";

        
        public static void Main(String[] args) 
        {
            HuntTheTerroristServer huntTheTerroristServer = new HuntTheTerroristServer();
            huntTheTerroristServer.ServerConnect();
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

            writer.WriteLine("Rescue the hostage!");

            bool done = false;          

            while (!done)
            {
                try 
                {
                    done = PlayHuntTheTerrorist(reader, writer);
                } catch (Exception exception) 
                {
                    Console.WriteLine(exception.ToString());
                } 
            }
                
            networkStream.Close();
            client.Close(); 

            listener.Stop();
        }

        public static bool PlayHuntTheTerrorist(StreamReader reader, StreamWriter writer)
        {
            bool done = false;
            //Creating the network of rooms.
            Dictionary<int, int[]> roomMap = CreateRoomMap();

            //Creating a player, set of terrorists, and a hostage.
            Terrorist[] terrorists = DropTerroristsInRooms();       
            Hostage hostage = DropHostageInRoom(terrorists);
            Player player1 = DropPlayerinRoom(roomMap, writer);
            CheckAdjacentRooms(player1, terrorists, hostage, roomMap, writer);          
            
            //Player is prompted for a next action.
            done = ChooseAction(player1, terrorists, hostage, roomMap, writer, reader);
            return done;
        }

        public static Dictionary<int, int[]> CreateRoomMap()
        {
            Dictionary<int, int[]> roomMap = new Dictionary<int, int[]>();

            int[] Room1 = {2, 5, 3};
            roomMap.Add(1, Room1);

            int[] Room2 = {1, 8, 3};
            roomMap.Add(2, Room2);

            int[] Room3 = {2, 4, 10};
            roomMap.Add(3, Room3);

            int[] Room4 = {3, 5, 12};
            roomMap.Add(4, Room4);

            int[] Room5 = {1, 14, 4};
            roomMap.Add(5, Room5);

            int[] Room6 = {1, 7, 15};
            roomMap.Add(6, Room6);

            int[] Room7 = {6, 8, 17};
            roomMap.Add(7, Room7);

            int[] Room8 = {2, 7, 9};
            roomMap.Add(8, Room8);

            int[] Room9 = {8, 10, 18};
            roomMap.Add(9, Room9);

            int[] Room10 = {3, 9, 11};
            roomMap.Add(10, Room10);

            int[] Room11 = {10, 12, 19};
            roomMap.Add(11, Room11);

            int[] Room12 = {4, 11, 13};
            roomMap.Add(12, Room12);

            int[] Room13 = {12, 14, 20};
            roomMap.Add(13, Room13);

            int[] Room14 = {5, 13, 15};
            roomMap.Add(14, Room14);

            int[] Room15 = {6, 14, 16};
            roomMap.Add(15, Room15);

            int[] Room16 = {15, 17, 20};
            roomMap.Add(16, Room16);

            int[] Room17 = {7, 16, 18};
            roomMap.Add(17, Room17);

            int[] Room18 = {9, 17, 19};
            roomMap.Add(18, Room18);

            int[] Room19 = {11, 18, 20};
            roomMap.Add(19, Room19);

            int[] Room20 = {13, 16, 19};
            roomMap.Add(20, Room20);

            return roomMap;
        }

        //This puts the player in one of the outer rooms. 
        public static Player DropPlayerinRoom(Dictionary<int, int[]> roomMap, StreamWriter writer)
        {
            Player player = new Player();

            //Player starts from any room from 1 - 5 (outermost rooms)
            Random p = new Random();
            player.playerRoomNumber = p.Next(1,6);

            writer.WriteLine("You have been dropped into Room {0}.", player.playerRoomNumber);
            AnnounceAdjacentRooms(player.playerRoomNumber, roomMap, writer);


            return player;
        }

        //This creates a set of terrorists and defines which rooms they are in.
        public static Terrorist[] DropTerroristsInRooms()
        {
            Terrorist terrorist1 = new Terrorist();
            Terrorist terrorist2 = new Terrorist();
            Terrorist terrorist3 = new Terrorist();
            Terrorist terrorist4 = new Terrorist();
            Terrorist terrorist5 = new Terrorist();

            Random terroristRoomNumGen = new Random();
            terrorist1.terroristRoomNumber = terroristRoomNumGen.Next(6,9);
            terrorist2.terroristRoomNumber = terroristRoomNumGen.Next(9,12);
            terrorist3.terroristRoomNumber = terroristRoomNumGen.Next(12,15);
            terrorist4.terroristRoomNumber = terroristRoomNumGen.Next(15,18);
            terrorist5.terroristRoomNumber = terroristRoomNumGen.Next(18,21);

            Terrorist[] terrorists = {terrorist1, terrorist2, terrorist3, terrorist4, terrorist5};
            return terrorists;
        }

        public static Hostage DropHostageInRoom(Terrorist[] terrorists)
        {
            Hostage hostage = new Hostage();

            Random hostageRoomOptionsGen = new Random();
            hostage.hostageRoomNumber = terrorists[hostageRoomOptionsGen.Next(1,5)].terroristRoomNumber;

            return hostage;
        }       

        public static void AnnounceAdjacentRooms(int roomNumber, Dictionary<int, int[]> roomMap, StreamWriter writer)
        {
            int[] adjacentRooms = roomMap[roomNumber];

            //Printing where the player can move to next and how many arrows they have.
            writer.WriteLine("Tunnels lead to rooms {0}, {1}, and {2}.\n", adjacentRooms[0], adjacentRooms[1], adjacentRooms[2]);
        }

        public static bool ChooseAction(Player player1, Terrorist[] terrorists, Hostage hostage, Dictionary<int, int[]> roomMap, StreamWriter writer, StreamReader reader)
        {
            bool done = false;
            //While the player has not been killed by the terrorist, 
            //the player is prompted for another action.
            while (player1.IsPlayerAlive() == true && hostage.IsHostageAlive() == true)
            {
                //Get adjacent rooms.
                //int[] adjacentRooms = roomMap[player1.playerRoomNumber];

                //Ask the player what they want to do. 
                writer.WriteLine("Shoot (S), throw a grenade (G), throw a flashbang (F), move (M), or quit (Q): ");
                string input = reader.ReadLine();
                string playerChoice = input.Trim().ToUpper();

                //If player wants to shoot, they lose a bullet.
                if (playerChoice == "S")
                {
                    Shoot(player1, terrorists, writer);
                }
                else if (playerChoice == "G")
                {
                    hostage.alive = ThrowGrenade(player1, terrorists, hostage, roomMap, writer, reader);
                }
                else if (playerChoice == "F")
                {
                    ThrowFlashbang(player1, terrorists, roomMap, writer, reader);                  
                }  
                //If player wants to move, they are prompted for which room and are moved there.
                else if (playerChoice == "M")
                {
                    player1.alive = Move(player1, terrorists, hostage, roomMap, writer, reader);
                }
                else if (playerChoice == "Q")
                {
                    player1.alive = false;
                }
                else
                {
                    writer.WriteLine("That's not an option.");
                }
            }

            writer.WriteLine("GAME OVER");
            done = true;
            
            return done;
        }

        public static void Shoot(Player player, Terrorist[] terrorists, StreamWriter writer)
        {
            //Player shoots.
            player.ShootBullets();
            bool isTerroristDead = false;

            //Check to see if any of the terrorists are in the room the player is in.
            for (int i=0; i<terrorists.Length; i++)
            {
                //If there is a terrorist in the room, the terrorist gets killed.
                if (terrorists[i].terroristRoomNumber == player.playerRoomNumber)
                {
                    terrorists[i].alive = false;
                    writer.WriteLine("\nTerrorist terminated.");
                    isTerroristDead = true;
                }
            }
            //Otherwise the player just loses a bullet.
            if (isTerroristDead == false)
            {
                writer.WriteLine("\nNo terrorists in here. Wasted bullet...");
            }

            //Lets the player know how many bullets they have left.
            writer.WriteLine("You have {0} bullets.", player.GetPlayerBullets());
        }

        public static bool ThrowGrenade(Player player, Terrorist[] terrorists, Hostage hostage, Dictionary<int, int[]> roomMap, StreamWriter writer, StreamReader reader)
        {
            bool roomExists = false;

            while (!roomExists)
            {
                //Player has to choose which room to throw a grenade into.
                writer.WriteLine("Choose which room you want to throw the grenade into: ");
                int grenadeIntoRoomNum = int.Parse(reader.ReadLine());

                roomExists = IsAdjacentRoom(player, grenadeIntoRoomNum, roomMap);

                if (roomExists == true)
                {
                    //Player throws grenade.
                    player.ThrowGrenades();

                    //If a hostage is in the room the player throws a grenade into, game is over.
                    if (hostage.hostageRoomNumber == grenadeIntoRoomNum)
                    {
                        writer.WriteLine("Oh no! You killed the hostage.");
                        hostage.alive = false;
                        break;
                    }

                    //If a terrorist is in the room the player throws a grenade into, it dies.
                    for (int i=0; i<terrorists.Length; i++)
                    {
                        if (terrorists[i].terroristRoomNumber == grenadeIntoRoomNum)
                        {
                            terrorists[i].alive = false;
                            writer.WriteLine("\nTerrorist terminated.");
                        }
                    }
                }
                else
                {
                    writer.WriteLine("That is not an adjacent room.");
                }
            }

            //Lets the player know how many grenades they have left after throwing one. 
            writer.WriteLine("You have {0} grenades.", player.GetPlayerGrenades());

            return hostage.alive;
        }

        public static void ThrowFlashbang(Player player, Terrorist[] terrorists, Dictionary<int, int[]> roomMap, StreamWriter writer, StreamReader reader)
        {
            bool roomExists = false;

            while (!roomExists)
            {
                //Player has to choose which room to throw a flashbang into.
                writer.WriteLine("Choose which room you want to throw the flashbang into: ");
                int flashbangIntoRoomNum = int.Parse(reader.ReadLine());

                roomExists = IsAdjacentRoom(player, flashbangIntoRoomNum, roomMap);

                bool isTerroristFlashbanged = false;

                if (roomExists == true)
                {
                    //Player throws flashbang.
                    player.ThrowFlashbangs();

                    for (int i=0; i<terrorists.Length; i++)
                    {
                        if (terrorists[i].terroristRoomNumber == flashbangIntoRoomNum)
                        {
                            terrorists[i].flashbanged = true;
                            isTerroristFlashbanged = true;
                            writer.WriteLine("You blinded a terrorist. Move now before they regain their sight.");
                        }
                    }

                    if (isTerroristFlashbanged == false)
                    {
                        writer.WriteLine("No one in that room.");
                    }
                }
                else
                {
                    writer.WriteLine("That is not an adjacent room.");
                } 
            }
            writer.WriteLine("You have {0} flashbangs.", player.GetPlayerFlashbangs());
        }

        public static bool Move(Player player, Terrorist[] terrorists, Hostage hostage, Dictionary<int, int[]> roomMap, StreamWriter writer, StreamReader reader)
        {
            bool roomExists = false;
            
            while (!roomExists)
            {
                writer.WriteLine("Choose room to move to: ");
                int roomNumber = int.Parse(reader.ReadLine());

                roomExists = IsAdjacentRoom(player, roomNumber, roomMap);

                if (roomExists == true)
                {
                    player.alive = EnterRoom(player, roomNumber, terrorists, hostage, roomMap, writer);
                    if (player.alive == true)
                    {
                        CheckAdjacentRooms(player, terrorists, hostage, roomMap, writer);
                    }
                }
            }

            return player.alive;
        }

        public static bool IsAdjacentRoom(Player player, int roomNumber, Dictionary<int, int[]> roomMap)
        {
            bool roomExists = false;
            int[] adjacentRooms = roomMap[player.playerRoomNumber];

            for (int i=0; i<adjacentRooms.Length; i++)
            {
                if (roomNumber == adjacentRooms[i])
                {
                    roomExists = true;
                }
            }

            return roomExists;
        }

        public static bool EnterRoom(Player player, int roomNumber, Terrorist[] terrorists, Hostage hostage, Dictionary<int, int[]> roomMap, StreamWriter writer)
        {
            player.playerRoomNumber = roomNumber;
            writer.WriteLine("--------------------------------------------------------------\nYou have now entered Room {0}.", player.GetPlayerRoomNumber());

            player.alive = CheckForTerrorists(player, terrorists, writer);

            if (player.alive == false)
            {
                return player.alive;
            }
            else
            {
                player.alive = CheckForHostage(player, hostage, writer);

                if (player.alive == false)
                {
                    return player.alive;
                }
                else
                {
                    AnnounceAdjacentRooms(player.playerRoomNumber, roomMap, writer);
                    writer.WriteLine("You have {0} bullets, {1} flashbangs, and {2} grenades.\n", player.GetPlayerBullets(), player.GetPlayerFlashbangs(), player.GetPlayerGrenades());
                }
            }

            return player.alive;
        }

        public static void CheckAdjacentRooms(Player player, Terrorist[] terrorists, Hostage hostage, Dictionary<int, int[]> roomMap, StreamWriter writer)
        {
            int[] adjacentRooms = roomMap[player.playerRoomNumber];

            for (int i=0; i<adjacentRooms.Length; i++)
            {
                for (int j=0; j<terrorists.Length; j++)
                {
                    if (adjacentRooms[i] == terrorists[j].terroristRoomNumber && terrorists[j].alive == true)
                    {
                        writer.WriteLine("Something doesn't feel right in here...");
                    }
                }

                if (adjacentRooms[i] == hostage.hostageRoomNumber)
                {
                    writer.WriteLine("I think I hear someone shouting in the next room.");
                }
            }
        }

        public static bool CheckForTerrorists(Player player, Terrorist[] terrorists, StreamWriter writer)
        {
            for (int i=0; i<terrorists.Length; i++)
            {
                if (terrorists[i].terroristRoomNumber == player.playerRoomNumber)
                {
                    if (terrorists[i].flashbanged == false)
                    {
                        writer.WriteLine("Killed by a terrorist.\nBetter luck next time!");
                        player.alive = false;
                    }                   
                }
            }

            return player.alive;
        }

        public static bool CheckForHostage(Player player, Hostage hostage, StreamWriter writer)
        {
            if (player.playerRoomNumber == hostage.hostageRoomNumber)
            {
                writer.WriteLine("You found the hostage!\nCongratulations, you win!!");
                player.alive = false;
            }

            return player.alive;
        }

    }
}
