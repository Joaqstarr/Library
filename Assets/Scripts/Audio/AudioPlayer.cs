using System;
using UnityEngine;

namespace Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _source;
        

        public void PlayAudioClip(AudioClip clip)
        {
            _source.PlayOneShot(clip);
        }
    }
}