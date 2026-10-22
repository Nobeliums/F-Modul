using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Configs
{
	[CreateAssetMenu(fileName = "GameModeConfig", menuName = "Project/GameModeConfig", order = 0)]
	public class GameModeConfig : ScriptableObject
	{
		[field: SerializeField]
		private List<GameModeConfigData> GameModeConfigDatas { get; set; }
		
		[field: SerializeField]
		public int PhraseLenght { get; private set; }

		[field: SerializeField]
		public int WinReward { get; private set; }

		[field: SerializeField]
		public int LosePenalty { get; private set; }

		public string GetGameCharsBy(GameCharsSequenceType gameCharsSequenceType)
		{
			return GameModeConfigDatas.Find(x => x.GameCharsSequenceType == gameCharsSequenceType).GameCharsSequence;
		}

		[Serializable]
		private class GameModeConfigData
		{
			[field: SerializeField]
			public GameCharsSequenceType GameCharsSequenceType { get; private set; }
		
			[field: SerializeField]
			public string GameCharsSequence { get; private set; }
		}
	}


}