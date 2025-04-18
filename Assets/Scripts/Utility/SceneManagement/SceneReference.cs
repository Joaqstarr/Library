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
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (ScenePath.Contains(scene.name) && scene.isLoaded)
                {
                    return true;
                }
            }
            return false;
        }



        public void LoadScene(bool forceReload = true)
        {
            if (forceReload)
            {
                if (IsLoaded())
                {
                    UnloadScene();
                }

                SceneManager.LoadSceneAsync(ScenePath, LoadSceneMode.Additive);
            }
            else
            {
                if (!IsLoaded())
                {
                    SceneManager.LoadSceneAsync(ScenePath, LoadSceneMode.Additive);
                }
            }
        }
        
        public void UnloadScene()
        {
            if (IsLoaded())
            {
                SceneManager.UnloadSceneAsync(ScenePath, UnloadSceneOptions.None);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ScenePath", ScenePath);
        }
    }
}