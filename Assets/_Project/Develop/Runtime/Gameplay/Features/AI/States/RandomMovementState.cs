using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeuture;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
	public class RandomMovementState : State, IUpdatebleState
	{
		private ReactiveVariable<Vector3> _moveDirection;
		private ReactiveVariable<Vector3> _rotationDirection;

		private float _cooldownBetweenDirectionGeneration;

		private float _currentTime;

		private float _randomDegree;

		public RandomMovementState(
			Entity entity,
			float cooldownBetweenDirectionGeneration,
			float randomDegree)
		{
			_moveDirection = entity.GetComponent<MoveDirection>().Value;
			_rotationDirection = entity.GetComponent<RotateDirection>().Value;

			_cooldownBetweenDirectionGeneration = cooldownBetweenDirectionGeneration;
			_randomDegree = randomDegree;
		}

		public void Update(float deltaTime)
		{
			_currentTime += deltaTime;

			if (_currentTime >= _cooldownBetweenDirectionGeneration)
			{
				GenerateNewDirection();
				_currentTime = 0f;
			}
		}

		private void GenerateNewDirection()
		{
			Vector3 inverseDirection = -_moveDirection.Value;
			
			Quaternion randomTurn = Quaternion.Euler(0f, Random.Range(-_randomDegree, _randomDegree), 0f);
			
			Vector3 newDirection = randomTurn * inverseDirection;
			
			_moveDirection.Value = newDirection;
			_rotationDirection.Value = newDirection;
		}

		public override void Enter()
		{
			base.Enter();
			
			Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
			_moveDirection.Value = randomDirection;
			_rotationDirection.Value = randomDirection;
			
			_currentTime = 0;
		}

		public override void Exit()
		{
			base.Exit();
			
			_moveDirection.Value = Vector3.zero;
		}
	}
}