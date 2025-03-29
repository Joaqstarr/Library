using UnityEngine;

namespace Enemies.Robot
{
    [CreateAssetMenu(fileName = "RobotData", menuName = "Enemy Data/Robot Data", order = 0)]
    public class RobotData : ScriptableObject
    {
        [field:SerializeField] public float WanderRadius { get; private set; } = 5f;
        [field:SerializeField] public float ReachedThreshold { get; private set; }  = 0.1f;
        [field:SerializeField] public float SearchRadius { get; private set; }  = 10f;
        [field: SerializeField] public LayerMask SearchMask { get; private set; }

        [field: Header("Aggro")] [field: SerializeField]
        public float StrafeDistance { get; private set; }= 5;
    }
}