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
        public float MusicValue => Save.GetValue(_music);
        public float EffectsValue => Save.GetValue(_effect);
        public float UIValue => Save.GetValue(_ui);

        public bool IsMasterEnabled
        {
            get => _isMasterEnabled;
            set
            {
                _isMasterEnabled = value;
                ToggleMasterMixer(_isMasterEnabled ? 0 : -80);
                OnIsMasterEnabled?.Invoke(_isMasterEnabled);
            }
        }
        
        public OptionsSave Save
        {
            get => Game.SaveData.options;
            private set => Game.SaveData.options = value;
        }
        
        private bool _isMasterEnabled;
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
            Game.Store.Save(Game.SaveData);
        }
        
        public void ResetSaveData()
        {
            Save = Game.Store.Load()?.options;
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