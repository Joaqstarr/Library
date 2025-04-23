using System;
using Player.Attack;
using UnityEngine;

namespace Player.Face
{
    public class InhaleExhaleFace : MonoBehaviour
    {

        private FaceEmotionHandler _emotionManager;
        
        private void Awake()
        {
            _emotionManager = GetComponent<FaceEmotionHandler>();
        }

        private void OnEnable()
        {
            PlayerAttackManager.OnAttackStart += OnAttackStart;
            PlayerAttackManager.OnAttackEnd += OnAttackEnd;
        }

        private void OnDisable()
        {
            PlayerAttackManager.OnAttackStart -= OnAttackStart;
            PlayerAttackManager.OnAttackEnd -= OnAttackEnd;
        }

        private void OnAttackEnd(bool isinhaling)
        {
            _emotionManager.StopEmotion(FaceEmotionHandler.FaceEmotion.Blow);
        }

        private void OnAttackStart(bool isinhaling)
        {
            _emotionManager.AddEmotion(FaceEmotionHandler.FaceEmotion.Blow);
        }
        
        
    }
}