using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.Input
{
	public static class InputService
	{
		private const KeyCode EndGameButton = KeyCode.Space;
		private const KeyCode DigitsGameModeButton = KeyCode.Alpha1;
		private const KeyCode LettersGameModeButton = KeyCode.Alpha2;
		private const KeyCode StatisticsButton = KeyCode.S;
		private const KeyCode BuyItemButton = KeyCode.G;
		
		public static bool GetEndGameButton()
		{
			return UnityEngine.Input.GetKeyDown(EndGameButton);
		}

		public static string GetPressedKeys()
		{
			return UnityEngine.Input.inputString;
		}

		public static bool GetStatisticsButton()
		{
			return UnityEngine.Input.GetKeyDown(StatisticsButton);
		}

		public static bool GetBuyItemButton()
		{
			return UnityEngine.Input.GetKeyDown(BuyItemButton);
		}

		public static bool GetDigitsGameModeButton()
		{
			return UnityEngine.Input.GetKeyDown(DigitsGameModeButton);
		}

		public static bool GetLettersGameModeButton()
		{
			return UnityEngine.Input.GetKeyDown(LettersGameModeButton);
		}

		public static Vector3 GetMovementDirection()
		{
			return new Vector3(UnityEngine.Input.GetAxisRaw("Horizontal"), 0.0f, UnityEngine.Input.GetAxisRaw("Vertical")).normalized;
		}
	}
}
