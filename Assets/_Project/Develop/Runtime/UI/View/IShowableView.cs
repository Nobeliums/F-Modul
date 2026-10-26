using DG.Tweening;

namespace _Project.Develop.Runtime.UI.View
{
	public interface IShowableView : IView
	{
		Tween Show();
		Tween Hide();
	}
}