using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Utilities.WalletService;
using _Project.Develop.Runtime.Meta.Configs;
using _Project.Develop.Runtime.Utilities.ConfigManagement;
using _Project.Develop.Runtime.Utilities.DataManagment.SaveLoadService;

namespace _Project.Develop.Runtime.Utilities.DataManagment.DataProvider
{
	public class PlayerDataProvider : DataProvider<PlayerData>
	{
		private readonly ConfigService _configService;
		
		public PlayerDataProvider(ISaveLoadService saveLoadService, ConfigService configService) : base(saveLoadService)
		{
			_configService = configService;
		}

		protected override PlayerData GetOriginData()
		{
			return new PlayerData()
			{
				WalletData = InitWalletData(),
				WinsCount = 0,
				LoseCount = 0
			};
		}

		private Dictionary<WalletCurrencyType, int> InitWalletData()
		{
			StartWalletConfig config = _configService.GetConfig<StartWalletConfig>();
			
			Dictionary<WalletCurrencyType, int> walletData = new Dictionary<WalletCurrencyType, int>();

			foreach (WalletCurrencyType currencyType in Enum.GetValues(typeof(WalletCurrencyType)))
			{
				walletData[currencyType] = config.GetAmountBy(currencyType);
			}
			
			return walletData;
		}
	}
}