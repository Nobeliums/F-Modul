using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Utilities.MainMenuCycle;
using _Project.Develop.Runtime.Meta.Utilities.ShopManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
	public static class MainMenuContextRegistrations
	{
		public static void Process(DIContainer container)
		{
			Debug.Log("[MainMenuContextRegistrations] Process");

			container.RegistryAsSingle(CreateMainMenuCycle);
			container.RegistryAsSingle(CreateShopService);
		}
		
		private static ShopService CreateShopService(DIContainer c)
			=> new ShopService(c);

		private static MainMenuCycle CreateMainMenuCycle(DIContainer c)
			=> new MainMenuCycle(c);
	}
}