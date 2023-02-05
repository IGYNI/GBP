using UnityEngine;

public class CodeLockDeleteDigit : CodeLockButton
{
	[SerializeField] private CodeLock codeLock;
	
	public override void Press()
	{
		codeLock.DeleteDigit();
	}
}