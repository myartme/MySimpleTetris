using Save;
using Save.Data;
using UnityEngine;

namespace Engine
{
    public class Saver : MonoBehaviour
    {
        public static IStorable Store;
        public static SaveData SaveData;
        private void Awake()
        {
            Store = new JsonSaveSystem();
            SaveData = new SaveData();
            SaveData = !Store.IsExists ? Store.Create() : Store.Load();
        }
    }
}