using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Configs.Levels;
using _Project.Develop.Runtime.UI.Presenters;
using _Project.Develop.Runtime.UI.View;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.UI.Core.Popups
{
	public class SelectLevelPopupViewPresenter : PopupBasePresenter
	{
		private const string TitleName = "Select chars sequence type";
		private readonly ProjectPresenterFactory _presenterFactory;
		private readonly ViewFactory _viewFactory;
		private readonly LevelListConfig _levelListConfig;

		private readonly SelectLevelPopupView _view;

		private List<LevelButtonPresenter> _presenters = new();

		public SelectLevelPopupViewPresenter(
			SelectLevelPopupView view,
			ICoroutinePerformer performer,
			ViewFactory viewFactory,
			ProjectPresenterFactory presenterFactory,
			LevelListConfig levelListConfig) : base(performer)
		{
			_viewFactory = viewFactory;
			_presenterFactory = presenterFactory;
			_levelListConfig = levelListConfig;
			_view = view;
		}

		protected override PopupBaseView View =>  _view;

		public override void Initialize()
		{
			base.Initialize();
			
			for (int levelNumber = 1; levelNumber <= _levelListConfig.Count; levelNumber++)
			{
				ButtonView button = _viewFactory.Create<ButtonView>(ViewIds.SelectCharsGameButtonView);
				LevelButtonPresenter presenter =
					_presenterFactory.CreateLevelButtonPresenter(
						levelNumber,
						button);

				presenter.Initialize();

				_presenters.Add(presenter);
				_view.ButtonListView.Add(button);
			}
		}

		public override void Dispose()
		{
			base.Dispose();

			foreach (LevelButtonPresenter presenter in _presenters)
			{
				_view.ButtonListView.Remove(presenter.ButtonView);
				_viewFactory.Release(presenter.ButtonView);
				presenter.Dispose();
			}
			
			_presenters.Clear();
		}
	}
}