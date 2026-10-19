namespace _Project.Develop.Runtime.Utilities.LoadingScreen
{
	public interface ILoadingScreen
	{
		bool IsShowing { get; }

		void Show();
		void Hide();
	}
}