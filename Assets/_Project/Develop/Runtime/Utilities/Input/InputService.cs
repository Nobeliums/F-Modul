using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.Input
{
	public static class InputService
	{
		private const KeyCode EndGameButton = KeyCode.Space;
		private const KeyCode DigitsGameModeButton = KeyCode.Alpha1;
		private const KeyCode LettersGameModeButton = KeyCode.Alpha2;
		
		public static bool GetEndGameButton()
		{
			return UnityEngine.Input.GetKeyDown(EndGameButton);
		}

		public static string GetPressedKeys()
		{
			return UnityEngine.Input.inputString;
		}

		public static bool GetDigitsGameModeButton()
		{
			return UnityEngine.Input.GetKeyDown(DigitsGameModeButton);
		}

		public static bool GetLettersGameModeButton()
		{
			return UnityEngine.Input.GetKeyDown(LettersGameModeButton);
		}
	}
}
