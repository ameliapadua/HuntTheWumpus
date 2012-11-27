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

			int[] adjacentRooms = {2, 8, 5};
			roomMap.Add(1, adjacentRooms);

			adjacentRooms[0] = 3;
			adjacentRooms[1] = 1;
			adjacentRooms[2] = 10;
			roomMap.Add(2, adjacentRooms);

			adjacentRooms[0] = 4;
			adjacentRooms[1] = 2;
			adjacentRooms[2] = 12;
			roomMap.Add(3, adjacentRooms);

			adjacentRooms[0] = 3;
			adjacentRooms[1] = 14;
			adjacentRooms[2] = 5;
			roomMap.Add(4, adjacentRooms);

			adjacentRooms[0] = 1;
			adjacentRooms[1] = 6;
			adjacentRooms[2] = 4;
			roomMap.Add(5, adjacentRooms);

			adjacentRooms[0] = 7;
			adjacentRooms[1] = 15;
			adjacentRooms[2] = 6;
			roomMap.Add(6, adjacentRooms);

			adjacentRooms[0] = 17;
			adjacentRooms[1] = 8;
			adjacentRooms[2] = 6;
			roomMap.Add(7, adjacentRooms);

			adjacentRooms[0] = 1;
			adjacentRooms[1] = 9;
			adjacentRooms[2] = 7;
			roomMap.Add(8, adjacentRooms);

			adjacentRooms[0] = 10;
			adjacentRooms[1] = 8;
			adjacentRooms[2] = 18;
			roomMap.Add(9, adjacentRooms);

			adjacentRooms[0] = 2;
			adjacentRooms[1] = 11;
			adjacentRooms[2] = 9;
			roomMap.Add(10, adjacentRooms);

			adjacentRooms[0] = 10;
			adjacentRooms[1] = 12;
			adjacentRooms[2] = 19;
			roomMap.Add(11, adjacentRooms);

			adjacentRooms[0] = 13;
			adjacentRooms[1] = 3;
			adjacentRooms[2] = 11;
			roomMap.Add(12, adjacentRooms);

			adjacentRooms[0] = 14;
			adjacentRooms[1] = 12;
			adjacentRooms[2] = 20;
			roomMap.Add(13, adjacentRooms);

			adjacentRooms[0] = 4;
			adjacentRooms[1] = 15;
			adjacentRooms[2] = 13;
			roomMap.Add(14, adjacentRooms);

			adjacentRooms[0] = 16;
			adjacentRooms[1] = 14;
			adjacentRooms[2] = 6;
			roomMap.Add(15, adjacentRooms);

			adjacentRooms[0] = 17;
			adjacentRooms[1] = 15;
			adjacentRooms[2] = 20;
			roomMap.Add(16, adjacentRooms);

			adjacentRooms[0] = 18;
			adjacentRooms[1] = 16;
			adjacentRooms[2] = 7;
			roomMap.Add(17, adjacentRooms);

			adjacentRooms[0] = 19;
			adjacentRooms[1] = 17;
			adjacentRooms[2] = 9;
			roomMap.Add(18, adjacentRooms);

			adjacentRooms[0] = 20;
			adjacentRooms[1] = 18;
			adjacentRooms[2] = 11;
			roomMap.Add(19, adjacentRooms);

			adjacentRooms[0] = 19;
			adjacentRooms[1] = 13;
			adjacentRooms[2] = 16;
			roomMap.Add(20, adjacentRooms);

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
					Console.WriteLine("You have {0} bullets.", player1.GetPlayerBullets());
				}
				//If player wants to move, they are prompted for which room and are moved there.
				else if (playerChoice == "M")
				{
					Console.WriteLine("Which cave would you like to move to?");
					int roomNumber = int.Parse(Console.ReadLine());
					EnterRoom(player1, roomNumber, roomMap);
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