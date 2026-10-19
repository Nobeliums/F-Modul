using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
	public class GameplayInputArgs : IInputSceneArgs
	{
		public GameplayInputArgs(int levelNumber)
		{
			LevelNumber = levelNumber;
		}

		public int LevelNumber { get; }
	}
}