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

			//Manually creating 5 caves for now.
			Cave cave1 = new Cave(1);
			Cave cave2 = new Cave(2);
			Cave cave3 = new Cave(3);
			Cave cave4 = new Cave(4);
			Cave cave5 = new Cave(5);

			//Player initially starts in cave 1.
			EnterCave(player1, 1);

			//Player is prompted for a next action.
			ChooseNextAction(player1);

		}

		//When a player enters a cave, which cave to move to is needed. The player is moved to that cave
		//and the caves the player can move to are printed. Also the available arrows are printed.
		public static void EnterCave(Player player, int caveNumber)
		{
			player.playerCaveNumber = caveNumber;
			Console.WriteLine("You have now entered cave {0}.", player.GetPlayerCaveNumber());

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
			Console.WriteLine("Tunnels lead to caves {0} and {1}", adjacentCave1, adjacentCave2);
			Console.WriteLine("You have {0} arrows.", player.GetPlayerArrows());
		}

		public static void ChooseNextAction(Player player1)
		{
			//While the player has not been eaten by the wumpus, the player is prompted
			//for another action.
			while (player1.IsPlayerAlive() == true)
			{
				//Ask the player what they want to do. 
				Console.WriteLine("Shoot or Move (S/M): ");
				string input = Console.ReadLine();
				string playerChoice = input.Trim().ToUpper();

				//If player wants to shoot an arrow, they lose an arrow.
				if (playerChoice == "S")
				{
					player1.ShootArrow();
					Console.WriteLine("You have {0} arrows.", player1.GetPlayerArrows());
				}
				//If player wants to move, they are prompted for which room and are moved there.
				else if (playerChoice == "M")
				{
					Console.WriteLine("Which cave would you like to move to?");
					int caveNumber = int.Parse(Console.ReadLine());
					EnterCave(player1, caveNumber);
				}
				//If player does not choose shoot or move, there is no action and they 
				//will be prompted again.
				else
				{
					Console.WriteLine("Invalid entry");
				}
			}
			
		}

	}
}