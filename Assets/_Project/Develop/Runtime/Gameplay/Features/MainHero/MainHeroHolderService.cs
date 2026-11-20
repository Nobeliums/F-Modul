using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.MainHero
{
	public class MainHeroHolderService : IInitializable, IDisposable
	{
		private readonly EntitiesLifeContext _entitiesLifeContext;
		private readonly ReactiveEvent<Entity> _heroRegistred = new();

		private Entity _mainHero;

		public MainHeroHolderService(EntitiesLifeContext entitiesLifeContext)
		{
			_entitiesLifeContext = entitiesLifeContext;
		}

		public IReadOnlyReactiveEvent<Entity> HeroRegistred => _heroRegistred;

		public Entity MainHero => _mainHero;

		public void Initialize()
		{
			_entitiesLifeContext.Added += OnEntityAdded;
		}

		public void Dispose()
		{
			_entitiesLifeContext.Added -= OnEntityAdded;
		}

		private void OnEntityAdded(Entity entity)
		{
			if (entity.HasComponent<IsMainHero>() == false)
				return;

			_entitiesLifeContext.Added -= OnEntityAdded;
			_mainHero = entity;
			_heroRegistred.Invoke(_mainHero);
		}
	}
}
