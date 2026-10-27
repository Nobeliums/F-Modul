using System;
using _Project.Develop.Runtime.UI.View;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.UI.Core
{
	public class PopupBaseView : MonoBehaviour, IShowableView
	{
		public event Action CloseRequest;
		
		[SerializeField] private CanvasGroup _group;
		[SerializeField] private Button _closeButton;
		
		private Sequence _animationSequence;

		private void Awake()
		{
			_group.alpha = 0.0f;
		}

		public Tween Show()
		{
			OnPreShow();

			_group.alpha = 1.0f;
			
			_animationSequence =  DOTween.Sequence();

			OnPostShow();

			return _animationSequence;
		}
		
		public Tween Hide()
		{
			OnPreHide();
	
			_group.alpha = 0.0f;
			
			_animationSequence =  DOTween.Sequence();
	
			OnPostHide();
			
			return _animationSequence;
		}

		public void OnCloseButtonClick() => CloseRequest?.Invoke();

		protected virtual void OnPreShow()
		{
			
		}

		protected virtual void OnPostShow()
		{
			
		}

		protected virtual void OnPreHide()
		{
			
		}

		protected virtual void OnPostHide()
		{
			
		}
	}
}