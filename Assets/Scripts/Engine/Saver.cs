using System;
using Save.Data;
using Save.SaveSystem;
using Service.Singleton;

namespace Engine
{
    public class Saver : AbstractSingleton<Saver>
    {
        public static SaveData SaveData;
        public static Action OnSaveData;
        public static Action OnLoadData;

        private static IStorable _store;
        private static SaveFormatter _defaultData;
        private static SaveFormatter _lastSavedData;
        
        private void Awake()
        {
            GetComponent<ISingularObject>().Initialize();
            _store ??= new JsonSaveSystem(); //new BinarySaveSystem();
            SaveData ??= new SaveData();
            _defaultData ??= SaveData.GetFormattedData();
            Load();
            _lastSavedData ??= SaveData.GetFormattedData();
        }

        public void Save()
        {
            _lastSavedData = SaveData.GetFormattedData();
            _store.Save(_lastSavedData);
            OnSaveData?.Invoke();
        }
        
        public void Reset()
        {
            SaveData.SetFormattedData(_lastSavedData);
            OnLoadData?.Invoke();
        }
        
        private void Load()
        {
            SaveData.SetFormattedData(_store.Load(_defaultData));
            OnLoadData?.Invoke();
        }
    }
}