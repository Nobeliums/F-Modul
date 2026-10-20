using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
	public static class MainMenuContextRegistrations
	{
		public static void Process(DIContainer container)
		{
			Debug.Log("[MainMenuContextRegistrations] Process");
		}
	}
}