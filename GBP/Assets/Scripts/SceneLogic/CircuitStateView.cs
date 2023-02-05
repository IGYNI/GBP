using UnityEngine;

public class CircuitStateView : MonoBehaviour
{
	[SerializeField] private GameObject activeView;
	[SerializeField] private GameObject lockedView;
	
	public void ActiveState()
	{
		activeView.SetActive(true);
		lockedView.SetActive(false);
	}

	public void LockedState()
	{
		activeView.SetActive(false);
		lockedView.SetActive(true);
	}
}