using System.Collections.Generic;

namespace _Project.Develop.Runtime.Meta.Features.ShopManagement
{
	public static class ShopItemNames
	{
		private static Dictionary<ShopItemType, string> _shopItems = new()
		{
			{ ShopItemType.ResetStatistics, "Reset Statistics" },
		};
		
		public static IReadOnlyDictionary<ShopItemType, string> ShopItemTypeToName => _shopItems;
	}
}