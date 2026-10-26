using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Meta.Features.GameStatistics;
using _Project.Develop.Runtime.Meta.Features.ShopManagement;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.Core.MainMenu;
using _Project.Develop.Runtime.UI.View;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;

namespace _Project.Develop.Runtime.UI.Core.Popups
{
	public class ShopPopupPresenter : PopupBasePresenter
	{
		private const string NotEnoughCurrencyMessage = "Not enough currency for {0}, you have {1} {2}, you need {3}";
		private const string SuccessBuyMessage = "Buy {0}. Now you have {1} {2}";
		
		private readonly ViewFactory _viewFactory;
		private readonly ShopService _shopService;
		private readonly WalletService _walletService;
		private readonly GameStatisticsService _gameStatisticsService;
		private readonly MainMenuPresenterFactory _mainMenuPresenterFactory;
		private readonly PlayerDataProvider _playerDataProvider;

		private ShopPopupView _popupView;

		private Dictionary<ShopItemType, Action> _shopItemTypeToAction;
		private List<ShopItemButtonPresenter> _presenters = new();
		
		public ShopPopupPresenter(
			ShopPopupView popupView,
			ICoroutinePerformer coroutinePerformer,
			ViewFactory viewFactory,
			ShopService shopService,
			GameStatisticsService gameStatisticsService,
			MainMenuPresenterFactory mainMenuPresenterFactory,
			Dictionary<ShopItemType, Action> shopItemTypeToAction,
			WalletService walletService,
			PlayerDataProvider dataProvider) : base(coroutinePerformer)
		{
			_viewFactory = viewFactory;
			_shopService = shopService;
			_gameStatisticsService = gameStatisticsService;
			_mainMenuPresenterFactory = mainMenuPresenterFactory;
			_shopItemTypeToAction = shopItemTypeToAction;
			_walletService = walletService;
			_popupView = popupView;
			_playerDataProvider =  dataProvider;
		}

		protected override PopupBaseView View => _popupView;

		public override void Initialize()
		{
			base.Initialize();
			
			_popupView.SetInfoBoxMessage(ShopPopupView.HelloMessage);

			foreach (ShopItemType shopItemType in Enum.GetValues(typeof(ShopItemType)))
			{
				ButtonView view = _viewFactory.Create<ButtonView>(ViewIds.ShopItemBuyButtonView);

				ShopItemButtonPresenter presenter = _mainMenuPresenterFactory
					.CreateShopItemButtonPresenter(
						_shopItemTypeToAction[shopItemType],
						view,
						shopItemType);
				
				presenter.Initialize();
				presenter.NotEnoughCurrency += OnNotEnoughCurrency;
				presenter.SuccessBuyItem += OnSucessBuy;
				
				_popupView.ButtonListView.Add(view);
				_presenters.Add(presenter);
			}
		}

		public override void Dispose()
		{
			base.Dispose();

			foreach (ShopItemButtonPresenter presenter in _presenters)
			{
				presenter.NotEnoughCurrency -= OnNotEnoughCurrency;
				presenter.SuccessBuyItem -= OnSucessBuy;
				
				presenter.Dispose();
				_viewFactory.Release(presenter.ButtonView);
				_popupView.ButtonListView.Remove(presenter.ButtonView);
			}
		}

		private void OnSucessBuy(ShopItemType shopItemType)
		{
			_coroutinePerformer.StartPerform(_playerDataProvider.Save());

			ShopItemPrice price = _shopService.GetPriceFor(shopItemType);
			int currencyAmount = _walletService.GetCurrency(price.Currency).Value;
			
			_popupView.SetInfoBoxMessage(
				String.Format(SuccessBuyMessage,
					ShopItemNames.ShopItemTypeToName[shopItemType],
					currencyAmount,
					price.Currency.ToString()));
		}

		private void OnNotEnoughCurrency(ShopItemType shopItemType)
		{
			ShopItemPrice itemPrice = _shopService.GetPriceFor(shopItemType);
			int currencyAmount = _walletService.GetCurrency(itemPrice.Currency).Value;
			
			_popupView.SetInfoBoxMessage(String.Format(
				NotEnoughCurrencyMessage,
				ShopItemNames.ShopItemTypeToName[shopItemType],
				currencyAmount,
				itemPrice.Currency.ToString(),
				itemPrice.Price.ToString()
				));
		}
	}
}