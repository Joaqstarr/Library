using System;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility.SceneManagement
{
    [Serializable]
    [CreateAssetMenu(fileName = "Scene Reference", menuName = "Scene Reference", order = 0)]
    public class SceneReference : ScriptableObject, ISerializable
    {
        [field: SerializeField]
        public string ScenePath { get; private set; }

        public bool IsLoaded()
        {
            return SceneManager.GetSceneByPath(ScenePath).isLoaded;
        }
        
        public void LoadScene()
        {
            SceneManager.LoadSceneAsync(ScenePath, LoadSceneMode.Additive);
        }
        
        public void UnloadScene()
        {
            SceneManager.UnloadSceneAsync(ScenePath, UnloadSceneOptions.None);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ScenePath", ScenePath);
        }
    }
}