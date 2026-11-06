using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Develop.Runtime.Gameplay.Configs
{
	[CreateAssetMenu(menuName = "Project/TeleportGhostConfig", fileName = "TeleportGhostConfig", order = 0)]
	public class GhostTeleportConfig : ScriptableObject
	{
		[field: SerializeField]
		public TeleportConfig TeleportConfig { get; private set; }
		
		[field: SerializeField]
		public HealthConfig HealthConfig { get; private set; }
		
		[field: SerializeField]
		public TeleportAttackConfig AttackConfig { get; private set; }
		
		[field: SerializeField]
		public EnergyConfig  EnergyConfig { get; private set; }
	}
}