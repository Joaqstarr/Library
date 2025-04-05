using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Utility
{
    [ExecuteInEditMode]
    public class AnimationRigRunner : MonoBehaviour
    {
            public bool shouldBuildRig;
            
            
            private void Update()
            {

                if (shouldBuildRig)
                {
                    GetComponent<RigBuilder>().Build();
    
                    shouldBuildRig = false;
                }
            }
    }
}