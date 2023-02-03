public class FabricatorToInventoryBridge : IItemHandler
{
	private readonly Fabricator _fabricator;
	private readonly Inventory _inventory;

	public FabricatorToInventoryBridge(Fabricator fabricator, Inventory inventory)
	{
		_inventory = inventory;
		_fabricator = fabricator;
	}
	
	public void ProcessItem(ItemInfo itemInfo)
	{
		if (_fabricator.RemoveIngredient(itemInfo))
		{
			_inventory.AddItem(itemInfo);
		}

		if (_fabricator.RemoveResult(itemInfo))
		{
			_inventory.AddItem(itemInfo);
		}
	}
}