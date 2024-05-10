using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Save.Data;
using UnityEngine;

namespace Save.SaveSystem
{
    public class BinarySaveSystem : AbstractSaveSystem, IStorable
    {
        private BinaryFormatter _binaryFormatter;
        
        public BinarySaveSystem() : base("dat")
        {
            _binaryFormatter = new BinaryFormatter();
        }

        public void Save(SaveFormatter data)
        {
            TryCreateFile();
            using var file = File.Create(filePath);
            _binaryFormatter.Serialize(file, data);
        }
        
        public SaveFormatter Load(SaveFormatter defaultData)
        {
            var fileData = defaultData;
            try
            {
                using var file = File.Open(filePath, FileMode.Open);
                fileData = (SaveFormatter)_binaryFormatter.Deserialize(file);
            }
            catch (Exception e)
            {
                Debug.LogError("SaveSystem: Binary Parse error " + e.Message);
            }

            return fileData;
        }
    }
}