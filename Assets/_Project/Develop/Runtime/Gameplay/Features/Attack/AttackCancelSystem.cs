using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack
{
	public class AttackCancelSystem : IInitializableSystem, IUpdatableSystem
	{
		private ReactiveVariable<bool> _inAttackProcess;
		private ReactiveEvent _attackCanceledEvent;

		private ICompositeCondition _mustCancelAttack;
		
		public void OnInit(Entity entity)
		{
			_inAttackProcess = entity.GetComponent<InAttackProcess>().Value;
			_attackCanceledEvent = entity.GetComponent<AttackCanceledEvent>().Value;
			
			_mustCancelAttack = entity.GetComponent<MustCancelAttack>().Value;
		}


		public void OnUpdate(float dt)
		{
			if (_inAttackProcess.Value == false)
				return;
			
			if (_mustCancelAttack.Evaluate())
			{
				Debug.Log("Attack Process Canceled");
				_inAttackProcess.Value = false;
				_attackCanceledEvent.Invoke();
			}
		}
	}
}