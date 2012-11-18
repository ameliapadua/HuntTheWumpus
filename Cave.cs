using System;

namespace HuntTheWumpus
{
	class Cave
	{
		public bool containsWumpus;
		public bool containsPlayer;
		public string hint;
		public int caveNumber;

		//Cave constructor. 
		public Cave(int caveNumber)
		{
			this.containsPlayer = false;
			this.containsWumpus = false;
			this.caveNumber = caveNumber;
		}

		public int GetCaveNumber()
		{
			return caveNumber;
		}

		public bool IsWumpusInCave()
		{
			return containsWumpus;
		}

		public bool IsPlayerInCave()
		{
			return containsPlayer;
		}

		public void MoveIntoCave()
		{
			this.containsWumpus = true;
			this.containsPlayer = true;
		}

		//This will eventually be used once the map of
		//the caves is developed and we know which cave
		//contains which obstacles.
		public void Hint(string hint)
		{
			this.hint = hint;
		}

	}
}