using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player.Face
{
    public class FaceBlinker : MonoBehaviour
    {
        private FaceEmotionHandler _faceEmotionHandler;

        [SerializeField] private Vector2 _blinkRanTime = new Vector2(3, 6);
        private void Awake()
        {
            _faceEmotionHandler = GetComponent<FaceEmotionHandler>();

            StartCoroutine(Blink());
        }

        IEnumerator Blink()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(_blinkRanTime.x, _blinkRanTime.y));
                _faceEmotionHandler.AddEmotion(FaceEmotionHandler.FaceEmotion.Blink, 0.1f);                
            }
        }
    }
}