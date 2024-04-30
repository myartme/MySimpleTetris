using Save;
using Save.Data;
using Service.Singleton;

namespace Engine
{
    public class Saver : AbstractSingleton<Saver>
    {
        public static IStorable Store;
        public static SaveData SaveData;
        
        private void Awake()
        {
            Store = new JsonSaveSystem(); //new BinarySaveSystem();
            SaveData = new SaveData();
            if (!Store.IsExists)
            {
                Store.Create();
                return;
            }

            var loadData = Store.Load();
            if (loadData is not null)
            {
                SaveData = loadData;
            }
            
            GetComponent<ISingularObject>().Initialize();
        }
    }
}