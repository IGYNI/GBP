public class FabricatorToInventoryBridge : IItemHandler
{
	private readonly Fabricator _fabricator;
	private readonly Inventory _inventory;
	private readonly VariableSystem _variableSystem;

	public FabricatorToInventoryBridge(Fabricator fabricator, Inventory inventory, VariableSystem variableSystem)
	{
		_variableSystem = variableSystem;
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