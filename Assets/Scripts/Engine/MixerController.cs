using UnityEngine;
using UnityEngine.Audio;

namespace Engine
{
    public class MixerController : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _audioMixer;


        private bool _isMasterVolumeEnabled = true;
        private readonly string _master = "MasterVolume",
            _music = "MusicVolume",
            _effect = "EffectsVolume",
            _ui = "InterfaceVolume";
        
        public void ToggleMasterMixer()
        {
            _isMasterVolumeEnabled = !_isMasterVolumeEnabled;
            ToggleMasterMixer(_isMasterVolumeEnabled);
        }

        public void ToggleMasterMixer(bool volumeEnabled)
        {
            if (volumeEnabled)
            {
                _audioMixer.audioMixer.SetFloat(_master, 0);
            }
            else
            {
                _audioMixer.audioMixer.SetFloat(_master, -80);
            }
        }

        public void ChangeMusicsVolume(float volume)
        {
            ChangeVolume(_music, volume);
        }

        public void ChangeEffectsVolume(float volume)
        {
            ChangeVolume(_effect, volume);
        }

        public void ChangeButtonsVolume(float volume)
        {
            ChangeVolume(_ui, volume);
        }

        private void ChangeVolume(string mixerName, float value)
        {
            _audioMixer.audioMixer.SetFloat(mixerName, Mathf.Log10(value) * 20);
        }
    }
}