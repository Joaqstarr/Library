using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    public class VolumeSetter : MonoBehaviour
    {

        [SerializeField]
        private AudioMixer _audioMixer;
        
        [SerializeField]
        private Slider _masterVolumeSlider;

        [SerializeField]
        private Slider _sfxVolumeSlider;
        [SerializeField]
        private Slider _musicVolumeSlider;
        
        public void SetMasterVolume(float volume)
        {
            AudioListener.volume = volume;
            PlayerPrefs.SetFloat("MasterVolume", volume);
            PlayerPrefs.Save();
            
        }

        public void SetSFXVolume(float volume)
        {
            _audioMixer.SetFloat("SFXVolume", volume);
            PlayerPrefs.SetFloat("SFXVolume", volume);
            PlayerPrefs.Save();
        }
        
        public void SetMusicVolume(float volume)
        {
            _audioMixer.SetFloat("MusicVolume", volume);
            PlayerPrefs.SetFloat("MusicVolume", volume);
            PlayerPrefs.Save();
        }

        void Start()
        {
            AudioListener.volume = PlayerPrefs.GetFloat("MasterVolume", 1);
            _masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0);


            _audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume", 0));
            _sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0);

            _audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume", 0));
            _musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0);
        }
    }
}