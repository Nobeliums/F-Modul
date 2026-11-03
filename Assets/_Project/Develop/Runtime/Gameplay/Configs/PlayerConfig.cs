using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Configs
{
	[CreateAssetMenu(menuName = "Project/PlayerConfig", fileName = "PlayerConfig", order = 0)]
	public class PlayerConfig : ScriptableObject
	{
		[field: SerializeField]
		public PlayerMovementConfig PlayerMovementConfig { get; private set; }
		
		[field: SerializeField]
		public PlayerHealthConfig PlayerHealthConfig { get; private set; }
	}
}