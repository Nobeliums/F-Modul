using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.UI.Presenters;
using _Project.Develop.Runtime.UI.View;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.UI.Core.Popups
{
	public class SelectCharsGameTypePopupViewPresenter : PopupBasePresenter
	{
		private const string TitleName = "Select chars sequence type"; 

		private readonly ProjectPresenterFactory _presenterFactory;
		private readonly ViewFactory _viewFactory;

		private readonly SelectCharsGameTypePopupView _view;

		private List<StartGameButtonPresenter> _presenters = new();

		public SelectCharsGameTypePopupViewPresenter(
			SelectCharsGameTypePopupView view,
			ICoroutinePerformer performer,
			ViewFactory viewFactory,
			ProjectPresenterFactory presenterFactory) : base(performer)
		{
			_viewFactory = viewFactory;
			_presenterFactory = presenterFactory;
			_view = view;
		}

		protected override PopupBaseView View =>  _view;

		public override void Initialize()
		{
			base.Initialize();

			

			foreach (GameCharsSequenceType gameCharsSequenceType in Enum.GetValues(typeof(GameCharsSequenceType)))
			{
				ButtonView button = _viewFactory.Create<ButtonView>(ViewIds.TileButtonView);
				StartGameButtonPresenter presenter =
					_presenterFactory.CreateStartGameButtonPresenter(
						gameCharsSequenceType,
						button);
				
				presenter.Initialize();
				
				_view.ButtonListView.Add(button);
			}
		}

		public override void Dispose()
		{
			base.Dispose();

			foreach (StartGameButtonPresenter presenter in _presenters)
			{
				_view.ButtonListView.Remove(presenter.ButtonView);
				_viewFactory.Release(presenter.ButtonView);
				presenter.Dispose();
			}
			
			_presenters.Clear();
		}
	}
}