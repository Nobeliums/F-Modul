using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Configs
{
	[CreateAssetMenu(menuName = "Project/Create TeleportConfig", fileName = "TeleportConfig", order = 0)]
	public class TeleportConfig : ScriptableObject
	{
		[field: SerializeField]
		public float TeleportRadius { get; private set; }
		
		[field: SerializeField]
		public float TeleportCooldown { get; private set; }
		
		[field: SerializeField]
		public float TeleportEnergyCost { get; private set; }
	}
}