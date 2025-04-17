using System.Runtime.Serialization;
using UnityEngine;
using Utility.SceneManagement;

namespace Systems.SaveSystem
{
    public class SaveData
    {
        public string CurrentLevelPath = null; // Serialized version of CurrentLevel
        public bool[] IsLevelCompleted = new bool[3];

        [System.NonSerialized]
        public SceneReference CurrentLevel = null; // Non-serialized reference

        
        public SerializableDictionary<string, bool> CutsceneFlags = new SerializableDictionary<string, bool>();
        public void PrepareForSave()
        {
            CurrentLevelPath = CurrentLevel != null ? CurrentLevel.name : null;
        }

        public void RestoreAfterLoad()
        {
            if (!string.IsNullOrEmpty(CurrentLevelPath))
            {
                CurrentLevel = Resources.Load<SceneReference>(CurrentLevelPath);
                
            }
        }
    }
}