using UnityEngine;

namespace Utility
{
    public class RandomShaker : MonoBehaviour
    {
        [Header("Position Shake")]
        [SerializeField] private Vector3 positionIntensity = Vector3.one; // Intensity of position shake
        [SerializeField] private float positionFrequency = 1f; // Frequency of position shake

        [Header("Rotation Shake")]
        [SerializeField] private Vector3 rotationIntensity = Vector3.one; // Intensity of rotation shake
        [SerializeField] private float rotationFrequency = 1f; // Frequency of rotation shake

        [Header("Scale Shake")]
        [SerializeField] private Vector3 scaleIntensity = Vector3.one; // Intensity of scale shake
        [SerializeField] private float scaleFrequency = 1f; // Frequency of scale shake

        private Vector3 _originalPosition;
        private Vector3 _originalRotation;
        private Vector3 _originalScale;

        private Vector3 _positionOffsetSeed;
        private Vector3 _rotationOffsetSeed;
        private Vector3 _scaleOffsetSeed;

        private void Start()
        {
            // Store the original local transform values
            _originalPosition = transform.localPosition;
            _originalRotation = transform.localEulerAngles;
            _originalScale = transform.localScale;

            // Generate unique random seeds for each object
            _positionOffsetSeed = new Vector3(Random.value, Random.value, Random.value);
            _rotationOffsetSeed = new Vector3(Random.value, Random.value, Random.value);
            _scaleOffsetSeed = new Vector3(Random.value, Random.value, Random.value);
        }

        private void Update()
        {
            // Shake position
            Vector3 positionOffset = ScaleVector3(new Vector3(
                Mathf.PerlinNoise(Time.time * positionFrequency + _positionOffsetSeed.x, _positionOffsetSeed.y) * 2f - 1f,
                Mathf.PerlinNoise(_positionOffsetSeed.x, Time.time * positionFrequency + _positionOffsetSeed.y) * 2f - 1f,
                Mathf.PerlinNoise(Time.time * positionFrequency + _positionOffsetSeed.z, _positionOffsetSeed.z) * 2f - 1f
            ), positionIntensity);
            transform.localPosition = _originalPosition + positionOffset;

            // Shake rotation
            Vector3 rotationOffset = ScaleVector3(new Vector3(
                Mathf.PerlinNoise(Time.time * rotationFrequency + _rotationOffsetSeed.x, _rotationOffsetSeed.y) * 2f - 1f,
                Mathf.PerlinNoise(_rotationOffsetSeed.x, Time.time * rotationFrequency + _rotationOffsetSeed.y) * 2f - 1f,
                Mathf.PerlinNoise(Time.time * rotationFrequency + _rotationOffsetSeed.z, _rotationOffsetSeed.z) * 2f - 1f
            ), rotationIntensity);
            transform.localEulerAngles = _originalRotation + rotationOffset;

            // Shake scale
            Vector3 scaleOffset = ScaleVector3(new Vector3(
                Mathf.PerlinNoise(Time.time * scaleFrequency + _scaleOffsetSeed.x, _scaleOffsetSeed.y) * 2f - 1f,
                Mathf.PerlinNoise(_scaleOffsetSeed.x, Time.time * scaleFrequency + _scaleOffsetSeed.y) * 2f - 1f,
                Mathf.PerlinNoise(Time.time * scaleFrequency + _scaleOffsetSeed.z, _scaleOffsetSeed.z) * 2f - 1f
            ), scaleIntensity);
            transform.localScale = _originalScale + scaleOffset;
        }

        private Vector3 ScaleVector3(Vector3 vector, Vector3 scalar)
        {
            return new Vector3(vector.x * scalar.x, vector.y * scalar.y, vector.z * scalar.z);
        }
    }
}