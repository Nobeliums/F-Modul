using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.LIfeCycle.Energy
{
	public class EnergyRegenerationSystem : IInitializableSystem, IDisposableSystem
	{
		private ReactiveEvent<float> _regenerationRequest;
		private ReactiveEvent<float> _regenerationEvent;

		private ReactiveVariable<float> _currentEnergy;
		private ReactiveVariable<float> _maxEnergy;

		private ICompositeCondition _canRegenerateEnergy;
		
		private IDisposable _requestDisposable;


		public void OnInit(Entity entity)
		{
			_regenerationRequest = entity.RegenerationEnergyRequest;
			_regenerationEvent = entity.RegenerationEnergyEvent;
			_currentEnergy = entity.CurrentEnergy;
			_maxEnergy = entity.MaxEnergy;
			_canRegenerateEnergy = entity.CanRegenerateEnergy;

			_requestDisposable = _regenerationRequest.Subscribe(OnRegenerationRequest);
		}

		private void OnRegenerationRequest(float energy)
		{
			if (energy < 0)
				throw new ArgumentOutOfRangeException(nameof(energy));

			if (_canRegenerateEnergy.Evaluate())
			{
				_currentEnergy.Value = MathF.Min(_currentEnergy.Value + energy, _maxEnergy.Value);
				
				_regenerationEvent.Invoke(energy);
			}
		}


		public void OnDispose()
		{
			_requestDisposable.Dispose();
		}
	}
}