using UnityEngine;

namespace Player.Commands
{
	public class Interact : IPlayerAction
	{
		public PlayerController.PlayerState State => PlayerController.PlayerState.Interact;
		
		
		private float _timer;
		private bool _completed;
		private bool _started;
		
		private readonly PlayerController _player;
		private readonly VariableSystem _variableSystem;
		private readonly ItemInteraction _item;

		public Interact(VariableSystem variableSystem, PlayerController player, ItemInteraction item)
		{
			_variableSystem = variableSystem;
			_player = player;
			_item = item;
		}
		
		public bool Completed()
		{
			return _completed;
		}

		public void Update()
		{
			if (!_started)
			{
				Debug.Log("[Player] PlayInteractAnimation");
				//_player.PlayInteractAnimation();
				_started = true;
			}
			_timer += Time.deltaTime;
			if (_timer > 1f)
			{
				_item.Interact(_variableSystem);
				Debug.Log("[Player] StopInteractAnimation");
				//_player.StopInteractAnimation();
				_completed = true;
			}
		}
	}
}