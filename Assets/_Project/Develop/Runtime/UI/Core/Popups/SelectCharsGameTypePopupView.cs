using _Project.Develop.Runtime.UI.View;
using TMPro;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Core.Popups
{
	public class SelectCharsGameTypePopupView : PopupBaseView
	{
		[SerializeField] private TMP_Text _titleText;
		[SerializeField] private ButtonListView _buttonListView;
		
		public ButtonListView ButtonListView => _buttonListView;
		
		public void SetTitle(string newTitle) =>  _titleText.text = newTitle;
	}
}