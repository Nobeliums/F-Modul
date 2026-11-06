using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Develop.Runtime.Gameplay.Configs
{
	[CreateAssetMenu(menuName = "Project/GhostConfig", fileName = "GhostConfig", order = 0)]
	public class GhostConfig : ScriptableObject
	{
		[field: SerializeField]
		public MovementConfig MovementConfig { get; private set; }
		
		[field: SerializeField]
		public HealthConfig HealthConfig { get; private set; }
		
		[field: SerializeField]
		public AttackConfig AttackConfig { get; private set; }
	}
}