using System;

namespace HuntTheTerrorist
{
  	public class Hostage
	{
		public bool alive;
		public int hostageRoomNumber;

		public Hostage()
		{
			this.alive = true;
		}

		public int GetHostageRoomNumber()
		{
			return hostageRoomNumber;
		}

		public bool IsHostageAlive()
		{
			return alive;
		}

		//If the hostage is "grenaded"
		public void KilledByGrenade()
		{
			this.alive = false;
		}
	}
}