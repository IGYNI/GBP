using UnityEngine;

namespace Player.Commands
{
	public class MoveToPoint : IPlayerAction
	{
		public PlayerController.PlayerState State => PlayerController.PlayerState.Run;
		
		private readonly Vector3 _target;
		private readonly PlayerController _player;

		public MoveToPoint(PlayerController player, Vector3 target)
		{
			_player = player;
			_target = target;
			player.SetDestination(target);
		}
		
		public bool Completed()
		{
			var vec = _player.transform.position - _target;
			vec.y = 0;
			return vec.magnitude <= 0.1f;
		}

		public void Update()
		{
			//wait until navmesh reach the point
		}
	}
}