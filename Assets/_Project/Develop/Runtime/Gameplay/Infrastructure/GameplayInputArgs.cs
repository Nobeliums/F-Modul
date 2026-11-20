using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
	public class GameplayInputArgs : IInputSceneArgs
	{
		private readonly int _levelNumber;

		public GameplayInputArgs(int levelNumber)
		{
			_levelNumber = levelNumber;
		}

		public int LevelNumber => _levelNumber;
	}
}