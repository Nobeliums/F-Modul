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
			_percentByMaxRegeneration = entity.GetComponent<PercentByMaxRegenerationEnergy>().Value;
			_regenerationInitialTime = entity.GetComponent<RegenerationEnergyInitialTime>().Value;
			_regenerationCurrentTime = entity.GetComponent<RegenerationEnergyCurrentTime>().Value;

			_maxEnergy = entity.GetComponent<MaxEnergy>().Value;
			_currentEnergy = entity.GetComponent<CurrentEnergy>().Value;
			
			_regenerationRequest = entity.GetComponent<RegenerationEnergyRequest>().Value;
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