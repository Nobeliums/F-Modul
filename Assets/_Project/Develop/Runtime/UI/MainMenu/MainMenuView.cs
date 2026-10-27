using System;
using _Project.Develop.Runtime.UI.Presenters;
using _Project.Develop.Runtime.UI.View;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.UI.Core.MainMenu
{
	public class MainMenuView : MonoBehaviour, IView
	{
		public event Action StartGameClick;
		public event Action ShopOpenButtonClick;
		
		[field: SerializeField] public IconTextListView WalletView { get; private set; }
		[field: SerializeField] public TextBoxListView GameStatisticsView { get; private set; }

		[SerializeField] private Button _startGameButton;
		[SerializeField] private Button _shopOpenButton;

		public void OnStartGameButtonClicked() => StartGameClick?.Invoke();
		public void OnShopOpenButtonClicked() => ShopOpenButtonClick?.Invoke();
	}
}