using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Configs.Stages;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.Enemies;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.StageFeature
{
	public class ClearAllEnemiesStage : IStage
	{
		private ClearAllEnemiesStageConfig _config;
		private EnemiesFactory _enemiesFactory;
		private EntitiesLifeContext _entitiesLifeContext;

		private ReactiveEvent _completed;

		private bool _inProcess;

		private Dictionary<Entity, IDisposable> _enemies = new();

		public ClearAllEnemiesStage(ClearAllEnemiesStageConfig config, EnemiesFactory enemiesFactory, EntitiesLifeContext entitiesLifeContext)
		{
			_config = config;
			_enemiesFactory = enemiesFactory;
			_entitiesLifeContext = entitiesLifeContext;
			_completed = new ReactiveEvent();
		}

		public IReadOnlyReactiveEvent Completed => _completed;

		public void Start()
		{
			if (_inProcess)
				throw new InvalidOperationException("Game mode is already started");

			SpawnEnemies();
			_inProcess = true;
		}

		public void Update(float deltaTime)
		{
			if (_inProcess == false)
				return;
			
			if (_enemies.Count == 0)
				ProcessEnd();
		}

		public void Cleanup()
		{
			foreach (KeyValuePair<Entity, IDisposable> pair in _enemies)
			{
				pair.Value.Dispose();
				_entitiesLifeContext.Release(pair.Key);
			}

			_enemies.Clear();
			_inProcess = false;
		}

		private void ProcessEnd()
		{
			_inProcess = false;
			_completed?.Invoke();
		}

		private void SpawnEnemies()
		{
			foreach (EnemyItemConfig enemyItem in _config.Enemies)
				SpawnEnemy(enemyItem);
		}

		private void SpawnEnemy(EnemyItemConfig enemyItem)
		{
			Entity spawnedEnemy = _enemiesFactory.Create(enemyItem.SpawnPosition, enemyItem.EntityConfig);

			IDisposable subscription = spawnedEnemy.IsDead.Subscribe((oldValue, isDead) =>
			{
				if (isDead)
				{
					_enemies[spawnedEnemy].Dispose();
					_enemies.Remove(spawnedEnemy);
				}
			});

			_enemies.Add(spawnedEnemy, subscription);
		}

		public void Dispose()
		{
			foreach (KeyValuePair<Entity, IDisposable> pair in _enemies)
				pair.Value.Dispose();

			_enemies.Clear();
			_inProcess = false;
		}
	}
}
