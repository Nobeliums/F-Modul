using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Configs
{
	[CreateAssetMenu(menuName = "Project/PlayerHealthConfig", fileName = "PlayerHealthConfig", order = 0)]
	public class PlayerHealthConfig : ScriptableObject
	{
		[field: SerializeField]
		public float MaxHealth { get; private set; }
		
		[field: SerializeField]
		public float StartHealth { get; private set; }
	}
}