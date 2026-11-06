using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeuture
{
	public class TeleportCooldownSystem : IInitializableSystem, IUpdatableSystem, IDisposableSystem
	{
		private ReactiveVariable<float> _cooldownCurrentTime;
		private ReactiveVariable<float> _coodownInitialTime;

		private ReactiveVariable<bool> _inTeleportCooldown;

		private ReactiveEvent _teleportEvent;

		private IDisposable _teleportEventDisposable;
		
		public void OnInit(Entity entity)
		{
			_cooldownCurrentTime = entity.GetComponent<TeleportCooldownCurrentTime>().Value;
			_coodownInitialTime = entity.GetComponent<TeleportCooldownInitialTime>().Value;
			_inTeleportCooldown = entity.GetComponent<InTeleportCooldown>().Value;
			
			_teleportEvent = entity.GetComponent<TeleportEvent>().Value;

			_teleportEventDisposable = _teleportEvent.Subscribe(OnTeleport);
		}

		public void OnUpdate(float dt)
		{
			if (_inTeleportCooldown.Value == false)
				return;
			
			_cooldownCurrentTime.Value -= dt;
			
			if (_cooldownCurrentTime.Value <= 0)
				_inTeleportCooldown.Value = false;
		}

		private void OnTeleport()
		{
			_inTeleportCooldown.Value = true;
			_cooldownCurrentTime.Value = _coodownInitialTime.Value;
		}

		public void OnDispose()
		{
			_teleportEventDisposable.Dispose();
		}
	}
}