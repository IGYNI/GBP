using UnityEngine;


public class DemoRaycast : MonoBehaviour
{
	[SerializeField] private Camera rayCam;
	[SerializeField] private VariableSystem variableSystem;
	[SerializeField] private VariableDesc variableDesc;
	
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			var ray = rayCam.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out var hit))
			{
				if (hit.transform.gameObject.name == "Item")
				{
					variableSystem.SetVariable(variableDesc.name, variableDesc.value);
					Destroy(hit.transform.gameObject);
				}
			}
		}
	}
}
