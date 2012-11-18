using System;

namespace HuntTheWumpus
{
	class Player
	{
		public int arrows;
		public int playerCaveNumber;
		public bool alive;

		public Player()
		{
			this.alive = true;
			this.arrows = 3;
		}

		public int GetPlayerCaveNumber()
		{
			return playerCaveNumber;
		}

		public int GetPlayerArrows()
		{
			return arrows;
		}

		public bool IsPlayerAlive()
		{
			return alive;
		}

		public void ShootArrow()
		{
			if (this.arrows == 0)
			{
				Console.WriteLine("You have no more arrows!");
			}
			else
			{
				this.arrows--;
			}
		}

	}
}