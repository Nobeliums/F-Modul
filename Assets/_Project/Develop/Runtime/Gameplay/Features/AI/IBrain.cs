using System;

namespace _Project.Develop.Runtime.Gameplay.Features.AI
{
	public interface IBrain : IDisposable
	{
		void Enable();

		void Disable();

		void Update(float deltaTime);
	}
}