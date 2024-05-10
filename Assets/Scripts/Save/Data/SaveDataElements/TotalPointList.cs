using System.Collections.Generic;
using Save.Data.Format;

namespace Save.Data.SaveDataElements
{
    public class TotalPointList
    {
        public List<TotalPointPosition> List => _saveData.GetList();
        private const int required_positions_count = 10;
        private TotalPointsSave _saveData;
            
        public int GetValue(int position)
        {
            return _saveData.GetValue(position);
        }
        
        public void SetValueByPosition(int position, int value)
        {
            _saveData.SetValueByPosition(position, value);
        }
        
        public int GetSupposedPositionByValue(int value)
        {
            return _saveData.GetSupposedPositionByValue(value);
        }
        
        public void SetValueWithShift(int value)
        {
            _saveData.SetValueWithShift(value);
        }
            
        public TotalPointList(TotalPointsSave saveData)
        {
            _saveData = saveData;
            InitializeOptions();
        }

        private void InitializeOptions()
        {
            for (var i = 1; i <= required_positions_count; i++)
            {
                _saveData.AddToList(i, 0);
            }
        }
    }
}