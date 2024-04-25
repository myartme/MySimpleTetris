using System;
using Save.Data.Format;
using Service.Singleton;
using UnityEngine;
using UnityEngine.Audio;

namespace Engine
{
    [DisallowMultipleComponent]
    public class MixerController : AbstractSingleton<MixerController>
    {
        [SerializeField] private AudioMixerGroup audioMixer;
        public static event Action<bool> OnIsMasterEnabled;
        public float MusicValue => Save.GetValue(_music);
        public float EffectsValue => Save.GetValue(_effect);
        public float UIValue => Save.GetValue(_ui);

        public bool IsMasterEnabled
        {
            get => Save.GetValue(_master) == 0;
            private set
            {
                ChangeMasterMixer(value ? 0 : -80);
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

        private void Awake()
        {
            GetComponent<ISingularObject>().Initialize();
        }

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
        
        public void ToggleMasterMixer(bool value)
        {
            IsMasterEnabled = value;
        }
        
        public void ToggleMasterMixer()
        {
            IsMasterEnabled = !IsMasterEnabled;
        }
        
        public void ChangeMasterMixer(float volume)
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