using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Save.Data;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace Save
{
    public class BinarySaveSystem : IStorable
    {
        public bool IsExists => File.Exists(_filePath);
        
        private BinaryFormatter _binaryFormatter = new ();
        private readonly string _filePath;
        
        public BinarySaveSystem()
        {
            _filePath = GetFilePath("save");
        }
        
        public void Create()
        {
            if(IsExists) return;

            using (File.Create(_filePath)) { }
        }

        public void Save(SaveData data)
        {
            using (var file = File.Create(_filePath))
            {
                _binaryFormatter.Serialize(file, data);
            }
        }
        
        public SaveData Load()
        {
            if (!File.Exists(_filePath)) return default;
            
            SaveData fileData = null;
            
            try
            {
                using var file = File.Open(_filePath, FileMode.Open);
                fileData = (SaveData)_binaryFormatter.Deserialize(file);
            }
            catch (Exception e)
            {
                Debug.LogError("SaveSystem: Binary Parse error " + e.Message);
            }

            return fileData;
        }

        private string GetFilePath(string name)
        {
            return Application.persistentDataPath + Path.DirectorySeparatorChar + name + ".dat";
        }
    }
}