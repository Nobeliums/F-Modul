using UnityEngine;

namespace _Project.Develop.Runtime.UI.Core.Gameplay
{
	public class GameplayCanvasRoot : MonoBehaviour
	{
		[field: SerializeField]
		public Transform HUD { get; private set; }
		
		[field: SerializeField]
		public Transform VFXUnderPopups { get; private set; }

		[field: SerializeField]
		public Transform Popups { get; private set; }

		[field: SerializeField]
		public Transform VFXOverPopups { get; private set; }
	}
}