using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.UI.Core.Popups;
using _Project.Develop.Runtime.UI.Presenters;
using _Project.Develop.Runtime.Utilities.ResourcesManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Core
{
	public abstract class PopupService : IDisposable
	{
		protected readonly ProjectPresenterFactory _projectPresenterFactory;

		private readonly ViewFactory _viewFactory;
		private readonly Dictionary<PopupBasePresenter, PopupBaseView> _presentersToViews = new();
		
		public PopupService(ProjectPresenterFactory projectPresenterFactory, ViewFactory viewFactory)
		{
			_projectPresenterFactory = projectPresenterFactory;
			_viewFactory = viewFactory;
		}
		
		protected abstract Transform PopupLayer { get; }

		public SelectCharsGameTypePopupViewPresenter CreateStartGamePopup()
		{
			SelectCharsGameTypePopupView view = _viewFactory
				.Create<SelectCharsGameTypePopupView>(ViewIds.SelectGameCharsView, PopupLayer);

			SelectCharsGameTypePopupViewPresenter presenter =
				_projectPresenterFactory.CreateSelectCharsGameTypePopupViewPresenter(view);

			OnPopupCreated(presenter, view);

			return presenter;
		}
		
		public void ClosePopup(PopupBasePresenter presenter)
		{
			presenter.CloseRequest -= ClosePopup;
			
			presenter.Hide(() =>
			{
				DisposeFor(presenter);
				_presentersToViews.Remove(presenter);
			});
		}

		public void Dispose()
		{
			foreach (var presenter in _presentersToViews.Keys)
			{
				presenter.CloseRequest -= ClosePopup;
				DisposeFor(presenter);
			}
			
			_presentersToViews.Clear();
		}

		protected void OnPopupCreated(PopupBasePresenter presenter, PopupBaseView view)
		{
			_presentersToViews.Add(presenter, view);;
			presenter.Initialize();
			presenter.Show();

			presenter.CloseRequest += ClosePopup;
		}

		private void DisposeFor(PopupBasePresenter presenter)
		{
			presenter.Dispose();
			_viewFactory.Release(_presentersToViews[presenter]);
		}


	}
}