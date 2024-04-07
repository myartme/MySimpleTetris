using System;
using System.Collections.Generic;
using UnityEngine;

namespace Save.Data.Format
{
    [Serializable]
    public class OptionsSave
    {
        public List<Option> list = new ();

        public float GetValue(string name)
        {
            var index = list.FindIndex(opt => opt.name == name);

            if (index != -1) return list[index].value;
            
            throw new NullReferenceException();
        }

        public void SetValueByName(string name, float value, int decimals = 0)
        {
            var index = list.FindIndex(opt => opt.name == name);

            if (index != -1)
            {
                list[index] = CreateElement(name, value, decimals);
            }
        }
        
        public void AddToList(string name, float value, int decimals = 0)
        {
            var index = list.FindIndex(opt => opt.name == name);
            
            if (index == -1)
            {
                list.Add(CreateElement(name, value, decimals));
            }
        }

        public Option CreateElement(string name, float value, int decimals = 0)
        {
            return new Option { name = name, value = SetValue(value, decimals) }; 
        }

        private float SetValue(float value, int decimals = -1)
        {
            if (decimals > 0)
            {
                return Mathf.Round(value * 10 * decimals) / (10 * decimals);
            }

            return value;
        }
    }
}