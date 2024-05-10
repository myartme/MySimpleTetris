using System;
using System.Collections.Generic;
using Save.Data.Format;
using Save.Data.SaveDataElements;

namespace Save.Data
{
    public class SaveData : IDataSavable
    {
        public AudioOptions Audio { get; private set; }
        public ControlOptions Control { get; private set; }
        public VisualOptions Visual { get; private set; }
        public LanguageOptions Language { get; private set; }
        public TotalPointList TotalPoints { get; private set; }
        
        private OptionsSave _optionsSave;
        private TotalPointsSave _totalPointsSave;
        
        public SaveData()
        {
            _optionsSave = new OptionsSave();
            _totalPointsSave = new TotalPointsSave();
            Init();
        }
        
        public SaveFormatter GetFormattedData()
        {
            return new SaveFormatter
            {
                Options = _optionsSave.GetList(),
                TotalPointsList = _totalPointsSave.GetList()
            };
        }

        public void SetFormattedData(SaveFormatter data)
        {
            _optionsSave = new OptionsSave(data.Options);
            _totalPointsSave = new TotalPointsSave(data.TotalPointsList);
            Init();
        }
        
        private void Init()
        {
            Audio = new AudioOptions(_optionsSave);
            Control = new ControlOptions(_optionsSave);
            Visual = new VisualOptions(_optionsSave);
            Language = new LanguageOptions(_optionsSave);
            TotalPoints = new TotalPointList(_totalPointsSave);
        }
    }

    [Serializable]
    public class Option
    {
        public string Name;
        public float Value;
    }
    
    [Serializable]
    public struct TotalPointPosition
    {
        public int Position;
        public int Value;
    }
    
    [Serializable]
    public class SaveFormatter
    {
        public List<Option> Options;
        public List<TotalPointPosition> TotalPointsList;
    }
}