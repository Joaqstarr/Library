using UnityEngine;

namespace Level.Launchpad
{
    public class FanSpinner : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotateSpeed;
        
        private void Update()
        {
            transform.Rotate(_rotateSpeed * Time.deltaTime);
        }
    }
}