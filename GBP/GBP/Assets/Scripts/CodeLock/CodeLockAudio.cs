using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeLockAudio : MonoBehaviour
{
    public void LockButtonClick()
    {
        AudioManager.instance.InitializeLockClick();
    }
    public void LockFail()
    {
        AudioManager.instance.InitializeLockFail();
    }
    public void LockOpen()
    {
        AudioManager.instance.InitializeLockOpen();
    }
    public void LockDone()
    {
        AudioManager.instance.InitializeLockDone();
    }
}
