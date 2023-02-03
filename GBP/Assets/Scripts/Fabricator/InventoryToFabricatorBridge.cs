public class InventoryToFabricatorBridge : ItemHandler
{
	private readonly Fabricator _fabricator;
	private readonly Inventory _inventory;

	public InventoryToFabricatorBridge(Fabricator fabricator, Inventory inventory)
	{
		_inventory = inventory;
		_fabricator = fabricator;
	}
	
	public void ProcessItem(ItemInfo itemInfo)
	{
		if (_fabricator.AddIngredient(itemInfo))
		{
			_inventory.RemoveItem(itemInfo);
		}
	}
}