using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.LIfeCycle.Energy
{
	public class EnergyRegenerationTimerSystem : IInitializableSystem, IUpdatableSystem
	{
		private ReactiveVariable<float> _percentByMaxRegeneration;

		private ReactiveVariable<float> _regenerationInitialTime;
		private ReactiveVariable<float> _regenerationCurrentTime;

		private ReactiveVariable<float> _maxEnergy;
		private ReactiveVariable<float> _currentEnergy;

		private ReactiveEvent<float> _regenerationRequest;
		
		public void OnInit(Entity entity)
		{
			_percentByMaxRegeneration = entity.PercentByMaxRegenerationEnergy;
			_regenerationInitialTime = entity.RegenerationEnergyInitialTime;
			_regenerationCurrentTime = entity.RegenerationEnergyCurrentTime;

			_maxEnergy = entity.MaxEnergy;
			_currentEnergy = entity.CurrentEnergy;

			_regenerationRequest = entity.RegenerationEnergyRequest;
		}

		public void OnUpdate(float dt)
		{
			if (_currentEnergy.Value >= _maxEnergy.Value)
				return;
			
			if (_regenerationCurrentTime.Value >= _regenerationInitialTime.Value)
			{
				float regenerationEnergy = _percentByMaxRegeneration.Value * _maxEnergy.Value;

				_regenerationRequest.Invoke(regenerationEnergy);
				_regenerationCurrentTime.Value = 0f;
				
				Debug.Log($"current energy:  {_currentEnergy.Value} / {_maxEnergy.Value}");
				
				return;
			}
			
			_regenerationCurrentTime.Value += dt;
		}
	}
}