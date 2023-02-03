using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimation : MonoBehaviour
{
	private PlayerController _playerController;
	[SerializeField] private Animator animator;
	
	private static readonly int AnimationRun = Animator.StringToHash("Run");
	private static readonly int AnimationTake = Animator.StringToHash("Take");
	private static readonly int AnimationInteract = Animator.StringToHash("Interact");
	
	private void Awake()
	{
		_playerController = GetComponent<PlayerController>();
		_playerController.State.OnValueChanged += HandlePlayerState;
	}

	private void HandlePlayerState(PlayerController.PlayerState prev, PlayerController.PlayerState current)
	{
		//Debug.Log(current);
		switch (current)
		{
			case PlayerController.PlayerState.Idle:
				animator.SetBool(AnimationRun,false);
				break;
			
			case PlayerController.PlayerState.Run:
				animator.SetBool(AnimationRun,true);
				break;
			
			case PlayerController.PlayerState.TakeItem:
				animator.SetTrigger(AnimationTake);
				break;
			
			case PlayerController.PlayerState.Interact:
				animator.SetTrigger(AnimationInteract);
				break;

		}		
	}
}
