using System;

namespace HuntTheTerrorist
{
	public class Room
	{
		public bool containsWumpus;
		public bool containsPlayer;
		public string hint;
		public int roomNumber;

		//Room constructor. 
		public Room(int roomNumber)
		{
			this.containsPlayer = false;
			this.containsWumpus = false;
			this.roomNumber = roomNumber;
		}

		public int GetRoomNumber()
		{
			return roomNumber;
		}

		public bool IsWumpusInRoom()
		{
			return containsWumpus;
		}

		public bool IsPlayerInRoom()
		{
			return containsPlayer;
		}

		public void MoveIntoRoom()
		{
			this.containsWumpus = true;
			this.containsPlayer = true;
		}

		//This will eventually be used once the map of
		//the Rooms is developed and we know which Room
		//contains which obstacles.
		public void Hint(string hint)
		{
			this.hint = hint;
		}

	}
}