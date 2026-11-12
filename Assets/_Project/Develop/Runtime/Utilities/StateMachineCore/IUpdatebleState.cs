namespace _Project.Develop.Runtime.Utilities.StateMachineCore
{
	public interface IUpdatebleState : IState
	{
		void Update(float deltaTime);
	}
}