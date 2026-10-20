using System;
using System.Text;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using Random = UnityEngine.Random;

namespace _Project.Develop.Runtime.Gameplay.Utilities.CharsSelector
{
	public class GameCharsSelector : IGameCharsSelector
	{
		private string _gameCharsSequence;

		public GameCharsSelector(string gameCharsSequence)
		{
			_gameCharsSequence = gameCharsSequence;
		}
		
		public string GetGameChars(int charsCount)
		{
			if (_gameCharsSequence == null || _gameCharsSequence.Length < charsCount || charsCount <= 0)
				throw new InvalidOperationException("Game chars sequence is invalid");
			
			StringBuilder builder = new StringBuilder();

			for (int i = 0; i < charsCount; i++)
			{
				char randomChar = _gameCharsSequence[Random.Range(0, _gameCharsSequence.Length)];
				
				builder.Append(randomChar);
			}
			
			return builder.ToString();
		}
	}
}

