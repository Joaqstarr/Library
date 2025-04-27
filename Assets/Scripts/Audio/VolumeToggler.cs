using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class VolumeToggler : MonoBehaviour
    {
        [SerializeField] private string _propertyName;
        
        [SerializeField]private AudioMixer _mixer;


        private Tweener _tweener;
        public void EnableMusic()
        {
            if(_tweener != null)
                _tweener.Kill();
            
            _tweener = DOVirtual.Float(-80, 0, 1f, (float value) =>
            {
                _mixer.SetFloat(_propertyName, value);
            }).OnComplete(() =>
            {
                _mixer.SetFloat(_propertyName, 0);
            });
        }

        public void DisableMusic()
        {
            if(_tweener != null)
                _tweener.Kill();
            
            _tweener =DOVirtual.Float(0, -80, 1f, (float value) =>
            {
                _mixer.SetFloat(_propertyName, value);
            }).OnComplete(() =>
            {
                _mixer.SetFloat(_propertyName, -80);
            });
        }
    }
}