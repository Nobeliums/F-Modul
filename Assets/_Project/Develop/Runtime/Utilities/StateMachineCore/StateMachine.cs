using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Gameplay.Features.Gamecycle.Condition;
using ICondition = _Project.Develop.Runtime.Utilities.Conditions.ICondition;

namespace _Project.Develop.Runtime.Utilities.StateMachineCore
{
	public abstract class StateMachine<TState> : State, IUpdatebleState, IDisposable where TState : class, IState
	{
		private List<StateNode<TState>> _states = new();
		
		private StateNode<TState> _currentState;

		private bool _isRunning;
		
		private List<IDisposable> _disposables;

		protected StateMachine(List<IDisposable> disposables)
		{
			_disposables = disposables;
		}

		protected TState CurrentState => _currentState.State;

		public void Update(float deltaTime)
		{
			if (_isRunning == false)
				return;
			
			foreach (StateTransition<TState> transition in _currentState.Transitions)
			{
				if (transition.Condition.Evaluate())
				{
					SwitchState(transition.ToState);
					break;
				}
			}
			
			UpdateLogic(deltaTime);
		}

		public void AddState(TState state)
		{
			_states.Add(new StateNode<TState>(state));
		}

		public void AddTransition(TState fromState, TState toState, ICondition condition)
		{
			StateNode<TState> from = _states.First(stateNode => stateNode.State == fromState);
			StateNode<TState> to = _states.First(stateNode => stateNode.State == toState);
			
			from.AddTransition(new StateTransition<TState>(to, condition));
		}

		public override void Enter()
		{
			base.Enter();
			
			if (_currentState == null)
				SwitchState(_states[0]);
			else
				_currentState.State.Enter();
			
			
			_isRunning = true;
		}

		public override void Exit()
		{
			base.Exit();
			
			_currentState?.State.Exit();
			
			_isRunning = false;
		}

		public void Dispose()
		{
			_isRunning = false;

			foreach (StateNode<TState> state in _states)
			{
				if (state.State is IDisposable stateDisposable)
					stateDisposable.Dispose();
			}

			foreach (IDisposable disposable in _disposables)
			{
				disposable.Dispose();
			}
			
			_disposables.Clear();
		}

		protected virtual void UpdateLogic(float deltaTime)
		{
			
		}

		private void SwitchState(StateNode<TState> nextState)
		{
			_currentState?.State.Exit();
			_currentState = nextState;
			_currentState.State.Enter();
		}
	}
}