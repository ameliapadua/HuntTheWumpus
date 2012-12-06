using System;

namespace HuntTheTerrorist
{
	class Terrorist
	{
		public bool alive;
		public bool flashbanged;
		public int terroristRoomNumber;

		public Terrorist()
		{
			this.alive = true;
			this.flashbanged = false;
		}

		public int GetTerroristRoomNumber()
		{
			return terroristRoomNumber;
		}

		public bool IsTerroristAlive()
		{
			return alive;
		}

		public bool IsTerroristFlashbanged()
		{
			return flashbanged;
		}

		//If the terrorist is shot, it dies.
		public void ShotWithBullets()
		{
			this.alive = false;
		}

		public void FlashBanged()
		{
			this.flashbanged = true;
		}

		public void KilledByGrenade()
		{
			this.alive = false;
		}
	}
}