using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
	public class FindTargetState : State, IUpdatebleState
	{
		private ITargetSelector _targetSelector;
		private EntitiesLifeContext _entitiesLifeContext;
		private ReactiveVariable<Entity> _currentTarget;

		public FindTargetState(ITargetSelector targetSelector,
			EntitiesLifeContext entitiesLifeContext, Entity entity)
		{
			_targetSelector = targetSelector;
			_entitiesLifeContext = entitiesLifeContext;
			_currentTarget = entity.CurrentTarget;
		}

		public void Update(float deltaTime)
		{
			_currentTarget.Value = _targetSelector.SelectTargetFrom(_entitiesLifeContext.Entities);
		}
	}
}