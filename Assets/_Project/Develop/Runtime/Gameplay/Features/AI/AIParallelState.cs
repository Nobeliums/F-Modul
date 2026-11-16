using _Project.Develop.Runtime.Utilities.StateMachineCore;

namespace _Project.Develop.Runtime.Gameplay.Features.AI
{
	public class AIParallelState : ParallelState<IUpdatebleState>, IUpdatebleState
	{
		public AIParallelState(params IUpdatebleState[] states) : base(states) { }

		public void Update(float deltaTime)
		{
			foreach (IUpdatebleState state in States)
			{
				state.Update(deltaTime);
			}
		}
	}
}