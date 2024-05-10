using Save.Data.Format;

namespace Save.Data.SaveDataElements
{
    public class LanguageOptions
    {
        private const string LanguageName ="Language";
        private OptionsSave _saveData;

        public float Language
        {
            get => _saveData.GetValue(LanguageName);
            set => _saveData.SetValueByName(LanguageName, value);
        }
        
        public LanguageOptions(OptionsSave saveData)
        {
            _saveData = saveData;
            InitializeOptions();
        }

        private void InitializeOptions()
        {
            _saveData.AddToList(LanguageName, 0f);
        }
    }
}