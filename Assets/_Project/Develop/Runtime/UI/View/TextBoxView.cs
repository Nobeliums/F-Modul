using _Project.Develop.Runtime.UI.View;
using TMPro;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.View
{
	public class TextBoxView : MonoBehaviour, IView
	{
		[SerializeField]
		private TMP_Text _text;
		
		public void SetText(string text)
			=> _text.text = text;
	}
}

