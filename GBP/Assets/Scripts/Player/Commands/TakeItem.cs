using UnityEngine;

namespace Player.Commands
{
	public class TakeItem : IPlayerAction
	{
		public PlayerController.PlayerState State => PlayerController.PlayerState.TakeItem;
		private readonly Item _item;
		private readonly VariableSystem _variableSystem;
		private readonly PlayerController _player;

		private float _timer;
		private bool _completed;
		private bool _started;

		public TakeItem(VariableSystem variableSystem, PlayerController player, Item item)
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
				Debug.Log("[Player] PlayTakeAnimation");
				//_player.PlayTakeAnimation();
				_started = true;
			}
			_timer += Time.deltaTime;
			if (_timer > 1f)
			{
				Debug.Log("[Player] StopTakeAnimation");
				//_player.StopTakeAnimation();
				_variableSystem.SetVariable(_item.itemName, "1");
				Object.Destroy(_item.gameObject);
				_completed = true;
			}
		}
	}
}