using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.Input
{
	public class DesktopInputService : IInputService
	{
		private const string Horizontal = "Horizontal";
		private const string Vertical = "Vertical";
		private const string MouseXAxis = "Mouse X";
		private const KeyCode AttackButtonKeyCode = KeyCode.R;
		private const int AttackMouseButtonCode = 0;

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

		public float XAxisDelta
		{
			get
			{
				if (IsEnabled == false)
					return 0f;

				return UnityEngine.Input.GetAxis(MouseXAxis);
			}
		}

		public bool IsAttackButtonPressed
		{
			get
			{
				if (IsEnabled == false)
					return false;
				
				return UnityEngine.Input.GetKeyDown(AttackButtonKeyCode) || UnityEngine.Input.GetMouseButtonDown(AttackMouseButtonCode);
			}
		}

		public Vector3 CursorPosition {
			get
			{
				if (IsEnabled == false)
					return Vector3.zero;
				
				Plane plane = new Plane(Vector3.up, new Vector3(0f, 0f, 0f));

				Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);

				if (plane.Raycast(ray, out float distance))
				{
					return ray.GetPoint(distance);
				}
				
				return Vector3.zero;
			}
		}
	}
}