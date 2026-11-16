using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Configs
{
	[CreateAssetMenu(menuName = "Project/Create TeleportAttackConfig", fileName = "TeleportAttackConfig", order = 0)]
	public class TeleportAttackConfig : ScriptableObject
	{
		[field: SerializeField, Min(0)]
		public float Damage { get; private set; }

		[field: SerializeField, Min(0)]
		public float TeleportTakeDamageRadius { get; private set; }
	}
}