using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeuture
{
	public class TeleportCooldownStartSystem : IInitializableSystem, IDisposableSystem
	{
		private ReactiveVariable<float> _cooldownCurrentTime;
		private ReactiveVariable<float> _coodownInitialTime;

		private ReactiveVariable<bool> _inTeleportCooldown;

		private ReactiveEvent _teleportEvent;

		private IDisposable _teleportEventDisposable;
		
		public void OnInit(Entity entity)
		{
			_cooldownCurrentTime = entity.TeleportCooldownCurrentTime;
			_coodownInitialTime = entity.TeleportCooldownInitialTime;
			_inTeleportCooldown = entity.InTeleportCooldown;

			_teleportEvent = entity.TeleportEvent;

			_teleportEventDisposable = _teleportEvent.Subscribe(OnTeleport);
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