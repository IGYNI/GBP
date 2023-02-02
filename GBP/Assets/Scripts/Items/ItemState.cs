using System;

[Serializable]
public struct ItemState
{
	public static ItemState Default = new ItemState();
	
	public bool active;
	public bool visible;

	public ItemState(bool active, bool visible)
	{
		this.active = active;
		this.visible = visible;
	}
	
	public ItemState(ItemState reference)
	{
		active = reference.active;
		visible = reference.visible;
	}
}
