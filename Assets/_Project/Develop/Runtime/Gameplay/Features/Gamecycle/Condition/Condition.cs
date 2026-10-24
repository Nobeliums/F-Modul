using System;

namespace _Project.Develop.Runtime.Gameplay.Features.Gamecycle.Condition
{
	public interface ICondition
	{
		public event Action ConditionCompletedEvent;
		
		public abstract void CheckCondition(string targetPhrase, string playerPhrase);
	}
}