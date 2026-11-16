using _Project.Develop.Runtime.Gameplay.Configs.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Develop.Runtime.Gameplay.Configs
{
	[CreateAssetMenu(menuName = "Project/PlayerConfig", fileName = "PlayerConfig")]
	public class PlayerConfig : EntityConfig
	{
		[field: SerializeField]
		public MovementConfig MovementConfig { get; private set; }
		
		[field: SerializeField]
		public HealthConfig HealthConfig { get; private set; }
		
		[field: SerializeField]
		public AttackConfig AttackConfig { get; private set; }
	}
}