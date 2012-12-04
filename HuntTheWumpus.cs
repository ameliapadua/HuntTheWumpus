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
			
			//set adjacentRoom array (im unsure about the syntax of setting the new array to the returned value)
			int[] adjacentRooms = new int[getAdjacentCaveArray(playerRoomNumber)];
			
			
			//Player is prompted when a terrorist or hostage is close
			ProximityPrompt(adjacentRooms, terroristRoomNumber, hostageRoomNumber);

			//Player is prompted for a next action.
			ChooseNextAction(player1, roomMap);

		}

		public void getAdjacentCaveArray(int caveNumber)
		{
			int[] adjacentCave = new int[4];
			
			//I have to do these individually since i cant think of a function which could identify all of the adjacent points.
			if(caveNumber = 1)
			{adjacentCave = {1, 2, 8, 5};}
			if(caveNumber = 2)
			{adjacentCave = {2, 3, 1, 10};}
			if(caveNumber = 3)
			{adjacentCave = [4] {3, 4, 2, 12};}
			if(caveNumber = 4)
			{ adjacentCave = {4, 3, 14, 5};}
			if(caveNumber = 5)
			{adjacentCave = {5, 1, 6, 4};}
			if(caveNumber = 6)
			{adjacentCave = {6, 7, 15, 6};}
			if(caveNumber = 7)
			{adjacentCave = {7, 17, 8, 6};}
			if(caveNumber = 8)
			{adjacentCave = {8, 1, 9, 7};}
			if(caveNumber = 9)
			{adjacentCave = {9, 10, 8, 18};}
			if(caveNumber = 10)
			{adjacentCave = {10, 2, 11, 9};}

			if(caveNumber = 11)
			{adjacentCave = {11, 10, 12, 19};}
			if(caveNumber = 12)
			{adjacentCave = {12, 13, 3, 11};}
			if(caveNumber = 13)
			{adjacentCave = {13, 14, 12, 20};}
			if(caveNumber = 14)
			{adjacentCave = {14, 4, 15, 13};}
			if(caveNumber = 15)
			{adjacentCave = {15, 16, 14, 6};}
			if(caveNumber = 16)
			{adjacentCave = {16, 17, 15, 20};}
			if(caveNumber = 17)
			{adjacentCave = {17, 18, 16, 7};}
			if(caveNumber = 18)
			{adjacentCave = {18, 19, 17, 9};}
			if(caveNumber = 19)
			{adjacentCave = {19, 20, 18, 11};}
			if(caveNumber = 20)
			{adjacentCave = {20, 19, 13, 16};}

			return adjacentCave;
			
			

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
	
		public static void ProximityPrompt(int[] adjacentRooms, int terroristRoomNumber, int hostageRoomNumber)
			foreach(int element in adjacentRooms)
			{
	
				if(terroristRoomNumber = adjacentRooms[element])//dont know if this is proper syntax
					{
					Console.WriteLine("Danger Close");
					}
			}
	
			foreach(int element in adjacentRooms)
			{
				if(hostageRoomNumber = adjacentrooms[element])
					{
					Console.WriteLine("I hear a hostage!!!");
					}
		}

	}
}