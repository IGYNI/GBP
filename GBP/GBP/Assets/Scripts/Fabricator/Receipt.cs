using System;
using System.Collections.Generic;

[Serializable]
public class Receipt
{
	public string screenName;
	public float fabricationTime = 5f;
	public List<ItemInfo> requiredItems;
	public ItemInfo result;
}
