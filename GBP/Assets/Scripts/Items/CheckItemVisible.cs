using UnityEngine;

public class CheckItemVisible : MonoBehaviour
{
	[SerializeField] private float repeat = 0.2f;
	[SerializeField] private ItemInfo item;
	[SerializeField] private GameObject target;

	private float _timer;

	private VariableSystem _variableSystem;

	private void Start()
	{
		_variableSystem = VariableSystem.Instance;
	}

	private void Update()
	{
		if (_timer >= repeat)
		{
			_timer = 0f;
			CheckVisible();
		}

		_timer += Time.deltaTime;
	}

	private void CheckVisible()
	{
		var gameVar = _variableSystem.GetVariable(item.itemName + Item.VisibleSuffix);
		if (gameVar == null)
			return;
		target.SetActive(gameVar.Value == "true");
	}
}