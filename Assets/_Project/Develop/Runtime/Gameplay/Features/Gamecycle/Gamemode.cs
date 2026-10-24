using System;
using _Project.Develop.Runtime.Gameplay.Features.Gamecycle.Condition;
using _Project.Develop.Runtime.Utilities.Input;
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

		private string _gamePhrase;
		private string _playerInputPhrase = "";

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

		public void Run()
		{
			_isStarted = true;
			_winCondition.ConditionCompletedEvent += Win;
			_loseCondition.ConditionCompletedEvent += Lose;
			
			Debug.Log($"{_gamePhrase} - input this phrase");
		}

		public void Initialize(string gamePhrase)
		{
			_isStarted = true;
			_gamePhrase = gamePhrase;
		}

		public void ProcessUpdate()
		{
			if (_isStarted == false)
				return;

			if (Input.anyKeyDown)
			{
				_playerInputPhrase += InputService.GetPressedKeys();
				Debug.Log($"Need input this phrase: " + _gamePhrase);
				Debug.Log($"Your phrase is: " + _playerInputPhrase);
			}

			_winCondition.CheckCondition(_gamePhrase,  _playerInputPhrase);
			_loseCondition.CheckCondition(_gamePhrase, _playerInputPhrase);
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
