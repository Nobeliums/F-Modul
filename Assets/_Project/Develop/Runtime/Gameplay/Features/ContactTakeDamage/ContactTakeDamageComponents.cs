using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.ContactTakeDamage
{
	public class BodyContactDamage : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}
}