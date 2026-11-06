using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Configs
{
	[CreateAssetMenu(menuName = "Project/EnergyConfig", fileName = "EnergyConfig")]
	public class EnergyConfig : ScriptableObject
	{
		[field: SerializeField]
		public float MaxEnergy { get; private set; }
		
		[field: SerializeField]
		public float StartEnergy { get; private set; }
		
		[field: SerializeField]
		public float EnergyRegeneretionPeriodInitialTime { get; private set; }
		
		[field: SerializeField, Range(0f, 1f)]
		public float EnergyRegenerationPercentByMax { get; private set; }
	}
}