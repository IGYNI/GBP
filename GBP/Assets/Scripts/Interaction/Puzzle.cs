using UnityEngine;

public class Puzzle : MonoBehaviour
{
	public enum EState
	{
		Idle,
		Processing,
		Complete,
		Failed,
	}

	public IObservableVar<EState> State => state;
	protected readonly ObservableVar<EState> state = new ObservableVar<EState>();

	public virtual void Show()
	{
		gameObject.SetActive(true);
		state.Value = EState.Processing;
	}
	
	public virtual void Hide()
	{
		state.Value = EState.Idle;
		gameObject.SetActive(false);
	}
	
}