using System.Collections.Generic;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.View
{
	public class ElementsListView<TELement> : MonoBehaviour, IView where TELement : MonoBehaviour, IView
	{
		[SerializeField] private Transform _parent;
		
		private List<TELement>  _elements = new();
		
		public void Add(params TELement[] elements)
		{
			_elements.AddRange(elements);

			foreach (TELement element in elements)
			{
				element.transform.SetParent(_parent);
			}
		}
		
		public void Remove(TELement element)
		{
			_elements.Remove(element);
			element.transform.SetParent(null);
		}
	}
}