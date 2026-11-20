using System;
using _Project.Develop.Runtime.Gameplay.Configs.Entities;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Configs.Stages
{
	[Serializable]
	public class EnemyItemConfig
	{
		[field: SerializeField]
		public Vector3 SpawnPosition { get; private set; }

		[field: SerializeField]
		public EntityConfig EntityConfig { get; private set; }
	}
}
