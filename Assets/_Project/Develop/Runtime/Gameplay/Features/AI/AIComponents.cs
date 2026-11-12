using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.AI
{
	public class CurrentTarget : IEntityComponent
	{
		public ReactiveVariable<Entity> Value;
	}
}