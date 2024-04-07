using Save.Data.Format;

namespace Engine
{
    public class TotalPointsTable
    {
        public TotalPointsSave Save
        {
            get => Saver.SaveData.totalPoints;
            private set => Saver.SaveData.totalPoints = value;
        }
        
        private const int REQUIRED_NUMBER_OF_PARAMS = 10;
        
        public void StoreSaveData()
        {
            Saver.Store.Save(Saver.SaveData);
        }
        
        public void InitializeSaveData()
        {
            if (Save.list.Count >= REQUIRED_NUMBER_OF_PARAMS) return;
            
            for (var i = 1; i <= REQUIRED_NUMBER_OF_PARAMS; i++)
            {
                Save.AddToList(i, 0);
            }

            StoreSaveData();
        }
    }
}