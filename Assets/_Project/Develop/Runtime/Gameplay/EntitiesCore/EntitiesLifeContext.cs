using System;
using System.Collections.Generic;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore
{
	public class EntitiesLifeContext : IDisposable
	{
		public event Action<Entity> Added;
		public event Action<Entity> Released;
		
		private readonly List<Entity> _entities = new();
		private readonly List<Entity> _removeRequest = new();

		public void Add(Entity entity)
		{
			_entities.Add(entity);
			entity.Initialize();
			Added?.Invoke(entity);
		}
		
		public void Update(float dt)
		{
			for (int i = 0; i < _entities.Count; i++)
				_entities[i].OnUpdate(dt);

			foreach (Entity entity in _removeRequest)
			{
				_entities.Remove(entity);
				entity.Dispose();
				Released?.Invoke(entity);
			}
			
			_removeRequest.Clear();
		}

		public void Release(Entity entity)
		{
			_removeRequest.Add(entity);
		}

		public void Dispose()
		{
			foreach (Entity entity in _entities)
			{
				entity.Dispose();
			}
			
			_entities.Clear();
			_removeRequest.Clear();
		}
	}
}