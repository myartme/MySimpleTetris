using System;
using Save.Data.Format;
using UnityEngine;
using UnityEngine.Audio;
using View.GUI.Buttons;

namespace Engine
{
    public class MixerController : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup audioMixer;
        [SerializeField] private MasterSoundSwitchSprite _switchSprite;
        public event Action<bool> OnIsMasterEnabled;
        public float MusicValue => 0.1f;//Save.GetValue(_music);
        public float EffectsValue => 0.1f;//Save.GetValue(_effect);
        public float UIValue => 0.1f;//Save.GetValue(_ui);

        public bool IsMasterEnabled
        {
            get => true;//Save.GetValue(_master) == 0;
            private set
            {
                ToggleMasterMixer(value ? 0 : -80);
                OnIsMasterEnabled?.Invoke(value);
            }
        }
        public OptionsSave Save
        {
            get => Saver.SaveData.options;
            private set => Saver.SaveData.options = value;
        }
        
        private readonly string _master = "MasterVolume",
            _music = "MusicVolume",
            _effect = "EffectsVolume",
            _ui = "InterfaceVolume";

        private void Start()
        {
            InitializeSaveData();
            LoadSaveData();
        }

        public void StoreSaveData()
        {
            Saver.Store.Save(Saver.SaveData);
        }
        
        public void ResetSaveData()
        {
            Save = Saver.Store.Load()?.options;
            LoadSaveData();
        }
        
        public void ToggleMasterMixer()
        {
            IsMasterEnabled = !IsMasterEnabled;
            StoreSaveData();
        }
        
        public void ToggleMasterMixer(float volume)
        {
            audioMixer.audioMixer.SetFloat(_master, volume);
            Save.SetValueByName(_master, volume);
        }

        public void ChangeMusicsVolume(float volume)
        {
            ChangeVolume(_music, volume);
            Save.SetValueByName(_music, volume);
        }

        public void ChangeEffectsVolume(float volume)
        {
            ChangeVolume(_effect, volume);
            Save.SetValueByName(_effect, volume);
        }

        public void ChangeButtonsVolume(float volume)
        {
            ChangeVolume(_ui, volume);
            Save.SetValueByName(_ui, volume);
        }

        private void ChangeVolume(string mixerName, float value)
        {
            audioMixer.audioMixer.SetFloat(mixerName, Mathf.Log10(value) * 20);
        }

        private void InitializeSaveData()
        {
            Save.AddToList(_master, 0f);
            Save.AddToList(_music, 0.5f);
            Save.AddToList(_effect, 0.5f);
            Save.AddToList(_ui, 0.5f);
            StoreSaveData();
        }

        private void LoadSaveData()
        {
            IsMasterEnabled = Save.GetValue(_master) > -80;
            ChangeMusicsVolume(Save.GetValue(_music));
            ChangeEffectsVolume(Save.GetValue(_effect));
            ChangeButtonsVolume(Save.GetValue(_ui));
        }
    }
}