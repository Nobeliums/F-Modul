using System;

namespace _Project.Develop.Runtime.Gameplay.Utilities.Gamemode.Condition
{
	public interface ICondition
	{
		public event Action ConditionCompletedEvent;
		
		public abstract void CheckCondition(string targetPhrase, string playerPhrase);
	}
}