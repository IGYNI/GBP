using UnityEngine;

//[RequireComponent(typeof(DemoPlayer))]
public class PlayerMindController : MonoBehaviour
{
	[SerializeField] private MindView mindView;
	[SerializeField] private PlayerMinds playerMinds;
	[SerializeField] private float minIdleTime = 15f;
	//[SerializeField] private float showMindTime = 10f;

	//private DemoPlayer _player;
	private Vector3 _lastPosition;

	private bool _playerIdle;
	private float _idleTimer;
	
	private void Awake()
	{
		//_player = GetComponent<DemoPlayer>();
	}

	private void Update()
	{
		//_playerIdle = _lastPosition == _player.transform.position;
		if (_playerIdle)
		{
			_idleTimer += Time.deltaTime;
		}
		else
		{
			_idleTimer = 0f;
			mindView.Hide();
		}

		if (_idleTimer >= minIdleTime)
		{
			Debug.Log("Player think about something");
			//mindView.ShowMind();
		}
	}
}
