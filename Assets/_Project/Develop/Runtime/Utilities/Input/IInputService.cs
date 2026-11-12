using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.Input
{
	public interface IInputService
	{
		public bool IsEnabled { get; set; }
		
		public Vector3 Direction { get; }
	}
}