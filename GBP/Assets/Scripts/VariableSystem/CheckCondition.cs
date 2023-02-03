using UnityEngine;

public abstract class CheckCondition : MonoBehaviour
{
	// [SerializeField] protected GameAction failAction;
	// [SerializeField] protected GameAction successAction;
	
	public abstract bool Satisfied();

	// public void PerformOnFail()
	// {
	// 	failAction.Invoke(gameObject, VariableSystem.Instance);
	// }
	//
	// public void PerformOnSuccess()
	// {
	// 	successAction.Invoke(gameObject, VariableSystem.Instance);
	// }
}
