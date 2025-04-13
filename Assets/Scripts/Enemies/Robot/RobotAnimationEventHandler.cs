using UnityEngine;

namespace Enemies.Robot
{
    public class RobotAnimationEventHandler : MonoBehaviour
    {
        public delegate void AnimationEventDelegate();
        
        public event AnimationEventDelegate OnAnimationEnded;
        public event AnimationEventDelegate OnAttackHit;

        public enum AnimationEventType
        {
            OnAnimationEnded,
            OnAttackHit
        }
        public void TriggerAnimationEvent(AnimationEventType eventType)
        {
            switch (eventType)
            {
                case AnimationEventType.OnAnimationEnded:
                    TriggerOnAnimationEnded();
                    break;
                case AnimationEventType.OnAttackHit:
                    TriggerOnAttackHit();
                    break;
            }
        }

        public void TriggerOnAnimationEnded()
        {
            OnAnimationEnded?.Invoke();
        }

        public void TriggerOnAttackHit()
        {
            OnAttackHit?.Invoke();
        }
        
    }
}