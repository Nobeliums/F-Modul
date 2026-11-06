using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Configs
{
	[CreateAssetMenu(fileName = "MovementConfig",  menuName = "Project/MovementConfig")]
	public class MovementConfig : ScriptableObject
	{
		[field: SerializeField] public float MoveSpeed { get; private set; }
		[field: SerializeField] public float RotationSpeed { get; private set; }
	}
}