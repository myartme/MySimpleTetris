using Save;
using Save.Data;
using UnityEngine;

namespace Engine
{
    public class Saver : MonoBehaviour
    {
        [SerializeField] private ColorTheme _colorTheme;
        
        public static IStorable Store;
        public static SaveData SaveData;
        private void Awake()
        {
            Store = new JsonSaveSystem();
            SaveData = new SaveData();
            if (!Store.IsExists)
            {
                Store.Create();
            }
            else
            {
                SaveData = Store.Load();
            }

            InitSaves();
        }
        
        private void InitSaves()
        {
            _colorTheme.InitializeSaveData();
            _colorTheme.LoadSaveData();
        }
    }
}