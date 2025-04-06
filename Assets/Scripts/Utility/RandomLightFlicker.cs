using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Utility
{
    public class RandomLightFlicker : MonoBehaviour
    {
        private Light _light;
        private float _baseIntensity;
        [SerializeField] private Vector2 _flickerAmount = new Vector2(0.2f, 1.2f);
        [SerializeField]
        private float _flickerSpeed = 0.1f;

        private void Start()
        {
            _light = GetComponent<Light>();
            if (_light == null)
            {
                Debug.LogError("RandomLightFlicker requires a Light component.");
                enabled = false;
                return;
            }
            _baseIntensity = _light.intensity;
            StartCoroutine(Flicker());
        }

        private IEnumerator Flicker()
        {
            yield return new WaitForSeconds(Random.Range(0, 5f));

            while (true)
            {
                float time = _flickerSpeed + Random.Range(0f, _flickerSpeed);
                _light.DOIntensity(_baseIntensity * Random.Range(_flickerAmount.x, _flickerAmount.y), time);
                yield return new WaitForSeconds(time);
            }
        }
    }
}