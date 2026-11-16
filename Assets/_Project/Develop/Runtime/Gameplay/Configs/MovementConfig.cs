using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Configs
{
	[CreateAssetMenu(fileName = "MovementConfig",  menuName = "Project/MovementConfig")]
	public class MovementConfig : ScriptableObject
	{
		[field: SerializeField, Min(0)] public float MoveSpeed { get; private set; }
		[field: SerializeField, Min(0)] public float RotationSpeed { get; private set; }
	}
}