using System;

namespace HuntTheWumpus
{
	class Wumpus
	{
		public bool alive;
		public int wumpusCaveNumber;

		public Wumpus()
		{
			this.alive = true;
			this.wumpusCaveNumber = 5;
		}

		public int GetWumpusCaveNumber()
		{
			return wumpusCaveNumber;
		}

		public bool IsWumpusAlive()
		{
			return alive;
		}

		//If the wumpus is shot, it dies.
		public void ShotWithArrow()
		{
			this.alive = false;
		}
	}
}