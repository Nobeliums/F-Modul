using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeuture
{
	public class TeleportByConditionRequestSystem : IInitializableSystem, IUpdatableSystem
	{
		private ICompositeCondition _canTeleport;

		private ReactiveEvent<Vector3> _teleportRequest;
		
		public void OnInit(Entity entity)
		{
			_canTeleport = entity.CanTeleport;
			_teleportRequest = entity.TeleportRequest;
		}

		public void OnUpdate(float dt)
		{
			// if (_canTeleport.Evaluate())
			// 	_teleportRequest.Invoke();
		}
	}
}