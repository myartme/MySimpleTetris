using System;
using System.Collections.Generic;

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

        public void SetValueByName(string name, float value)
        {
            var index = list.FindIndex(opt => opt.name == name);

            if (index != -1)
            {
                list[index] = CreateElement(name, value);
            }
        }
        
        public void AddToList(string name, float value)
        {
            var index = list.FindIndex(opt => opt.name == name);
            
            if (index == -1)
            {
                list.Add(CreateElement(name, value));
            }
        }

        public Option CreateElement(string name, float value)
        {
            return new Option { name = name, value = value }; 
        }
    }
}