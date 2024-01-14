using System;
using Save;
using Save.Data;
using Save.Data.Format;
using Service;
using UnityEngine;
using UnityEngine.Audio;

namespace Engine
{
    public class MixerController : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup audioMixer;
        public bool IsMasterEnabled => _isMasterVolumeEnabled;
        public float MusicValue => _saveFormatter.SaveData.GetValue(music);
        public float EffectsValue => _saveFormatter.SaveData.GetValue(effect);
        public float UIValue => _saveFormatter.SaveData.GetValue(ui);

        private IStorable<SaveFormatter<SoundSave, Option>> _storable;
        private SaveFormatter<SoundSave, Option> _saveFormatter;
        private bool _isMasterVolumeEnabled = true;

        public const string master = "MasterVolume",
            music = "MusicVolume",
            effect = "EffectsVolume",
            ui = "InterfaceVolume";

        private void Awake()
        {
            _storable = new JsonSaveSystem<SaveFormatter<SoundSave, Option>>(SaveNames.Sound);
            _saveFormatter = new SaveFormatter<SoundSave, Option>();
            
            if (!_storable.IsExists)
            {
                InitData();
            }
            
            LoadFormatter();
            
            if (_saveFormatter.SaveData.GetValue(master) <= -80)
            {
                _isMasterVolumeEnabled = false;
            }
        }

        private void Start()
        {
            SetData();
        }

        public void SaveParams()
        {
            _storable.Save(_saveFormatter);
        }

        public void ResetParams()
        {
            LoadFormatter();
            SetData();
        }

        public void ToggleMasterMixer()
        {
            _isMasterVolumeEnabled = !_isMasterVolumeEnabled;
            ToggleMasterMixer(_isMasterVolumeEnabled);
        }

        public void ToggleMasterMixer(bool isVolumeEnabled)
        {
            var volume = isVolumeEnabled ? 0 : -80;
            ToggleMasterMixer(volume);
        }
        
        public void ToggleMasterMixer(float volume)
        {
            audioMixer.audioMixer.SetFloat(master, volume);
            _saveFormatter.SaveData.SetValue(master, volume);
            SaveParams();
        }

        public void ChangeMusicsVolume(float volume)
        {
            ChangeVolume(music, volume);
            _saveFormatter.SaveData.SetValue(music, volume);
        }

        public void ChangeEffectsVolume(float volume)
        {
            ChangeVolume(effect, volume);
            _saveFormatter.SaveData.SetValue(effect, volume);
        }

        public void ChangeButtonsVolume(float volume)
        {
            ChangeVolume(ui, volume);
            _saveFormatter.SaveData.SetValue(ui, volume);
        }

        private void ChangeVolume(string mixerName, float value)
        {
            audioMixer.audioMixer.SetFloat(mixerName, Mathf.Log10(value) * 20);
        }
        
        private void LoadFormatter()
        {
            var store = _storable.Load();
            var data = store?.SaveData;
            
            if (data == null) return;
            
            _saveFormatter = store;
        }

        private void InitData()
        {
            _storable.Create();
            ToggleMasterMixer(true);
            ChangeMusicsVolume(0.5f);
            ChangeEffectsVolume(0.5f);
            ChangeButtonsVolume(0.5f);
            _storable.Save(_saveFormatter);
        }

        private void SetData()
        {
            ToggleMasterMixer(_saveFormatter.SaveData.GetValue(master));
            ChangeMusicsVolume(_saveFormatter.SaveData.GetValue(music));
            ChangeEffectsVolume(_saveFormatter.SaveData.GetValue(effect));
            ChangeButtonsVolume(_saveFormatter.SaveData.GetValue(ui));
        }
    }
}