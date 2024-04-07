using System;
using System.Collections.Generic;

namespace Save.Data.Format
{
    [Serializable]
    public class TotalPointsSave
    {
        public List<TotalPoints> list = new ();
        
        public int GetValue(int position)
        {
            var index = list.FindIndex(opt => opt.position == position);

            if (index != -1) return list[index].value;
            
            throw new NullReferenceException();
        }
        
        public void SetValueByPosition(int position, int value)
        {
            var index = list.FindIndex(opt => opt.position == position);

            if (index != -1)
            {
                list[index] = CreateElement(position, value);
            }
        }
        
        public int GetSupposedPositionByValue(int value)
        {
            return list.FindIndex(opt => opt.value < value) + 1;
        }
        
        public void SetValueWithShift(int value)
        {
            var position = GetSupposedPositionByValue(value);
            if(position == 0) return;
            
            for (var i = list.Count - 1; i >= position; i--)
            {
                list[i] = CreateElement(i+1, list[i-1].value);
            }

            SetValueByPosition(position, value);
        }

        public void AddToList(int position, int value)
        {
            var index = list.FindIndex(opt => opt.position == position);
            
            if (index == -1)
            {
                list.Add(CreateElement(position, value));
            }
        }

        public TotalPoints CreateElement(int position, int value)
        {
            return new TotalPoints { position = position, value = value }; 
        }
    }
}