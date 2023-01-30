using System;
using System.Collections.Generic;
using UnityEngine;

namespace SceneManagement
{
	[Serializable]
	public class SerializedSceneInfo : ISceneInfo
	{
		public string sceneName;
		public List<string> hints;
		public List<Sprite> images;
		
		public string SceneName => sceneName;
		public List<string> Hints => hints;
		public List<Sprite> Images => images;
	}
}