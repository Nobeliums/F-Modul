using _Project.Develop.Runtime.Gamecycle;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.Features.CharsSelector;
using _Project.Develop.Runtime.Gameplay.Utilities.Gamecycle.Condition;
using UnityEngine;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.ConfigManagement;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
	public class GameplayContextRegistrations
	{
		public static void Process(DIContainer container, GameplayInputArgs gameplayAgrs)
		{
			Debug.Log("[GameplayContextRegistrations] Process]");

			container.RegistryAsSingle((container) => gameplayAgrs);
			container.RegistryAsSingle(CreateGameCharsSelector);
			container.RegistryAsSingle(CreateGamemode);
		}

		private static IGameCharsSelector CreateGameCharsSelector(DIContainer c)
		{
			GameModeConfig config = c.Resolve<ConfigService>().GetConfig<GameModeConfig>();

			string sequence = config.GetGameCharsBy(c.Resolve<GameplayInputArgs>().GameCharsSequenceType);

			return new GameCharsSelector(sequence);
		}

		private static Gamemode CreateGamemode(DIContainer c)
			=> new Gamemode(new AllCharsEqualsCondition(), new CharNotEqualCondition());
	}
}