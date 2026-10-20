using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
	public class GameplayInputArgs : IInputSceneArgs
	{
		private readonly GameCharsSequenceType _gameCharsSequenceType;
		public GameplayInputArgs(GameCharsSequenceType gameCharsSequenceType)
		{
			_gameCharsSequenceType = gameCharsSequenceType;
		}

		public GameCharsSequenceType GameCharsSequenceType => _gameCharsSequenceType;
	}
}