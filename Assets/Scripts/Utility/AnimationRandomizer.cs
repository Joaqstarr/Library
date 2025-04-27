using UnityEngine;

namespace Utility
{
    public class AnimationRandomizer : MonoBehaviour
    {
        private Animator Animator; // Assign in Inspector

        public void Start()
        {
            Animator = GetComponent<Animator>();
            Invoke(nameof(EnableAnimator), Random.value);
        }

        private void EnableAnimator()
        {
            Animator.enabled = true;
        }
    }
}