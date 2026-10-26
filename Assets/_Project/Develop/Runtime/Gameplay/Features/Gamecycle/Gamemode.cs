using System;
using _Project.Develop.Runtime.Gameplay.Features.Gamecycle.Condition;
using _Project.Develop.Runtime.Utilities.Input;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Features.Gamecycle
{
	public class Gamemode : IDisposable
	{
		public event Action WinEvent;
		public event Action LoseEvent;
		
		private bool _isStarted;
		private bool _isWin;
		private bool _isLose;

		private ReactiveVabiable<string> _gamePhrase = new();
		private ReactiveVabiable<string> _playerInputPhrase = new();

		private ICondition _winCondition;
		private ICondition _loseCondition;
		
		public bool IsStarted => _isStarted;
		public bool IsWin => _isWin;
		public bool IsLose => _isLose;

		public Gamemode(ICondition winCondition, ICondition loseCondition)
		{
			_winCondition =  winCondition;
			_loseCondition = loseCondition;
		}
		
		public IReadOnlyReactiveVariable<string> GamePhrase => _gamePhrase;
		public IReadOnlyReactiveVariable<string> PlayerInputPhrase => _playerInputPhrase;

		public void Run()
		{
			_isStarted = true;
			_winCondition.ConditionCompletedEvent += Win;
			_loseCondition.ConditionCompletedEvent += Lose;
		}

		public void Initialize(string gamePhrase)
		{
			_isStarted = true;
			_gamePhrase.Value = gamePhrase;
			_playerInputPhrase.Value = "";
		}

		public void ProcessUpdate()
		{
			if (_isStarted == false)
				return;

			if (Input.anyKeyDown)
			{
				_playerInputPhrase.Value += InputService.GetPressedKeys();
			}

			_winCondition.CheckCondition(_gamePhrase.Value,  _playerInputPhrase.Value);
			_loseCondition.CheckCondition(_gamePhrase.Value, _playerInputPhrase.Value);
		}

		private void Win()
		{
			Debug.Log("Gamemode won! Press space to main menu");
			_isStarted = false;
			_isWin = true;
			WinEvent?.Invoke();
		}

		private void Lose()
		{
			Debug.Log("Gamemode lost! Press space to restart");
			_isStarted = false;
			_isLose = true;
			LoseEvent?.Invoke();
		}

		public void Dispose()
		{
			_winCondition.ConditionCompletedEvent -= Win;
			_loseCondition.ConditionCompletedEvent -= Lose;
		}
	}
}
