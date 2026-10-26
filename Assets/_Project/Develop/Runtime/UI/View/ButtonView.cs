using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.UI.View
{
	public class ButtonView : MonoBehaviour, IView
	{
		public event Action ClickEvent;
		
		[SerializeField] private TMP_Text _buttonText;
		[SerializeField] private Button _button;
		
		public void SetText(string text) => _buttonText.text = text;

		public void OnEnable()
		{
			_button.onClick.AddListener(OnClicked);
		}

		public void OnDisable()
		{
			_button.onClick.RemoveListener(OnClicked);
		}

		public void OnClicked() => ClickEvent?.Invoke();
	}
}