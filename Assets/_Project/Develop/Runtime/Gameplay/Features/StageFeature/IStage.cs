using System;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.StageFeature
{
	public interface IStage : IDisposable
	{
		IReadOnlyReactiveEvent Completed { get; }

		void Start();

		void Update(float deltaTime);

		void Cleanup();
	}
}
