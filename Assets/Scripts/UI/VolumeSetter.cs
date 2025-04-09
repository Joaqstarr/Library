using UnityEngine;
using UnityEngine.Audio;

namespace UI
{
    public class VolumeSetter : MonoBehaviour
    {

        [SerializeField]
        private AudioMixer _audioMixer;
        
        public void SetMasterVolume(float volume)
        {
            AudioListener.volume = volume;
            PlayerPrefs.SetFloat("MasterVolume", volume);
            
        }

        public void SetSFXVolume(float volume)
        {
            
            _audioMixer.SetFloat("SFXVolume", volume);
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }
        
        public void SetMusicVolume(float volume)
        {
            _audioMixer.SetFloat("MusicVolume", volume);
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }

        void Start()
        {
            AudioListener.volume = PlayerPrefs.GetFloat("MasterVolume", 1);


            _audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume", 0));

            _audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume", 0));
        }
    }
}