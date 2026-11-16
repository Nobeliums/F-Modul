using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
	public class TeleportState : State, IUpdatebleState
	{
		private ReactiveVariable<bool> _inTeleportCooldown;
		private ReactiveVariable<float> _teleportEnergyCost;
		private ReactiveEvent<float> _spendEnergyRequest;
		
		private ReactiveEvent<Vector3> _teleportRequest;
		
		private IPointSelector  _pointSelector;
		
		public TeleportState(
			ReactiveVariable<bool> inTeleportCooldown,
			ReactiveEvent<Vector3> teleportRequest,
			IPointSelector pointSelector,
			ReactiveVariable<float> teleportEnergyCost,
			ReactiveEvent<float> spendEnergyRequest)
		{
			_inTeleportCooldown = inTeleportCooldown;
			_teleportRequest = teleportRequest;
			_pointSelector = pointSelector;
			_teleportEnergyCost = teleportEnergyCost;
			_spendEnergyRequest = spendEnergyRequest;
		}

		public override void Enter()
		{
			base.Enter();
			
			_spendEnergyRequest.Invoke(_teleportEnergyCost.Value);

			Vector3 newPoint = _pointSelector.GetPoint();
			
			_teleportRequest.Invoke(newPoint);
			_inTeleportCooldown.Value = true;
		}

		public void Update(float deltaTime)
		{
		}
	}
}