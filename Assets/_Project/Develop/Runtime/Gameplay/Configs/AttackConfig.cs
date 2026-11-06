using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Configs
{
	[CreateAssetMenu(fileName = "DamageConfig", menuName = "Project/DamageConfig")]
	public class AttackConfig : ScriptableObject
	{
		[field: SerializeField]
		public float Damage { get; private set; }
		
		[field: SerializeField]
		public float InitialAttackTime { get;  private set; }
		
		[field: SerializeField]
		public float AttackDelayTime { get; private set; }
		
		[field: SerializeField]
		public float AttackCooldownInitialTime { get; private set; }
	}
}