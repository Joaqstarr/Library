using UnityEngine;
using UnityEngine.Timeline;

namespace Systems.CutsceneSystem
{
    [CreateAssetMenu(fileName = "New Cutscene Data", menuName = "Cutscenes/Cutscene Data", order = 0)]
    public class CutsceneData : ScriptableObject
    {
        [field: SerializeField]
        public TimelineAsset Cutscene { get; private set; } 

        
    }
}