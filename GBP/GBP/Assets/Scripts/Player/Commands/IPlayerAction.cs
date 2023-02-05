namespace Player.Commands
{
	public interface IPlayerAction
	{
		PlayerController.PlayerState State { get; }
		bool Completed();
		void Update();
	}
}