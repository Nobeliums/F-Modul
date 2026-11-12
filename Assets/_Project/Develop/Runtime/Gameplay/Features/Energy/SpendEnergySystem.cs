using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using Unity.VisualScripting;

namespace _Project.Develop.Runtime.Gameplay.Features.LIfeCycle.Energy
{
	public class SpendEnergySystem : IInitializableSystem, IDisposableSystem
	{
		private ReactiveVariable<float> _currentEnergy;
		private ReactiveEvent<float> _spendEnergyRequest;
		private ReactiveEvent<float> _spendEnergyEvent;
		
		private ICompositeCondition _canSpendCondition;
		
		private IDisposable _requestDisposable;

		public void OnInit(Entity entity)
		{
			_currentEnergy = entity.CurrentEnergy;
			_spendEnergyRequest = entity.SpendEnergyRequest;
			_spendEnergyEvent = entity.SpendEnergyEvent;
			_canSpendCondition = entity.CanSpendEnergy;

			_requestDisposable = _spendEnergyRequest.Subscribe(OnSpendEnergyRequest);
		}

		private void OnSpendEnergyRequest(float energyCost)
		{
			if (energyCost < 0)
				throw new ArgumentOutOfRangeException(nameof(energyCost));

			if (_canSpendCondition.Evaluate())
			{
				if (_currentEnergy.Value < energyCost)
					return;
				
				_currentEnergy.Value -= energyCost;
				_spendEnergyEvent.Invoke(energyCost);
			}
		}

		public void OnDispose()
		{
			_requestDisposable.Dispose();
		}
	}
}