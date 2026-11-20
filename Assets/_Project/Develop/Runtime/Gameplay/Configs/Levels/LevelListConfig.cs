using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Configs.Levels
{
	[CreateAssetMenu(menuName = "Project/LevelListConfig", fileName = "LevelListConfig", order = 0)]
	public class LevelListConfig : ScriptableObject
	{
		[SerializeField]
		private List<LevelConfig> _levelList;

		public int Count => _levelList.Count;

		public LevelConfig GetBy(int number)
		{
			if (number < 1)
				throw new ArgumentException($"{nameof(number)} must be at least 1: {number}");

			if (number > _levelList.Count)
				throw new ArgumentException($"{nameof(number)} is too big: {number}");

			return _levelList[number - 1];
		}
	}
}
