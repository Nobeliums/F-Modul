using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
	public class RandomPointInRadiusSelector : IPointSelector
	{
		private ReactiveVariable<float> _radius;
		
		public RandomPointInRadiusSelector(ReactiveVariable<float> radius)
		{
			_radius = radius;
		}
		
		public Vector3 GetPoint()
		{
			return GetRandomDirection() * GetRandomRadius();
		}

		private float GetRandomRadius()
		{
			return Random.Range(0f, _radius.Value);
		}

		private Vector3 GetRandomDirection()
		{
			float randomXDirection = Random.Range(-1f, 1f);
			float randomZDirection = Random.Range(-1f, 1f);
			
			return new Vector3(randomXDirection, 0f, randomZDirection).normalized;
		}
	}
}