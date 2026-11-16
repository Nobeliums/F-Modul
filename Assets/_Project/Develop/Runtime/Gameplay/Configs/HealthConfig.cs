using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Configs
{
	[CreateAssetMenu(menuName = "Project/HealthConfig", fileName = "HealthConfig", order = 0)]
	public class HealthConfig : ScriptableObject
	{
		[field: SerializeField, Min(0)]
		public float MaxHealth { get; private set; }
		
		[field: SerializeField, Min(0)]
		public float StartHealth { get; private set; }
		
		[field: SerializeField, Min(0)]
		public float DeathProcessInitialTime { get; private set; }
	}
}