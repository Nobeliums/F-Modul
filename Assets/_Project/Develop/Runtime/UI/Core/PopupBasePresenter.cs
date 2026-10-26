using System;
using System.Collections;
using _Project.Develop.Runtime.UI.Presenters;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using DG.Tweening;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Core
{
	public abstract class PopupBasePresenter : IPresenter
	{
		public event Action<PopupBasePresenter> CloseRequest;
		
		protected readonly ICoroutinePerformer _coroutinePerformer;
		private Coroutine _process;

		protected PopupBasePresenter(ICoroutinePerformer coroutinePerformer)
		{
			_coroutinePerformer = coroutinePerformer;
		}

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
			KillProcess();
			
			_process = _coroutinePerformer.StartPerform(ProcessShow());
		}

		public void Hide(Action callback = null)
		{
			KillProcess();
			
			_process = _coroutinePerformer.StartPerform(ProcessHide(callback));
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

		private IEnumerator ProcessShow()
		{
			OnPreShow();

			yield return View.Show().WaitForCompletion();

			OnPostShow();
		}

		private IEnumerator ProcessHide(Action callback)
		{
			OnPreHide();

			yield return View.Hide().WaitForCompletion();

			OnPostHide();
			
			callback?.Invoke();
		}
		
		private void KillProcess()
		{
			if (_process != null)
				_coroutinePerformer.StopPerform(_process);
		}
	}
}