using _Project.Develop.Runtime.UI.View;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Core.Gameplay
{
	public class GameplayView : MonoBehaviour, IView
	{
		[SerializeField] private TextBoxView _targetPhrase;
		[SerializeField] private TextBoxView _playerPhrase;
		
		public void SetTargetPhrase(string targetPhrase) =>  _targetPhrase.SetText(targetPhrase);
		public void SetPlayerPhrase(string playerPhrase) =>  _playerPhrase.SetText(playerPhrase);
	}
}