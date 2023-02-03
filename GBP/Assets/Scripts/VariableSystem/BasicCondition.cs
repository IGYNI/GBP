using System.Collections.Generic;

public class BasicCondition : CheckCondition
{
	public List<VariableDesc> checkVariables;


	public override bool Satisfied()
	{
		var variableSystem = VariableSystem.Instance;
		foreach (VariableDesc variableDesc in checkVariables)
		{
			var gameVar = variableSystem.GetVariable(variableDesc.name);
			if (gameVar != null)
			{
				if (gameVar.Value != variableDesc.value)
				{
					return false;
				}
			}
			else
				return false;
		}

		return true;
	}
}
