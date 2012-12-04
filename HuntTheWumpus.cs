using System;
using System.Collections.Generic;

namespace HuntTheWumpus
{
	class HuntTheWumpus
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Time to hunt the Wumpus!");

			PlayHuntTheWumpus();
		}

		public static void PlayHuntTheWumpus()
		{
			//Creating a player and a wumpus.
			Player player1 = new Player();
			Wumpus wumpus = new Wumpus();

			//Creating a random cave to start at
			//Player starts from any cave from 1 - 5 (outermost caves)
			Random r = new Random();
			int playerRoomNumber = r.Next(1,6);	

			Dictionary<int, int[]> roomMap = CreateRoomMap();

			//Player initially starts in cave 1.
			EnterRoom(player1, playerRoomNumber, roomMap);

			//Player is prompted for a next action.
			ChooseNextAction(player1, roomMap);

		}

		public static Dictionary<int, int[]> CreateRoomMap()
		{
			Dictionary<int, int[]> roomMap = new Dictionary<int, int[]>();

			int[] Room1 = {2, 5, 3};
			roomMap.Add(1, Room1);

			int[] Room2 = {1, 10, 3};
			roomMap.Add(2, Room2);

			int[] Room3 = {2, 4, 12};
			roomMap.Add(3, Room3);

			int[] Room4 = {3, 5, 14};
			roomMap.Add(4, Room4);

			int[] Room5 = {1, 10, 3};
			roomMap.Add(5, Room5);

			int[] Room6 = {5, 7, 15};
			roomMap.Add(6, Room6);

			int[] Room7 = {6, 8, 17};
			roomMap.Add(7, Room7);

			int[] Room8 = {1, 7, 9};
			roomMap.Add(8, Room8);

			int[] Room9 = {8, 10, 18};
			roomMap.Add(9, Room9);

			int[] Room10 = {2, 9, 11};
			roomMap.Add(10, Room10);

			int[] Room12 = {3, 11, 13};
			roomMap.Add(12, Room12);

			int[] Room13 = {12, 14, 20};
			roomMap.Add(13, Room13);

			int[] Room14 = {4, 13, 15};
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

		//When a player enters a cave, which cave to move to is needed. The player is moved to that cave
		//and the caves the player can move to are printed. Also the available arrows are printed.
		public static void EnterRoom(Player player, int roomNumber, Dictionary<int, int[]> roomMap)
		{
			player.playerRoomNumber = roomNumber;
			Console.WriteLine("You have now entered Room {0}.", player.GetPlayerRoomNumber());

			int[] adjacentRooms = roomMap[roomNumber];

			//Printing where the player can move to next and how many arrows they have.
			Console.WriteLine("Tunnels lead to rooms {0}, {1}, and {2}", adjacentRooms[0], adjacentRooms[1], adjacentRooms[2]);
			Console.WriteLine("You have {0} bullets, {1} flashbangs, and {2} grenades.", player.GetPlayerBullets(), player.GetPlayerFlashbangs(), player.GetPlayerGrenades());
		}

		public static void ChooseNextAction(Player player1, Dictionary<int, int[]> roomMap)
		{
			//While the player has not been eaten by the wumpus, the player is prompted
			//for another action.
			while (player1.IsPlayerAlive() == true)
			{
				//Ask the player what they want to do. 
				Console.WriteLine("Shoot (S), throw a grenade (G), throw a flashbang (F), or move (M): ");
				string input = Console.ReadLine();
				string playerChoice = input.Trim().ToUpper();

				//If player wants to shoot an arrow, they lose an arrow.
				if (playerChoice == "S")
				{
					player1.ShootBullets();
					Console.WriteLine("\nYou have {0} bullets.", player1.GetPlayerBullets());
				}
				//If player wants to move, they are prompted for which room and are moved there.
				else if (playerChoice == "M")
				{
					bool roomExists = false;
					int[] adjacentRooms = roomMap[player1.playerRoomNumber];
					while (!roomExists)
					{
						Console.WriteLine("Which room would you like to move to?");
						int roomNumber = int.Parse(Console.ReadLine());

						for (int i=0; i<3; i++)
						{
							if (roomNumber == adjacentRooms[i])
							{
								EnterRoom(player1, roomNumber, roomMap);
								roomExists = true;
							}
						}
					}

				}
				//If player does not choose shoot or move, there is no action and they 
				//will be prompted again.
				else if (playerChoice == "G")
				{
					player1.ThrowGrenades();
					Console.WriteLine("You have {0} grenades.", player1.GetPlayerGrenades());
				}
				else if (playerChoice == "F")
				{
					player1.ThrowFlashbangs();
					Console.WriteLine("You have {0} flashbangs.", player1.GetPlayerFlashbangs());
				}
				else
				{
					Console.WriteLine("Invalid entry");
				}
			}

		}

	}
}