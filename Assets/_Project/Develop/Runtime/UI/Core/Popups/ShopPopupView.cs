using _Project.Develop.Runtime.UI.View;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Core.Popups
{
	public class ShopPopupView : PopupBaseView
	{
		public const string HelloMessage = "Choose item to buy";
		
		[SerializeField] private TMP_Text _titleText;
		[SerializeField] private ButtonListView _buttonListView;
		[SerializeField] private TextBoxView _infoBoxView;
		
		public ButtonListView ButtonListView => _buttonListView;
		
		public void SetTitle(string title) => _titleText.text = title;
		
		public void SetInfoBoxMessage(string text) =>  _infoBoxView.SetText(text);
	}
}