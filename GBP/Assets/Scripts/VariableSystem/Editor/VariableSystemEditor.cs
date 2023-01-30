using UnityEditor;

[CustomEditor(typeof(VariableSystem))]
public class VariableSystemEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		var vs = (VariableSystem)target;
		if (vs.Variables != null)
		{
			EditorGUILayout.LabelField("VARIABLES");
			foreach (var vsVariable in vs.Variables)
			{
				//EditorGUILayout.BeginHorizontal("box");
				EditorGUILayout.LabelField(vsVariable.Key, vsVariable.Value.Value);
				//EditorGUILayout.EndHorizontal();
			}
		}
		else
		{
			EditorGUILayout.LabelField("Runtime variables");
		}

	}
}
