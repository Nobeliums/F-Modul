using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Configs
{
	[CreateAssetMenu(menuName = "Project/HealthConfig", fileName = "HealthConfig", order = 0)]
	public class HealthConfig : ScriptableObject
	{
		[field: SerializeField]
		public float MaxHealth { get; private set; }
		
		[field: SerializeField]
		public float StartHealth { get; private set; }
		
		[field: SerializeField]
		public float DeathProcessInitialTime { get; private set; }
	}
}