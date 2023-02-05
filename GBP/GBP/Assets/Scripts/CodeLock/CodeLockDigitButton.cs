using UnityEngine;

public class CodeLockDigitButton : CodeLockButton
{
	public string digit;
	[SerializeField] private CodeLock codeLock;

	public override void Press()
	{
		codeLock.AddDigit(digit);
	}
}