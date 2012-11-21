using System;

namespace HuntTheWumpus
{
	class Wumpus
	{
		public bool alive;
		public int wumpusRoomNumber;

		public Wumpus()
		{
			this.alive = true;
			this.wumpusRoomNumber = 5;
		}

		public int GetWumpusRoomNumber()
		{
			return wumpusRoomNumber;
		}

		public bool IsWumpusAlive()
		{
			return alive;
		}

		//If the wumpus is shot, it dies.
		public void ShotWithBullets()
		{
			this.alive = false;
		}
	}
}