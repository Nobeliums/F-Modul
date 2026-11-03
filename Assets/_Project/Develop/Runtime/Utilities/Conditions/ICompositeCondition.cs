using Unity.VisualScripting;

namespace _Project.Develop.Runtime.Utilities.Conditions
{
	public interface ICompositeCondition : ICondition
	{
		ICompositeCondition AddCondition(ICondition condition);

		ICompositeCondition RemoveCondition(ICondition condition);
	}
}