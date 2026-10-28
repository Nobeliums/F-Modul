using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Configs
{
	[CreateAssetMenu(fileName = "PlayerRigidbodyMovementConfig",  menuName = "Project/PlayerRigidbodyMovementConfig")]
	public class PlayerRigidbodyMovementConfig : ScriptableObject
	{
		[field: SerializeField] public float MoveSpeed { get; private set; }
		[field: SerializeField] public float RotationSpeed { get; private set; }
	}
}