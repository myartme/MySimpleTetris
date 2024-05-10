using Save.Data.Format;

namespace Save.Data.SaveDataElements
{
    public class VisualOptions
    {
        private const string ThemeIdName ="ThemeId";
        private const string BlockIdName = "BlockId";
        private OptionsSave _saveData;
            
        public float ThemeId
        {
            get => _saveData.GetValue(ThemeIdName);
            set => _saveData.SetValueByName(ThemeIdName, value);
        }

        public float BlockId
        {
            get => _saveData.GetValue(BlockIdName);
            set => _saveData.SetValueByName(BlockIdName, value);
        }
        
        public VisualOptions(OptionsSave saveData)
        {
            _saveData = saveData;
            InitializeOptions();
        }

        private void InitializeOptions()
        {
            _saveData.AddToList(ThemeIdName, 0f);
            _saveData.AddToList(BlockIdName, 0f);
        }
    }
}