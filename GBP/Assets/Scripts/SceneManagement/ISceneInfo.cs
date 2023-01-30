using System.Collections.Generic;
using UnityEngine;

namespace SceneManagement
{
	public interface ISceneInfo
	{
		string SceneName { get; }
		public List<string> Hints { get; }
		public List<Sprite> Images { get; }
	}
}