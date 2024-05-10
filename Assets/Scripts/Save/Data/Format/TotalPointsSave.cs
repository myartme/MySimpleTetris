using System;
using System.Collections.Generic;
using System.Linq;

namespace Save.Data.Format
{
    public class TotalPointsSave
    {
        private List<TotalPointPosition> _list;
        
        public TotalPointsSave(List<TotalPointPosition> defaultList = null)
        {
            _list = defaultList ?? new List<TotalPointPosition>();
        }
        
        public int GetValue(int position)
        {
            var index = _list.FindIndex(opt => opt.Position == position);

            if (index != -1) return _list[index].Value;
            
            throw new NullReferenceException();
        }
        
        public void SetValueByPosition(int position, int value)
        {
            var index = _list.FindIndex(opt => opt.Position == position);

            if (index != -1)
            {
                _list[index] = CreateElement(position, value);
            }
        }
        
        public int GetSupposedPositionByValue(int value)
        {
            return _list.FindIndex(opt => opt.Value < value) + 1;
        }
        
        public void SetValueWithShift(int value)
        {
            var position = GetSupposedPositionByValue(value);
            if(position == 0) return;
            
            for (var i = _list.Count - 1; i >= position; i--)
            {
                _list[i] = CreateElement(i+1, _list[i-1].Value);
            }

            SetValueByPosition(position, value);
        }

        public void AddToList(int position, int value)
        {
            var index = _list.FindIndex(opt => opt.Position == position);
            
            if (index == -1)
            {
                _list.Add(CreateElement(position, value));
            }
        }

        private TotalPointPosition CreateElement(int position, int value)
        {
            return new TotalPointPosition { Position = position, Value = value }; 
        }
        
        public List<TotalPointPosition> GetList()
        {
            return _list.Select(el 
                => new TotalPointPosition
                {
                    Position = el.Position, 
                    Value = el.Value
                }).ToList();
        }
    }
}