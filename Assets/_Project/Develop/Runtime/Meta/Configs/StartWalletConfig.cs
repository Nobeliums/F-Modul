using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Configs
{
	[CreateAssetMenu(fileName = "StartWalletConfig", menuName = "Project/StartWalletConfig", order = 0)]
	public class StartWalletConfig : ScriptableObject
	{
		[field: SerializeField]
		private List<WalletConfigData> _walletConfigDatas { get; set; }

		public int GetAmountBy(WalletCurrencyType currencyType)
			=> _walletConfigDatas.Find(x => x.CurrencyType == currencyType).StartAmount;
		
		[Serializable]
		private class WalletConfigData
		{
			[field: SerializeField]
			public WalletCurrencyType CurrencyType { get; private set; }

			[field: SerializeField]
			public int StartAmount { get; private set; } 
		}
	}
}