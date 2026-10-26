using System;
using _Project.Develop.Runtime.UI.Presenters;

namespace _Project.Develop.Runtime.UI.Core
{
	public abstract class PopupBasePresenter : IPresenter
	{
		public event Action<PopupBasePresenter> CloseRequest;
		
		protected abstract PopupBaseView View { get; }
		
		public virtual void Initialize()
		{
			
		}
		
		public virtual void Dispose()
		{
			View.CloseRequest -= OnCloseRequest;
		}

		public void Show()
		{
			OnPreShow();

			View.Show();

			OnPostShow();
		}

		public void Hide(Action callback = null)
		{
			OnPreHide();

			View.Hide();

			OnPostHide();
			
			callback?.Invoke();
		}
		
		protected virtual void OnPreShow()
		{
			View.CloseRequest += OnCloseRequest;
		}

		protected virtual void OnPostShow()
		{
			
		}

		protected virtual void OnPreHide()
		{
			View.CloseRequest -= OnCloseRequest;
		}

		protected virtual void OnPostHide()
		{
			
		}
		
		protected void OnCloseRequest() => CloseRequest?.Invoke(this);
	}
}