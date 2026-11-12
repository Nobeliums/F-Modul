using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Sensors
{
	public class DeathMaskTouchDetectorSystem : IInitializableSystem, IUpdatableSystem
	{
		private Buffer<Collider> _colliderBuffer;
		private ReactiveVariable<bool> _isTouchDeathMask;

		private LayerMask _deathMask;

		public void OnInit(Entity entity)
		{
			_colliderBuffer = entity.ContactColliderBuffer;
			_isTouchDeathMask = entity.IsTouchDeathMask;

			_deathMask = entity.DeathMask;
		}

		public void OnUpdate(float dt)
		{
			for (int i = 0; i < _colliderBuffer.Count; i++)
			{
				if (MatchWithDeathLayer(_colliderBuffer.Items[i]))
				{
					_isTouchDeathMask.Value = true;
					return;
				}
			}
			
			_isTouchDeathMask.Value = false;
		}

		private bool MatchWithDeathLayer(Collider collider)
		{
			return ((1 << collider.gameObject.layer) & _deathMask) != 0;
		}
	}
}