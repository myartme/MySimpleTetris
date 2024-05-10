using System;
using System.IO;
using Save.Data;
using UnityEngine;

namespace Save.SaveSystem
{
    public class JsonSaveSystem : AbstractSaveSystem, IStorable
    {
        public JsonSaveSystem() : base("json") { }

        public void Save(SaveFormatter data)
        {
            TryCreateFile();
            var json = JsonUtility.ToJson(data, true);
            using var file = new StreamWriter(filePath);
            file.WriteLine(json);
        }
        
        public SaveFormatter Load(SaveFormatter defaultData)
        {
            var fileData = defaultData;
            try
            {
                var json = File.ReadAllText(filePath);
                fileData = JsonUtility.FromJson<SaveFormatter>(json);
            }
            catch (Exception e)
            {
                Debug.LogError("SaveSystem: Json Parse error " + e.Message);
            }

            return fileData;
        }
    }
}