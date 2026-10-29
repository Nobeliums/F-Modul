using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		private readonly Dictionary<Type, IEntityComponent> _components = new();

		private readonly List<IEntitySystem> _systems = new();

		private readonly List<IUpdatableSystem> _updatables = new();
		private readonly List<IInitializableSystem> _initializables = new();
		private readonly List<IDisposableSystem> _disposables = new();

		private bool _isInit;

		public void Initialize()
		{
			foreach (IInitializableSystem initializableSystem in _initializables)
				initializableSystem.OnInit(this);
			
			_isInit = true;
		}

		public void OnUpdate(float dt)
		{
			if (_isInit == false)
				return;
			
			foreach (IUpdatableSystem updatableSystem in _updatables)
				updatableSystem.OnUpdate(dt);
		}

		public void Dispose()
		{
			foreach (IDisposableSystem disposableSystem in _disposables)
				disposableSystem.OnDispose();
		}
		
		public Entity AddComponent<TComponent>(TComponent component) where TComponent : class, IEntityComponent
		{
			_components.Add(typeof(TComponent), component);
			return this;
		}

		public bool HasComponent<TComponent>() where TComponent : class, IEntityComponent
		{
			return _components.ContainsKey(typeof(TComponent));
		}

		public bool TryGetComponent<TComponent>(out TComponent component) where TComponent : class, IEntityComponent
		{
			if (_components.TryGetValue(typeof(TComponent), out IEntityComponent componentObj))
			{
				component = (TComponent)componentObj;
				return true;
			}
			
			component = null;
			return false;
		}

		public TComponent GetComponent<TComponent>() where TComponent : class, IEntityComponent
		{
			if (TryGetComponent(out TComponent component))
			{
				return component;
			}
			
			throw new ArgumentException($"Component of type {typeof(TComponent)} not found");
		}
		
		public Entity AddSystem(IEntitySystem system)
		{
			if (_systems.Contains(system))
			{
				throw new ArgumentException($"System {system.GetType().Name} is already registered");
			}
			
			_systems.Add(system);
			
			if (system is IInitializableSystem initializableSystem)
			{
				_initializables.Add(initializableSystem);

				if (_isInit)
					initializableSystem.OnInit(this);
			}

			if (system is IUpdatableSystem updatableSystem)
			{
				_updatables.Add(updatableSystem);
			}
			
			if (system is IDisposableSystem disposableSystem)
			{
				_disposables.Add(disposableSystem);
			}

			return this;
		}

		public void RemoveSystem(IEntitySystem system)
		{
			
		}
	}
}