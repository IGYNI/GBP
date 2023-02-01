using UnityEngine;

namespace Player.Commands
{
	public class RotateToTarget : IPlayerAction
	{
		public PlayerController.PlayerState State => PlayerController.PlayerState.Idle;
		private readonly PlayerController _player;
		private readonly Vector3 _target;

		public RotateToTarget(PlayerController player, Vector3 target)
		{
			_target = target;
			_target.y = 0;
			_player = player;
		}
		
		public bool Completed()
		{
			return Vector3.Dot(_player.transform.forward, _target) > 0.99f;
		}

		public void Update()
		{
			_player.transform.forward = Vector3.Slerp(_player.transform.forward, _target, Time.deltaTime * 8);
		}
	}
}