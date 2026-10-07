using UnityEngine;

namespace _Project.Develop.Runtime.ConfigManagement
{
    [CreateAssetMenu(fileName = "TestConfig", menuName = "ConfigManagement/TestConfig")]
    public class TestConfig : ScriptableObject
    {
        [field: SerializeField] public int Damage { get; private set; }
    }
}