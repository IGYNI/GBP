using UnityEngine;

namespace Player.Commands
{
	public class Interact : IPlayerAction
	{
		public PlayerController.PlayerState State => PlayerController.PlayerState.Interact;
		public bool Failed => false;
		
		private float _timer;
		private bool _completed;
		private bool _started;
		
		private readonly VariableSystem _variableSystem;
		private readonly ItemInteraction _item;

		public Interact(VariableSystem variableSystem,  ItemInteraction item)
		{
			_variableSystem = variableSystem;
			_item = item;
		}
		
		public bool Completed()
		{
			return _completed;
		}

		public bool Update()
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

			return true;
		}
	}
}