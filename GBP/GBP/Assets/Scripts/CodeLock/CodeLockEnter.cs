using UnityEngine;

public class CodeLockEnter : CodeLockButton
{
	[SerializeField] private CodeLock codeLock;
	
	public override void Press()
	{
		codeLock.Enter();
	}
}