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

        private bool _isLoaded = false;
        public bool IsLoaded()
        {
            return _isLoaded;
        }
        
        public void LoadScene()
        {
            if (!IsLoaded())
            {
                _isLoaded = true;
                SceneManager.LoadSceneAsync(ScenePath, LoadSceneMode.Additive);
            }
        }
        
        public void UnloadScene()
        {
            if (IsLoaded())
            {
                _isLoaded = false;
                SceneManager.UnloadSceneAsync(ScenePath, UnloadSceneOptions.None);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ScenePath", ScenePath);
        }
    }
}