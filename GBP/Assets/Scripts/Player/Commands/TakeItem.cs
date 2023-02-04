using UnityEngine;

namespace Player.Commands
{
	public class TakeItem : IPlayerAction
	{
		public PlayerController.PlayerState State => PlayerController.PlayerState.TakeItem;
		private readonly ItemInteraction _item;
		private readonly VariableSystem _variableSystem;
		private readonly PlayerController _player;

		private float _timer;
		private bool _completed;
		private bool _started;

		public TakeItem(VariableSystem variableSystem, PlayerController player, ItemInteraction item)
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
				_started = true;
            }
            _timer += Time.deltaTime;
			if (_timer > 1f)
			{
				_item.Interact(_variableSystem);
				_completed = true;
			}
		}
	}
}