using System.Collections.Generic;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Configs.Stages
{
	[CreateAssetMenu(menuName = "Project/ClearAllEnemiesStageConfig", fileName = "ClearAllEnemiesStageConfig", order = 0)]
	public class ClearAllEnemiesStageConfig : StageConfig
	{
		[SerializeField]
		private List<EnemyItemConfig> _enemies;

		public IReadOnlyList<EnemyItemConfig> Enemies => _enemies;
	}
}
