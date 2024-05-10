namespace Save.Data
{
    public interface IDataSavable
    {
        public SaveFormatter GetFormattedData();
        public void SetFormattedData(SaveFormatter data);
    }
}