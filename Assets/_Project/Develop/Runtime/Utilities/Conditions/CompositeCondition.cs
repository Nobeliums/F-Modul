using System;
using System.Collections.Generic;

namespace _Project.Develop.Runtime.Utilities.Conditions
{
	public class CompositeCondition : ICompositeCondition
	{
		private List<ICondition> _conditions = new();
		private Func<bool, bool, bool> _standartLogicOperation;

		public CompositeCondition(Func<bool, bool, bool> standartLogicOperation)
		{
			_standartLogicOperation = standartLogicOperation;
		}

		public CompositeCondition() : this(LogicOperations.And)
		{
		}

		public bool Evaluate()
		{
			if (_conditions.Count == 0)
				return false;
			
			bool result =  _conditions[0].Evaluate();

			for (int i = 1; i < _conditions.Count; i++)
			{
				result = _standartLogicOperation.Invoke(result, _conditions[i].Evaluate());
			}
			
			return result;
		}

		public ICompositeCondition AddCondition(ICondition condition)
		{
			_conditions.Add(condition);
			return this;
		}

		public ICompositeCondition RemoveCondition(ICondition condition)
		{
			_conditions.Add(condition);
			return this;
		}
	}
}