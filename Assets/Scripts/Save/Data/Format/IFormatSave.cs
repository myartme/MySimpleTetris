using System.Collections.Generic;

namespace Save.Data.Format
{
    public interface IFormatSave<TS>
    {
        public List<TS> GetData();
    }
}