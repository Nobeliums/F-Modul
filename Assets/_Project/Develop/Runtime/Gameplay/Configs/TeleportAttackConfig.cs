using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Configs
{
	[CreateAssetMenu(menuName = "Project/Create TeleportAttackConfig", fileName = "TeleportAttackConfig", order = 0)]
	public class TeleportAttackConfig : ScriptableObject
	{
		[field: SerializeField]
		public float Damage { get; private set; }

		[field: SerializeField]
		public float TeleportTakeDamageRadius { get; private set; }
	}
}