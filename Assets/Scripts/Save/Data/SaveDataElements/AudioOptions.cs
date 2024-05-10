using Save.Data.Format;

namespace Save.Data.SaveDataElements
{
    public class AudioOptions
    {
        private const string MasterVolumeName ="MasterVolume";
        private const string MusicVolumeName = "MusicVolume";
        private const string EffectsVolumeName = "EffectsVolume";
        private const string InterfaceVolumeName = "InterfaceVolume";
        private OptionsSave _saveData;
            
        public float MasterVolume
        {
            get => _saveData.GetValue(MasterVolumeName);
            set => _saveData.SetValueByName(MasterVolumeName, value);
        }

        public float MusicVolume
        {
            get => _saveData.GetValue(MusicVolumeName);
            set => _saveData.SetValueByName(MusicVolumeName, value);
        }

        public float EffectsVolume
        {
            get => _saveData.GetValue(EffectsVolumeName);
            set => _saveData.SetValueByName(EffectsVolumeName, value);
        }

        public float InterfaceVolume
        {
            get => _saveData.GetValue(InterfaceVolumeName);
            set => _saveData.SetValueByName(InterfaceVolumeName, value);
        }
        
        public AudioOptions(OptionsSave saveData)
        {
            _saveData = saveData;
            InitializeOptions();
        }

        private void InitializeOptions()
        {
            _saveData.AddToList(MasterVolumeName, 0f);
            _saveData.AddToList(MusicVolumeName, 0.5f);
            _saveData.AddToList(EffectsVolumeName, 0.5f);
            _saveData.AddToList(InterfaceVolumeName, 0.5f);
        }
    }
}