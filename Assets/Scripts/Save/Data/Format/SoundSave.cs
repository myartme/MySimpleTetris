using System;
using System.Collections.Generic;
using Engine;

namespace Save.Data.Format
{
    public class SoundSave : IFormatSave<Option>
    {
        private List<Option> _list = new ();
        public SoundSave()
        {
            _list.Add(new Option { name = MixerController.master, value = 0f });
            _list.Add(new Option { name = MixerController.music, value = 0f });
            _list.Add(new Option { name = MixerController.effect, value = 0f });
            _list.Add(new Option { name = MixerController.ui, value = 0f });
        }
        
        public float GetValue(string name)
        {
            var index = _list.FindIndex(opt => opt.name == name);

            if (index != -1) return _list[index].value;
            
            throw new NullReferenceException();
        }
        
        public void SetValue(string name, float value)
        {
            var index = _list.FindIndex(opt => opt.name == name);

            if (index != -1)
            {
                _list[index] = new Option { name = name, value = value };
            }
        }

        public List<Option> GetData()
        {
            return _list;
        }
    }
}