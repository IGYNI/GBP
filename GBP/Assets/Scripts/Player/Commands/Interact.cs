using Interaction;
using UnityEngine;

namespace Player.Commands
{
	public class Interact : IPlayerAction
	{
		public PlayerController.PlayerState State => PlayerController.PlayerState.Interact;
		
		private readonly PlayerController _player;
		private readonly IInteractable _interactable;

		private float _timer;
		private bool _completed;
		private bool _started;

		public Interact(PlayerController player, IInteractable interactable)
		{
			_player = player;
			_interactable = interactable;
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
				_interactable.Interact();
				Debug.Log("[Player] StopInteractAnimation");
				//_player.StopInteractAnimation();
				_completed = true;
			}
		}
	}
}