using System;
using System.Collections.Generic;
using System.Linq;

namespace Save.Data.Format
{
    public class OptionsSave
    {
        private List<Option> _list;

        public OptionsSave(List<Option> defaultList = null)
        {
            _list = new List<Option>(defaultList ?? new List<Option>());
        }

        public float GetValue(string name)
        {
            var index = _list.FindIndex(opt => opt.Name == name);

            if (index != -1) return _list[index].Value;
            
            throw new NullReferenceException();
        }

        public void SetValueByName(string name, float value)
        {
            var index = _list.FindIndex(opt => opt.Name == name);

            if (index != -1)
            {
                _list[index] = CreateElement(name, value);
            }
        }
        
        public void AddToList(string name, float value)
        {
            var index = _list.FindIndex(opt => opt.Name == name);
            
            if (index == -1)
            {
                _list.Add(CreateElement(name, value));
            }
        }

        private Option CreateElement(string name, float value)
        {
            return new Option { Name = name, Value = value }; 
        }
        
        public List<Option> GetList()
        {
            return _list.Select(el 
                => new Option
                {
                    Name = el.Name, 
                    Value = el.Value
                }).ToList();
        }
    }
}