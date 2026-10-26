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
		[SerializeField] private Image _background;

		[SerializeField] private Color _availableColor;
		[SerializeField] private Color _unavailableColor;
		[SerializeField] private Color _activeColor;

		private bool _isAvailable;

		public void OnEnable()
		{
			_button.onClick.AddListener(OnClicked);
		}

		public void SetText(string text) => _buttonText.text = text;
		
		public void SetAvailable(bool isAvailable) => _isAvailable = isAvailable;
		
		public void SetActiveColor() => _background.color = _activeColor;

		public void SetAvailableColor() => _background.color = _availableColor;

		public void SetUnavailableColor() => _background.color = _unavailableColor;
		
		public void SetIsAvailableColor() => _background.color = _isAvailable ? _availableColor : _unavailableColor;

		public void OnDisable()
		{
			_button.onClick.RemoveListener(OnClicked);
		}

		public void OnClicked() => ClickEvent?.Invoke();
	}
}