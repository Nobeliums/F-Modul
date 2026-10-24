using System;
using _Project.Develop.Runtime.Gameplay.Features.WalletService;
using _Project.Develop.Runtime.Meta.Configs;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Features.ShopManagement
{
	public class ShopService
	{
		private WalletService _walletService;
		private ShopConfig _config;
		
		public ShopService(WalletService walletService,  ShopConfig config)
		{
			_walletService = walletService;
			_config = config;
		}

		public bool TryBuyShopItem(ShopItemType shopItemType, Action callback)
		{
			ShopItemPrice itemPrice = _config.GetItemPrice(shopItemType);

			if (_walletService.IsEnough(itemPrice.Price, itemPrice.Currency))
			{
				_walletService.Debit(itemPrice.Price, itemPrice.Currency);

				callback?.Invoke();
				Debug.Log($"Успешно куплен {shopItemType} за {itemPrice.Price} {itemPrice.Currency}");
				return true;
			}
			
			Debug.Log($"У вас не хватает {itemPrice.Currency}, нужно: {itemPrice.Price}, у вас {_walletService.GetCurrency(itemPrice.Currency).Value}");

			return false;
		}
	}
}