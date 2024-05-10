using Save.Data.Format;

namespace Save.Data.SaveDataElements
{
    public class ControlOptions
    {
        private const string HorizontalMoveSpeedName ="HorizontalMoveSpeed";
        private const string VerticalMoveSpeedName = "VerticalMoveSpeed";
        private OptionsSave _saveData;
            
        public float HorizontalMoveSpeed
        {
            get => _saveData.GetValue(HorizontalMoveSpeedName);
            set => _saveData.SetValueByName(HorizontalMoveSpeedName, value);
        }

        public float VerticalMoveSpeed
        {
            get => _saveData.GetValue(VerticalMoveSpeedName);
            set => _saveData.SetValueByName(VerticalMoveSpeedName, value);
        }
        
        public ControlOptions(OptionsSave saveData)
        {
            _saveData = saveData;
            InitializeOptions();
        }

        private void InitializeOptions()
        {
            _saveData.AddToList(HorizontalMoveSpeedName, 0.5f);
            _saveData.AddToList(VerticalMoveSpeedName, 0.8f);
        }
    }
}