using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.Input
{
	public interface IInputService
	{
		public bool IsEnabled { get; set; }
		
		public Vector3 Direction { get; }
		
		public float XAxisDelta { get; }
		
		public bool IsAttackButtonPressed { get; }

		public Vector3 CursorPosition { get; }
	}
}