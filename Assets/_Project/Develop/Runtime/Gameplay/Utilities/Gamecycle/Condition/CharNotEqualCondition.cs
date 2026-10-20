using System;

namespace _Project.Develop.Runtime.Gameplay.Utilities.Gamecycle.Condition
{
	public class CharNotEqualCondition : ICondition
	{
		public event Action ConditionCompletedEvent;

		public void CheckCondition(string targetPhrase, string playerPhrase)
		{
			for (int i = 0; i < playerPhrase.Length; i++)
			{
				if (playerPhrase[i] != targetPhrase[i])
				{
					ConditionCompletedEvent?.Invoke();
				}
			}
		}
	}
}