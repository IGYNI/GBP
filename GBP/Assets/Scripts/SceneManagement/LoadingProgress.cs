namespace SceneManagement
{
	public struct LoadingProgress
	{
		public float progress;
		public string description;

		public static LoadingProgress Full = new LoadingProgress() { progress = 1f };
	}
}