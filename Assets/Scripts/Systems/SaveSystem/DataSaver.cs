using System;
using System.IO;
using UnityEngine;

namespace Systems.SaveSystem
{
    public class DataSaver
    {

        private string _path = "";
        
        public DataSaver(string path)
        {
            _path = Path.Join(Application.persistentDataPath, path);
        }
        
        public SaveData LoadData()
        {
            if (File.Exists(_path))
            {
                try
                {
                    string json = File.ReadAllText(_path);
                    SaveSystem.SaveData data = JsonUtility.FromJson<SaveData>(json);
                    data.RestoreAfterLoad();
                    return data;
                }
                catch (IOException e)
                {
                    Debug.LogError($"Failed to load save data: {e.Message}");
                }
            }
            else
            {
                Debug.LogWarning("Save file not found. Returning default SaveData.");
            }

            return new SaveData();
        }

        public void SaveData(SaveData data)
        {
            try
            {
                data.PrepareForSave();
                string json = JsonUtility.ToJson(data, true);

                if (_path == null || _path == "")
                {
                    throw new Exception("Save path not set");
                }
                Directory.CreateDirectory(Path.GetDirectoryName(_path));

                using (FileStream stream = new FileStream(_path, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(json);
                    }
                }

            }catch (IOException e)
            {
                Debug.LogError($"Failed to save data: {e.Message}");
            }
        }
    }
}