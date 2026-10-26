using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Features.Gamecycle;
using _Project.Develop.Runtime.UI.Presenters;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Core.Gameplay
{
	public class GameplayPresenter : IPresenter
	{
		private readonly GameplayView _gameplayView;
		
		private readonly GameplayCycle _gameplayCycle;
		private readonly GameplayPopupService _gameplayPopupService;
		
		private readonly List<IDisposable> _disposables = new();
		
		public GameplayPresenter(
			GameplayView gameplayView,
			GameplayCycle gameplayCycle, GameplayPopupService gameplayPopupService)
		{
			_gameplayView = gameplayView;
			_gameplayCycle = gameplayCycle;
			_gameplayPopupService = gameplayPopupService;
		}

		public void Initialize()
		{
			Debug.Log(_gameplayCycle.ToString());
			
			_disposables.Add(_gameplayCycle.GamePhrase.Subscribe(OnGamePhraseChanged));
			_disposables.Add(_gameplayCycle.PlayerInputPhrase.Subscribe(OnPlayerPhraseChanged));

			_gameplayCycle.GameEnd += OnGameEnd;
		}
		
		public void Dispose()
		{
			foreach (IDisposable disposable in _disposables)
			{
				disposable.Dispose();
			}
			
			_gameplayCycle.GameEnd -= OnGameEnd;
		}

		private void OnPlayerPhraseChanged(string oldValue, string newValue)
		{
			_gameplayView.SetPlayerPhrase(newValue);
		}

		private void OnGamePhraseChanged(string oldValue, string newValue)
		{
			_gameplayView.SetTargetPhrase(newValue);
		}
		
		private void OnGameEnd()
		{
			_gameplayPopupService.CreateGameResultPopup();
		}
	}
}