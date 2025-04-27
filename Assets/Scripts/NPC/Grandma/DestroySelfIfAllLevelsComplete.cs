using Systems.Gamemode;
using Systems.SaveSystem;
using UnityEngine;

namespace NPC.Grandma
{
    public class DestroySelfIfAllLevelsComplete : MonoBehaviour
    {
        private void Start()
        {
            if (Gamemanager.Instance)
            {
                SaveData data = Gamemanager.Instance.GetSaveData();

                if (data != null)
                {
                    if (data.GetLevelCompletedCount() >= 3)
                    {
                        Destroy(gameObject);
                    }
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}