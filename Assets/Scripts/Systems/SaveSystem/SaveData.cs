using Utility.SceneManagement;

namespace Systems.SaveSystem
{
    public class SaveData
    {
        public SceneReference CurrentLevel = null;
        public float SteamAmount = -1;
        public int Health = -1;
        public bool[] IsLevelCompleted = new bool[3];
    }
}