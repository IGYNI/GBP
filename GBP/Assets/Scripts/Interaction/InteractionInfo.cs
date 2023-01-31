using System;

[Serializable]
public class InteractionInfo
{
	public enum EInteractionMode
	{
		Radius,
		FixedPoint,
	}

	public EInteractionMode interactionMode;
	public float interactionRadius;
}