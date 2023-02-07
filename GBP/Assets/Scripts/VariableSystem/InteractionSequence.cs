using System.Collections;
using UnityEngine;

public abstract class InteractionSequence : MonoBehaviour
{
	public abstract IEnumerator Proceed();
}