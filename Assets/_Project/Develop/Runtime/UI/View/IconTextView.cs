using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.UI.View
{
	public class IconTextView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _text;
		[SerializeField] private Image _sprite;
		
		public void SetText(string text)
			=> _text.text = text;
		
		public void SetSprite(Sprite sprite)
			=> _sprite.sprite = sprite;
	}
}