using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Utilities.WalletService;
using _Project.Develop.Runtime.Meta.Utilities.ShopManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Configs
{
	[CreateAssetMenu(fileName = "ShopConfig", menuName = "Project/ShopConfig")]
	public class ShopConfig : ScriptableObject
	{
		[field: SerializeField]
		private List<ShopConfigItem> ShopItemsCost { get; set; }

		public ShopItemPrice GetItemPrice(ShopItemType shopItemType)
		{
			ShopConfigItem item = ShopItemsCost.Find(x => x.ShopItemType == shopItemType);
			
			return new ShopItemPrice(item.Cost, item.Currency);
		}

		[Serializable]
		private class ShopConfigItem
		{
			[field: SerializeField]
			public ShopItemType ShopItemType { get; private set; }

			[field: SerializeField]
			public int Cost { get; private set; }
			
			[field: SerializeField]
			public WalletCurrencyType Currency { get; private set; }
		}
	}
}