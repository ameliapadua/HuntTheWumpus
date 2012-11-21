using System;

namespace HuntTheWumpus
{
	class Player
	{
		public int bullets;
		public int grenades;
		public int flashbangs;
		public int playerRoomNumber;
		public bool alive;

		public Player()
		{
			this.alive = true;
			this.bullets = 3;
			this.flashbangs = 2;
			this.grenades = 1;
		}

		public int GetPlayerRoomNumber()
		{
			return playerRoomNumber;
		}

		public int GetPlayerBullets()
		{
			return bullets;
		}

		public int GetPlayerFlashbangs()
		{
			return flashbangs;
		}

		public int GetPlayerGrenades()
		{
			return grenades;
		}

		public bool IsPlayerAlive()
		{
			return alive;
		}

		public void ShootBullets()
		{
			if (this.bullets == 0)
			{
				Console.WriteLine("You have no more bullets!");
			}
			else
			{
				this.bullets--;
			}
		}

		public void ThrowFlashbangs()
		{
			if (this.flashbangs == 0)
			{
				Console.WriteLine("You have no more flashbangs!");
			}
			else
			{
				this.flashbangs--;
			}
		}

		public void ThrowGrenades()
		{
			if (this.grenades == 0)
			{
				Console.WriteLine("You have no more grenades!");
			}
			else
			{
				this.grenades--;
			}
		}

	}
}