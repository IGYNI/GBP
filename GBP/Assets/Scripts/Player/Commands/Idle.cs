namespace Player.Commands
{
	public class Idle : IPlayerAction
	{
		public PlayerController.PlayerState State => PlayerController.PlayerState.Idle;
		public bool Failed => false;

		public bool Completed()
		{
			return false;
		}

		

		public bool Update()
		{
			return true;
			//TODO
		}
	}
}