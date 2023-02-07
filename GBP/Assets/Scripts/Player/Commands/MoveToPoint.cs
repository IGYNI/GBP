using UnityEngine;

namespace Player.Commands
{
	public class MoveToPoint : IPlayerAction
	{
		public PlayerController.PlayerState State => PlayerController.PlayerState.Run;
		public bool Failed { get; private set; }
		
		private Vector3 _target;
		private readonly PlayerController _player;
		private float _stuckTime;
		private Vector3 _prevPosition;

		public MoveToPoint(PlayerController player, Vector3 target)
		{
			_player = player;
			_target = target;
			Failed = false;
			player.SetDestination(target);
		}
		
		public bool Completed()
		{
			var vec = _player.transform.position - _target;
			vec.y = 0;
			return vec.magnitude <= 0.1f;
		}

		public bool Update()
		{
			var sqrDist = (_player.transform.position - _prevPosition).sqrMagnitude;
			if (sqrDist <= 0)
			{
				_stuckTime += Time.deltaTime;
			}
			else
			{
				_stuckTime = 0f;
			}

			if (_stuckTime > 0.5f)
			{
				_player.SetDestination(_player.transform.position);
				_target = _player.transform.position;
				Failed = true;
				return false;
			}

			_prevPosition = _player.transform.position;
			return true;
			//wait until navmesh reach the point
		}

		public override string ToString()
		{
			var distance = (_player.transform.position - _target).magnitude;
			return $"Move To Point. Dist: {distance:f}";
		}
	}
}