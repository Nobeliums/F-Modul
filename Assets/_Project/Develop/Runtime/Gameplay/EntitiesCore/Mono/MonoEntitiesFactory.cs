using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.ResourcesManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono
{
	public class MonoEntitiesFactory : IInitializable, IDisposable
	{
		private readonly ResourceLoadService _loadService;
		
		private readonly EntitiesLifeContext _entitiesLifeContext;

		private readonly CollidersRegistryService _collidersRegistryService;

		private readonly Dictionary<Entity, MonoEntity> _entityToMono = new();

		public MonoEntitiesFactory(
			ResourceLoadService loadService,
			EntitiesLifeContext entitiesLifeContext,
			CollidersRegistryService collidersRegistryService)
		{
			_loadService = loadService;
			_entitiesLifeContext = entitiesLifeContext;
			_collidersRegistryService = collidersRegistryService;
		}

		public MonoEntity Create(Entity entity, Vector3 position, string path)
		{
			MonoEntity prefab = _loadService.Load<MonoEntity>(path);

			MonoEntity instance = Object.Instantiate(prefab, position, Quaternion.identity, null);
			
			instance.Initialize(_collidersRegistryService);
			
			instance.Link(entity);
			
			_entityToMono.Add(entity, instance);

			return instance;
		}
		
		public void Initialize()
		{
			_entitiesLifeContext.Released += OnEntityRealesed;
		}

		private void OnEntityRealesed(Entity entity)
		{
			CleanUpFor(entity);

			_entityToMono.Remove(entity);
		}

		public void Dispose()
		{
			_entitiesLifeContext.Released -= OnEntityRealesed;
			
			foreach (Entity entity in _entityToMono.Keys)
				CleanUpFor(entity);
			
			_entityToMono.Clear();
		}

		private void CleanUpFor(Entity entity)
		{
			MonoEntity monoEntity = _entityToMono[entity];
			monoEntity.Cleanup(entity);
			Object.Destroy(monoEntity.gameObject);
		}
	}
}