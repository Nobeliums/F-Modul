using _Project.Develop.Runtime.Gamecycle;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.Features.CharsSelector;
using _Project.Develop.Runtime.Gameplay.Features.Gamecycle.Condition;
using _Project.Develop.Runtime.Gameplay.Features.WalletService;
using UnityEngine;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.GameStatistics;
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
			container.RegistryAsSingle(CreateGameplayCycle);
		}

		private static IGameCharsSelector CreateGameCharsSelector(DIContainer c)
		{
			GameModeConfig config = c.Resolve<ConfigService>().GetConfig<GameModeConfig>();

			string sequence = config.GetGameCharsBy(c.Resolve<GameplayInputArgs>().GameCharsSequenceType);

			return new GameCharsSelector(sequence);
		}

		private static GameplayCycle CreateGameplayCycle(DIContainer c)
		{
			IGameCharsSelector charsSelector = c.Resolve<IGameCharsSelector>();
			ConfigService configService = c.Resolve<ConfigService>();
			GameModeConfig config = configService.GetConfig<GameModeConfig>();
			GameStatisticsService gameStatisticsService = c.Resolve<GameStatisticsService>();
			WalletService walletService = c.Resolve<WalletService>();
	
			Gamemode gamemode = new Gamemode(new AllCharsEqualsCondition(), new CharNotEqualCondition());

			return new GameplayCycle(gamemode, charsSelector, config, gameStatisticsService, walletService);
		}
	}
}