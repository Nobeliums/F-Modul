using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Utilities.ResourcesManagement;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono
{
	public class MonoEntitiesFactory : IInitializable, IDisposable
	{
		private readonly ResourceLoadService _loadService;
		
		private readonly EntitiesLifeContext _entitiesLifeContext;

		private readonly Dictionary<Entity, MonoEntity> _entityToMono = new();

		public MonoEntitiesFactory(ResourceLoadService loadService, EntitiesLifeContext entitiesLifeContext)
		{
			_loadService = loadService;
			_entitiesLifeContext = entitiesLifeContext;
		}

		public MonoEntity Create(Entity entity, Vector3 position, string path)
		{
			MonoEntity prefab = _loadService.Load<MonoEntity>(path);

			MonoEntity instance = Object.Instantiate(prefab, position, Quaternion.identity, null);
			
			instance.Setup(entity);

			return instance;
		}
		
		public void Initialize()
		{
			_entitiesLifeContext.Released += OnEntityRealesed;
		}

		private void OnEntityRealesed(Entity entity)
		{

			
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