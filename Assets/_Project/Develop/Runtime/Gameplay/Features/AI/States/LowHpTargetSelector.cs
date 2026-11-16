using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using _Project.Develop.Runtime.Gameplay.Features.LIfeCycle;
using _Project.Develop.Runtime.Utilities.Conditions;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
	public class LowHpTargetSelector : ITargetSelector
	{
		private Entity _source;

		public LowHpTargetSelector(Entity source)
		{
			_source = source;
		}

		public Entity SelectTargetFrom(IEnumerable<Entity> targets)
		{
			IEnumerable<Entity> selectedTargets = targets.Where(target =>
			{
				bool result = target.HasComponent<TakeDamageRequest>();
				result = result && target.HasComponent<CurrentHealth>();

				if (target.TryGetCanApplyDamage(out ICompositeCondition canApplyDamage))
				{
					result = result && canApplyDamage.Evaluate();
				}

				result = result && target != _source;

				return result;
			});
			
			if (selectedTargets.Any() == false)
				return null;
			
			Entity currentTarget = selectedTargets.First();
			
			foreach (Entity target in selectedTargets)
			{
				float currentTargetHealth = currentTarget.CurrentHealth.Value;
				float targetHealth = target.CurrentHealth.Value;

				if (targetHealth < currentTargetHealth)
					currentTarget = target;
			}
			
			return currentTarget;
		}
	}
}