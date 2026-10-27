using System;
using _Project.Develop.Runtime.Meta.Features.ShopManagement;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.Presenters;
using _Project.Develop.Runtime.UI.View;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.UI.Core.MainMenu
{
	public class ShopItemButtonPresenter : IPresenter
	{
		public event Action<ShopItemType> NotEnoughCurrency;
		public event Action<ShopItemType> SuccessBuyItem;
		
		private Action _shopAction;
		
		private ButtonView _buttonView;

		private ShopService _shopService;
		private ShopItemType _itemType;

		public ShopItemButtonPresenter(Action shopAction,
			ButtonView buttonView,
			ShopService shopService,
			ShopItemType itemType)
		{
			_shopAction = shopAction;
			_buttonView = buttonView;
			_shopService = shopService;
			_itemType = itemType;
		}
		
		public ButtonView ButtonView => _buttonView;
		
		public void Initialize()
		{
			_buttonView.SetText(ShopItemNames.ShopItemTypeToName[_itemType]);
			_buttonView.SetAvailable(true);
			_buttonView.SetAvailableColor();
			_buttonView.ClickEvent += TryBuyItem;
		}

		public void Dispose()
		{
			_buttonView.ClickEvent -= TryBuyItem;
		}
		
		private void TryBuyItem()
		{
			bool isBuyed = _shopService.TryBuyShopItem(_itemType, _shopAction);

			if (isBuyed)
				SuccessBuyItem?.Invoke(_itemType);
			else
				NotEnoughCurrency?.Invoke(_itemType);
		}
	}
}