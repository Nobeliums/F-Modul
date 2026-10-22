using _Project.Develop.Runtime.Gameplay.Utilities.WalletService;

namespace _Project.Develop.Runtime.Meta.Utilities.ShopManagement
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