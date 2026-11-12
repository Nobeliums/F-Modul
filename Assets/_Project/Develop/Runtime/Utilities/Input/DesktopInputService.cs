using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.Input
{
	public class DesktopInputService : IInputService
	{
		private const string Horizontal = "Horizontal";
		private const string Vertical = "Vertical";

		public bool IsEnabled { get; set; } = true;

		public Vector3 Direction
		{
			get
			{
				if (IsEnabled == false)
					return Vector3.zero;
				
				return new Vector3(UnityEngine.Input.GetAxisRaw(Horizontal), 0f,
					UnityEngine.Input.GetAxisRaw(Vertical));
			}
		}
	}
}