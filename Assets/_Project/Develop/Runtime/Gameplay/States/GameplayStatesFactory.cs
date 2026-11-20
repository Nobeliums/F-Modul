using _Project.Develop.Runtime.Gameplay.Configs.Stages;
using _Project.Develop.Runtime.Gameplay.Features.MainHero;
using _Project.Develop.Runtime.Gameplay.Features.StageFeature;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.LevelsProgression;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using _Project.Develop.Runtime.Utilities.Input;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.Gameplay.States
{
	public class GameplayStatesFactory
	{
		private readonly DIContainer _container;

		public GameplayStatesFactory(DIContainer container)
		{
			_container = container;
		}

		public PreparationState CreatePreparationState()
		{
			return new PreparationState(_container.Resolve<PreparationTriggerService>());
		}

		public StageProcessState CreateStageProcessState()
		{
			return new StageProcessState(_container.Resolve<StageProviderService>());
		}

		public WinState CreateWinState(GameplayInputArgs args)
		{
			return new WinState(
				_container.Resolve<IInputService>(),
				_container.Resolve<LevelsProgressionService>(),
				args,
				_container.Resolve<PlayerDataProvider>(),
				_container.Resolve<SceneSwitcherService>(),
				_container.Resolve<ICoroutinePerformer>());
		}

		public GameplayStateMachine CreateCoreLoopState()
		{
			GameplayStateMachine stateMachine = new GameplayStateMachine();

			PreparationState preparationState = CreatePreparationState();
			StageProcessState stageProcessState = CreateStageProcessState();

			stateMachine.AddState(preparationState);
			stateMachine.AddState(stageProcessState);

			PreparationTriggerService preparationTriggerService = _container.Resolve<PreparationTriggerService>();
			StageProviderService stageProviderService = _container.Resolve<StageProviderService>();

			ICompositeCondition toStageProcessCondition = new CompositeCondition();
			toStageProcessCondition.AddCondition(new FuncCondition(() => preparationTriggerService.HasMainHeroContact.Value));
			toStageProcessCondition.AddCondition(new FuncCondition(() => stageProviderService.HasNextStage));

			ICondition toPreparationCondition = new FuncCondition(() => stageProviderService.CurrentStageResult.Value == StageResults.Completed);
			
			stateMachine.AddTransition(preparationState, stageProcessState, toStageProcessCondition);
			stateMachine.AddTransition(stageProcessState, preparationState, toPreparationCondition);

			return stateMachine;
		}

		public DefeatState CreateDefeatState()
		{
			return new DefeatState(
				_container.Resolve<IInputService>(),
				_container.Resolve<SceneSwitcherService>(),
				_container.Resolve<ICoroutinePerformer>());
		}

		public GameplayStateMachine CreateGameplayStateMachine(GameplayInputArgs args)
		{
			GameplayStateMachine stateMachine = new GameplayStateMachine();

			GameplayStateMachine coreLoopState = CreateCoreLoopState();
			WinState winState = CreateWinState(args);
			DefeatState defeatState = CreateDefeatState();

			stateMachine.AddState(coreLoopState);
			stateMachine.AddState(winState);
			stateMachine.AddState(defeatState);

			StageProviderService stageProviderService = _container.Resolve<StageProviderService>();
			PreparationTriggerService preparationTriggerService = _container.Resolve<PreparationTriggerService>();

			ICompositeCondition toWinCondition = new CompositeCondition();
			toWinCondition.AddCondition(new FuncCondition(() => preparationTriggerService.HasMainHeroContact.Value));
			toWinCondition.AddCondition(new FuncCondition(() => stageProviderService.CurrentStageResult.Value == StageResults.Completed));
			toWinCondition.AddCondition(new FuncCondition(() => stageProviderService.HasNextStage == false));

			stateMachine.AddTransition(coreLoopState, winState, toWinCondition);

			MainHeroHolderService mainHeroHolderService = _container.Resolve<MainHeroHolderService>();

			ICondition toDefeatCondition = new FuncCondition(() =>
				mainHeroHolderService.MainHero != null && mainHeroHolderService.MainHero.IsDead.Value);

			stateMachine.AddTransition(coreLoopState, defeatState, toDefeatCondition);

			return stateMachine;
		}
	}
}
