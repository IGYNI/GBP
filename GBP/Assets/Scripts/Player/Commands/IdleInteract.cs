using UnityEngine;

namespace Player.Commands
{
	public class IdleInteract : IPlayerAction
	{
		public PlayerController.PlayerState State => PlayerController.PlayerState.IdleInteract;
		
		
		private float _timer;
		private bool _completed;
		private bool _started;
		
		private readonly VariableSystem _variableSystem;
		private readonly ItemInteraction _item;

		public IdleInteract(VariableSystem variableSystem, ItemInteraction item)
		{
			_variableSystem = variableSystem;
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
				_started = true;
				_item.Interact(_variableSystem);
			}
			_timer += Time.deltaTime;
			if (_timer > _item.InteractionTime)
			{
				_completed = true;
			}
		}
	}
}