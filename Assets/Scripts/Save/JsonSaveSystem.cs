using System;
using System.IO;
using Save.Data;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace Save
{
    public class JsonSaveSystem : IStorable
    {
        public bool IsExists => File.Exists(_filePath);
        
        private readonly string _filePath;
        
        public JsonSaveSystem()
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
            var json = JsonUtility.ToJson(data, true);
            using (var file = new StreamWriter(_filePath))
            {
                file.WriteLine(json);
            }
        }
        
        public SaveData Load()
        {
            if (!File.Exists(_filePath)) return default;

            var json = File.ReadAllText(_filePath);
            SaveData fileData = null;
            try
            {
                fileData = JsonUtility.FromJson<SaveData>(json);
            }
            catch (Exception e)
            {
                Debug.LogError("SaveSystem: Json Parse error " + e.Message);
            }

            return fileData;
        }

        private string GetFilePath(string name)
        {
            return Application.persistentDataPath + Path.DirectorySeparatorChar + name + ".json";
        }
    }
}