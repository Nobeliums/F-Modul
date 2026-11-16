using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
	public interface IPointSelector
	{
		Vector3 GetPoint();
	}

	public class ToTargetDirectionPointSelector : IPointSelector
	{
		private Entity _source;
		private ReactiveVariable<Entity> _currentTarget;
		private ReactiveVariable<float> _radius;

		public ToTargetDirectionPointSelector(ReactiveVariable<Entity> currentTarget, Entity source, ReactiveVariable<float> radius)
		{
			_currentTarget = currentTarget;
			_source = source;
			_radius = radius;
		}

		public Vector3 GetPoint()
		{
			Vector3 toTargetDirection =
				(_currentTarget.Value.TransformComponent.position - _source.TransformComponent.position);

			float distanceToTarget = toTargetDirection.magnitude;
			float teleportDistance = distanceToTarget > _radius.Value ? _radius.Value : distanceToTarget;
			
			return toTargetDirection.normalized * teleportDistance;
		}
	}
}