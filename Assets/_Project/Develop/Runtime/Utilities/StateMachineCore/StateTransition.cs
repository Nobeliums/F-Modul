using _Project.Develop.Runtime.Utilities.Conditions;

namespace _Project.Develop.Runtime.Utilities.StateMachineCore
{
	public class StateTransition<TState> where TState : class, IState
	{
		public StateTransition(StateNode<TState> toState, ICondition condition)
		{
			ToState = toState;
			Condition = condition;
		}

		public StateNode<TState> ToState { get; }
		
		public ICondition Condition { get; }
	}
}