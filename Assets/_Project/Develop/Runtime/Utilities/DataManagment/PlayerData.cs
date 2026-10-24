using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Features.WalletService;

namespace _Project.Develop.Runtime.Utilities.DataManagment
{
	public class PlayerData : ISaveData
	{
		public Dictionary<WalletCurrencyType, int> WalletData;

		public int WinsCount;
		public int LoseCount;
	}
}