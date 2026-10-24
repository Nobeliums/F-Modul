using _Project.Develop.Runtime.Gameplay.Features.WalletService;

namespace _Project.Develop.Runtime.Meta.Features.ShopManagement
{
	public struct ShopItemPrice
	{
		public ShopItemPrice(int price, WalletCurrencyType currency)
		{
			Price = price;
			Currency = currency;
		}

		public int Price { get; }
		public WalletCurrencyType Currency { get; }
	}
}