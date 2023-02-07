namespace Player.Commands
{
	public interface IPlayerAction
	{
		PlayerController.PlayerState State { get; }
		bool Failed { get; }
		bool Completed();
		bool Update();
	}
}