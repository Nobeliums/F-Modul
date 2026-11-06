using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.LIfeCycle.Energy
{
	public class CurrentEnergy : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class MaxEnergy : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class PercentByMaxRegenerationEnergy : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class RegenerationEnergyInitialTime : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class RegenerationEnergyCurrentTime : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class SpendEnergyRequest : IEntityComponent
	{
		public ReactiveEvent<float> Value;
	}

	public class SpendEnergyEvent : IEntityComponent
	{
		public ReactiveEvent<float> Value;
	}

	public class RegenerationEnergyRequest : IEntityComponent
	{
		public ReactiveEvent<float> Value;
	}

	public class RegenerationEnergyEvent : IEntityComponent
	{
		public ReactiveEvent<float> Value;
	}

	public class CanSpendEnergy : IEntityComponent
	{
		public ICompositeCondition Value;
	}

	public class CanRegenerateEnergy : IEntityComponent
	{
		public ICompositeCondition Value;
	}
}