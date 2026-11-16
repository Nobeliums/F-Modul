using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Configs
{
	[CreateAssetMenu(menuName = "Project/Create TeleportConfig", fileName = "TeleportConfig", order = 0)]
	public class TeleportConfig : ScriptableObject
	{
		[field: SerializeField, Min(0)]
		public float TeleportRadius { get; private set; }
		
		[field: SerializeField, Min(0)]
		public float TeleportCooldown { get; private set; }
		
		[field: SerializeField, Min(0)]
		public float TeleportEnergyCost { get; private set; }
	}
}