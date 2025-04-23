using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Face
{
    public class FaceEmotionHandler : MonoBehaviour
    {

        public enum FaceEmotion
        {
            Neutral,
            Happy,
            Sad,
            Angry,
            Blow,
            Blink
        }
        public enum FacePart
        {
            Eyes,
            Mouth,
            EyeBrowL,
            EyeBrowR
        }
        
        [Serializable]
        struct EmotionDefinition
        {
            public FaceEmotion Emotion;

            public Material Eyes;
            public Material Mouth;
            public Material LEyebrow;
            public Material REyebrow;
        }

        struct EmotionStruct
        {
            public FaceEmotion Emotion;
            public float Duration;
            public float Timer;
        }

        [SerializeField] private EmotionDefinition _defaultEmotion;
        [SerializeField]
        private EmotionDefinition[] _emotionDefinitions;
        private Dictionary<FaceEmotion, EmotionDefinition> _emotionToDefinition;
        
        private Stack<EmotionStruct> _activeEmotions = new Stack<EmotionStruct>();

        [SerializeField] private Renderer _eyeRenderer;
        [SerializeField] private Renderer _mouthRenderer;
        [SerializeField] private Renderer _leftEyeBrowRenderer;
        [SerializeField] private Renderer _rightEyeBrowRenderer;

        private void Start()
        {
            _emotionToDefinition = new Dictionary<FaceEmotion, EmotionDefinition>();
            foreach (var emotionDefinition in _emotionDefinitions)
            {
                _emotionToDefinition[emotionDefinition.Emotion] = emotionDefinition;
            }
        }


        public void AddEmotion(FaceEmotion emotion, float duration)
        {
            EmotionStruct newEmotion = new EmotionStruct
            {
                Emotion = emotion,
                Duration = duration,
                Timer = 0f
            };

            _activeEmotions.Push(newEmotion);
        }
        
        public void StopEmotion(FaceEmotion emotion)
        {
            if (_activeEmotions.Count == 0) return;
            

            if(_activeEmotions.Peek().Emotion == emotion)
            {
                _activeEmotions.Pop();
            }
            
            Stack<EmotionStruct> tempEmotions = new Stack<EmotionStruct>();

            while (_activeEmotions.Count > 0)
            {
                if(_activeEmotions.Peek().Emotion == emotion)
                {
                    _activeEmotions.Pop();
                }
                else
                {
                    tempEmotions.Push(_activeEmotions.Pop());
                }
            }
            
            while (tempEmotions.Count > 0)
            {
                _activeEmotions.Push(tempEmotions.Pop());
            }
        }

        private void Update()
        {
            if (_activeEmotions.Count == 0)
            { 
                _leftEyeBrowRenderer.material = GetMaterialForFacePart(FacePart.EyeBrowL);
                _rightEyeBrowRenderer.material = GetMaterialForFacePart(FacePart.EyeBrowR);
                _eyeRenderer.material = GetMaterialForFacePart(FacePart.Eyes);
                _mouthRenderer.material = GetMaterialForFacePart(FacePart.Mouth);
                return;
            }

            EmotionStruct currentEmotion = _activeEmotions.Pop();
            currentEmotion.Timer += Time.deltaTime;
            
            _activeEmotions.Push(currentEmotion);

            if (currentEmotion.Timer >= currentEmotion.Duration)
            {
                _activeEmotions.Pop();
            }
            else
            {
                _leftEyeBrowRenderer.material = GetMaterialForFacePart(FacePart.EyeBrowL);
                _rightEyeBrowRenderer.material = GetMaterialForFacePart(FacePart.EyeBrowR);
                _eyeRenderer.material = GetMaterialForFacePart(FacePart.Eyes);
                _mouthRenderer.material = GetMaterialForFacePart(FacePart.Mouth);
            }
        }
        

        private Material GetMaterialForFacePart(FacePart part)
        {
            foreach (var emotion in _activeEmotions)
            {
                if (_emotionToDefinition.TryGetValue(emotion.Emotion, out var definition))
                {
                    var material = GetMaterialForFacePartFromDefinition(part, definition);
                    if (material != null)
                    {
                        return material;
                    }
                }
            }

            return GetMaterialForFacePartFromDefinition(part, _defaultEmotion); // Return default if no material is found
        }
        
        
        private Material GetMaterialForFacePartFromDefinition(FacePart part, EmotionDefinition definition)
        {
            switch (part)
            {
                case FacePart.Eyes:
                    return definition.Eyes;
                case FacePart.Mouth:
                    return definition.Mouth;
                case FacePart.EyeBrowL:
                    return definition.LEyebrow;
                case FacePart.EyeBrowR:
                    return definition.REyebrow;
                default:
                    return null;
            }
        }

        public void HideFace()
        {
            _rightEyeBrowRenderer.enabled = false;
            _leftEyeBrowRenderer.enabled = false;
            _eyeRenderer.enabled = false;
            _mouthRenderer.enabled = false;
        }
        
        public void ShowFace()
        {
            _rightEyeBrowRenderer.enabled = true;
            _leftEyeBrowRenderer.enabled = true;
            _eyeRenderer.enabled = true;
            _mouthRenderer.enabled = true;
        }
    }
}