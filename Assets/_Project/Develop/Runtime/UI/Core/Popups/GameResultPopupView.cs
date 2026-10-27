using _Project.Develop.Runtime.UI.View;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Core.Popups
{
	public class GameResultPopupView : PopupBaseView
	{
		[SerializeField] private TMP_Text _titleText;
		[SerializeField] private TextBoxView _gameResult;
		[SerializeField] private ButtonView _gameEndButton;
		
		public ButtonView GameEndButton => _gameEndButton;
		
		public void SetTitle(string title) => _titleText.text = title;
		public void SetGameResult(string title) => _gameResult.SetText(title);
	}
}