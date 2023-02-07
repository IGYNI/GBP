public class InventoryToFabricatorBridge : IItemHandler
{
	private readonly Fabricator _fabricator;
	private readonly Inventory _inventory;
	private VariableSystem _variableSystem;

	public InventoryToFabricatorBridge(Fabricator fabricator, Inventory inventory, VariableSystem variableSystem)
	{
		_variableSystem = variableSystem;
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