using System;
using _Project.Develop.Runtime.Gameplay.Configs.Levels;
using _Project.Develop.Runtime.Gameplay.Configs.Stages;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.StageFeature
{
	public class StageProviderService : IDisposable
	{
		private ReactiveVariable<int> _currentStageNumber = new();
		private ReactiveVariable<StageResults> _currentStageResult = new();

		private LevelConfig _levelConfig;
		private StageFactory _stageFactory;

		private IStage _currentStage;

		public StageProviderService(LevelConfig levelConfig, StageFactory stageFactory)
		{
			_levelConfig = levelConfig;
			_stageFactory = stageFactory;
		}

		public IReadOnlyReactiveVariable<int> CurrentStageNumber => _currentStageNumber;

		public IReadOnlyReactiveVariable<StageResults> CurrentStageResult => _currentStageResult;

		private IDisposable _currentStageDisposable;

		public int StageCount => _levelConfig.Stages.Count;

		public bool HasNextStage => _currentStageNumber.Value < StageCount;

		public void StartCurrent()
		{
			_currentStageDisposable = _currentStage.Completed.Subscribe(OnStageCompleted);
			_currentStage.Start();
		}

		private void OnStageCompleted()
		{
			_currentStageResult.Value = StageResults.Completed;
		}

		public void UpdateCurrent(float deltaTime)
		{
			_currentStage?.Update(deltaTime);
		}

		public void CleanupCurrent()
		{
			_currentStage?.Cleanup();
		}
		
		public void SwitchToNext()
		{
			if (_currentStage != null)
				CleanupCurrent();

			_currentStageNumber.Value++;
			_currentStageResult.Value = StageResults.Uncompleted;

			_currentStage = _stageFactory.Create(_levelConfig.Stages[_currentStageNumber.Value - 1]);
		}

		public void Dispose()
		{
			_currentStage?.Dispose();
			_currentStageDisposable?.Dispose();
		}
	}
}
