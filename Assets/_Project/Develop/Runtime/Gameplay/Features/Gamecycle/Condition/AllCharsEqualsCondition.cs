using System;

namespace _Project.Develop.Runtime.Gameplay.Features.Gamecycle.Condition
{
	public class AllCharsEqualsCondition : ICondition
	{
		public event Action ConditionCompletedEvent;

		public void CheckCondition(string targetPhrase, string playerPhrase)
		{
			if (playerPhrase.Length != targetPhrase.Length)
				return;

			if (targetPhrase.Equals(playerPhrase))
			{
				ConditionCompletedEvent?.Invoke();
			}
		}
	}
}