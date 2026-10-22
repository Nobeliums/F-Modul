using System;
using _Project.Develop.Runtime.Gameplay.Utilities.WalletService;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Configs;
using _Project.Develop.Runtime.Utilities.ConfigManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Utilities.ShopManagement
{
	public class ShopService
	{
		private DIContainer _container;
		private WalletService _walletService;
		private ShopConfig _config;
		
		public ShopService(DIContainer container)
		{
			_container = container;
			_walletService = _container.Resolve<WalletService>();
			_config = _container.Resolve<ConfigService>().GetConfig<ShopConfig>();
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