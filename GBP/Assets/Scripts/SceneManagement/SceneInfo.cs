using System.Collections.Generic;
using UnityEngine;

namespace SceneManagement
{
	[CreateAssetMenu(menuName = "Scene Management/Scene Info", fileName = "SceneInfo")]
	public class SceneInfo : ScriptableObject
	{
		public string sceneName;
		public List<string> hints;
	}
}