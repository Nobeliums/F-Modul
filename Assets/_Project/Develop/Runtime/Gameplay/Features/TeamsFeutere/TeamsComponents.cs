using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.TeamsFeutere
{
	public class Team : IEntityComponent
	{
		public ReactiveVariable<Teams> Value;
	}
}