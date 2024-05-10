using System;
using Save.Data.SaveDataElements;
using Service.Singleton;
using UnityEngine;
using UnityEngine.Audio;

namespace Engine
{
    public class MixerController : AbstractSingleton<MixerController>
    {
        [SerializeField] private AudioMixerGroup audioMixer;
        public static event Action<bool> OnIsMasterEnabled;
        public float MusicValue
        {
            get => Save.MusicVolume;
            private set => ChangeMusicsVolume(value);
        }
        public float EffectsValue
        {
            get => Save.EffectsVolume;
            private set => ChangeEffectsVolume(value);
        }
        public float InterfaceValue
        {
            get => Save.InterfaceVolume;
            private set => ChangeInterfaceVolume(value);
        }

        public bool IsMasterEnabled
        {
            get => Save.MasterVolume > -80;
            private set
            {
                ChangeMasterMixer(value ? 0 : -80);
                OnIsMasterEnabled?.Invoke(value);
            }
        }
        
        private AudioOptions Save => Saver.SaveData.Audio;
        
        private const string MasterMixerName = "MasterVolume",
            MusicMixerName = "MusicVolume",
            EffectMixerName = "EffectsVolume",
            UIMixerName = "InterfaceVolume";

        private void Awake()
        {
            GetComponent<ISingularObject>().Initialize();
        }
        
        private void Start()
        {
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
            audioMixer.audioMixer.SetFloat(MasterMixerName, volume);
            Save.MasterVolume = volume;
        }

        public void ChangeMusicsVolume(float volume)
        {
            ChangeVolume(MusicMixerName, volume);
            Save.MusicVolume = volume;
        }

        public void ChangeEffectsVolume(float volume)
        {
            ChangeVolume(EffectMixerName, volume);
            Save.EffectsVolume = volume;
        }

        public void ChangeInterfaceVolume(float volume)
        {
            ChangeVolume(UIMixerName, volume);
            Save.InterfaceVolume = volume;
        }

        private void ChangeVolume(string mixerName, float value)
        {
            audioMixer.audioMixer.SetFloat(mixerName, Mathf.Log10(value) * 20);
        }

        private void LoadSaveData()
        {
            IsMasterEnabled = Save.MasterVolume > -80;
            MusicValue = Save.MusicVolume;
            EffectsValue = Save.InterfaceVolume;
            InterfaceValue = Save.EffectsVolume;
        }

        private void OnEnable()
        {
            Saver.OnLoadData += LoadSaveData;
        }

        private void OnDisable()
        {
            Saver.OnLoadData -= LoadSaveData;
        }
    }
}