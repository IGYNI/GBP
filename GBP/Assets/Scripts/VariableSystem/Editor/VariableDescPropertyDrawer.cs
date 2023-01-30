using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(VariableDesc))]
public class VariableDescPropertyDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		//base.OnGUI(position, property, label);
		GUI.Box(position, label.text);
		var varName = property.FindPropertyRelative("name"); 
		var varValue = property.FindPropertyRelative("value"); 
		position.y = 18;
		position.height = 20;
		varName.stringValue = EditorGUI.TextField(position, "Variable Name", varName.stringValue);
		position.y = 40;
		varValue.stringValue = EditorGUI.TextField(position, "Check Value", varValue.stringValue);
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		//return base.GetPropertyHeight(property, label) * 2;
		return 62;
	}
}