using System;
using Save.Data.Format;

namespace Save.Data
{
    [Serializable]
    public class SaveData
    {
        public OptionsSave options = new();
        public TotalPointsSave totalPoints = new();
    }
    
    [Serializable]
    public struct Option
    {
        public string name;
        public float value;
    }
    
    [Serializable]
    public struct TotalPoints
    {
        public int position;
        public int value;
    }
}