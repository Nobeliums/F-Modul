using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.UI.Core.Popups
{
	public class SelectCharsGameTypePopupViewPresenter : PopupBasePresenter
	{
		private const string TitleName = "Select chars sequence type"; 
		protected override PopupBaseView View { get; }

		public SelectCharsGameTypePopupViewPresenter(PopupBaseView view)
		{
			View = view;
		}

		public override void Initialize()
		{
			base.Initialize();
			
			
		}

		public override void Dispose()
		{
			base.Dispose();
		}
	}
}