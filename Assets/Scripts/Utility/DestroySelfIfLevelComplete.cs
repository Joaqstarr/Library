using System;
using Systems.Gamemode;
using Systems.SaveSystem;
using UnityEngine;

namespace Utility
{
    public class DestroySelfIfLevelComplete : MonoBehaviour
    {
        [Serializable]
        private struct DestroySelfLevelRule
        {
            public int Level;
            public bool _isComplete;
        }

        [SerializeField] private DestroySelfLevelRule[] _rules;

        private void Start()
        {
            Invoke(nameof(CheckIfLevelsComplete), 0.01f);
        }

        private void CheckIfLevelsComplete()
        {
            if (Gamemanager.Instance)
            {
                SaveData data = Gamemanager.Instance.GetSaveData();

                if (data != null)
                {
                    for (int i = 0; i < _rules.Length; i++)
                    {
                        if (data.IsLevelCompleted[_rules[i].Level] == _rules[i]._isComplete)
                        {
                            Destroy(gameObject);
                            return;
                        }
                    }
                }
            }
        }
    }
}