using _Project.Develop.Runtime.Gameplay.Utilities.Gamemode.Condition;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay
{
	public class Gamemode
	{
		private bool _isStarted;

		private string _gamePhrase;
		private string _playerInputPhrase = "";

		private ICondition _winCondition;
		private ICondition _loseCondition;

		public Gamemode(ICondition winCondition, ICondition loseCondition)
		{
			_winCondition =  winCondition;
			_loseCondition = loseCondition;
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
				_playerInputPhrase += Input.inputString;
				Debug.Log(_playerInputPhrase);
			}
			
			_winCondition.CheckCondition(_gamePhrase,  _playerInputPhrase);
			_loseCondition.CheckCondition(_gamePhrase, _playerInputPhrase);
		}

		private void Win()
		{
			Debug.Log("Gamemode won!");
			_isStarted = false;
		}

		private void Lose()
		{
			Debug.Log("Gamemode lost!");
			_isStarted = false;
		}
	}
}