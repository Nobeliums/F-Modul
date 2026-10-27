using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Configs
{
	[CreateAssetMenu(fileName = "StartWalletViewConfig", menuName = "Project/StartWalletViewConfig", order = 0)]
	public class StartWalletViewConfig : ScriptableObject
	{
		[field: SerializeField]
		private List<WalletConfigData> _walletConfigDatas;

		public Sprite GetViewBy(WalletCurrencyType currencyType)
			=> _walletConfigDatas.Find(x => x.CurrencyType == currencyType).Sprite;
		
		[Serializable]
		private class WalletConfigData
		{
			[field: SerializeField]
			public WalletCurrencyType CurrencyType { get; private set; }

			[field: SerializeField]
			public Sprite Sprite { get; private set; } 
		}
	}
}