using Save.Data;

namespace Save.SaveSystem
{
    public interface IStorable
    {
        public void Save(SaveFormatter data);
        public SaveFormatter Load(SaveFormatter defaultData);
    }
}