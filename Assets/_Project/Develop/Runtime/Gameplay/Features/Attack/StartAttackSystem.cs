using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack
{
	public class StartAttackSystem : IInitializableSystem, IDisposableSystem
	{
		private ReactiveEvent _startAttackRequest;
		private ReactiveEvent _startAttackEvent;

		private ReactiveVariable<bool> _inAttackProcess;
		private ICompositeCondition _canStartAttack;

		private IDisposable _startAttackRequestDisposable;
		
		public void OnInit(Entity entity)
		{
			_startAttackRequest = entity.GetComponent<StartAttackRequest>().Value;
			_startAttackEvent = entity.GetComponent<StartAttackEvent>().Value;
			
			_inAttackProcess = entity.GetComponent<InAttackProcess>().Value;
			_canStartAttack = entity.GetComponent<CanStartAttack>().Value;

			_startAttackRequestDisposable = _startAttackRequest.Subscribe(OnStartAttackRequest);
		}

		private void OnStartAttackRequest()
		{
			if (_canStartAttack.Evaluate())
			{
				_inAttackProcess.Value = true;
				_startAttackEvent.Invoke();
				Debug.Log("Start Attack");
			}
			else
			{
				Debug.Log("Can't attack");
			}
		}

		public void OnDispose()
		{
			_startAttackRequestDisposable.Dispose();
		}
	}
}