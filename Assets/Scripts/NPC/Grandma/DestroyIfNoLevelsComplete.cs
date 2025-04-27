using Systems.Gamemode;
using Systems.SaveSystem;
using UnityEngine;

namespace NPC.Grandma
{
    public class DestroyIfNoLevelsComplete : MonoBehaviour
    {
        private void Start()
        {
            if (Gamemanager.Instance)
            {
                SaveData data = Gamemanager.Instance.GetSaveData();

                if (data != null)
                {
                    if (data.GetLevelCompletedCount() == 0)
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