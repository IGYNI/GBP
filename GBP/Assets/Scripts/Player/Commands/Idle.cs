namespace Player.Commands
{
	public class Idle : IPlayerAction
	{
		public PlayerController.PlayerState State => PlayerController.PlayerState.Idle;

		public bool Completed()
		{
			return false;
		}

		public void Update()
		{
			//TODO
		}
	}
}