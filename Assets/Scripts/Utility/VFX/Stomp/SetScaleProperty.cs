using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

namespace Utility.VFX.Stomp
{
    public class SetScaleProperty : MonoBehaviour
    {
        [SerializeField]
        private string _propertyName = "WorldScale"; // Name of the exposed property in your VFX Graph

        private VisualEffect _vfx;

        private void Awake()
        {
            _vfx = GetComponent<VisualEffect>();
        }

        private void Update()
        {
            if (_vfx.HasVector3(_propertyName))
            {
                _vfx.SetVector3(_propertyName, transform.lossyScale);
            }
        }
    }
}