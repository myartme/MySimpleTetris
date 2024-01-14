using System.IO;
using Save.Data;
using Service;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace Save
{
    public class JsonSaveSystem<T> : IStorable<T> where T : AbstractSaveData
    {
        public bool IsExists => File.Exists(_filePath);
        
        private readonly string _filePath;
        
        public JsonSaveSystem(SaveNames fileSaveName)
        {
            _filePath = GetFilePath(fileSaveName.GetDescription());
        }
        
        public void Create()
        {
            if(IsExists) return;

            using (File.Create(_filePath)) { }
        }

        public void Save(T data)
        {
            var json = JsonUtility.ToJson(data);
            using (var file = new StreamWriter(_filePath))
            {
                file.WriteLine(json);
            }
        }
        
        public T Load()
        {
            if (!File.Exists(_filePath)) return default;

            var json = File.ReadAllText(_filePath);
            return JsonUtility.FromJson<T>(json);
        }

        private string GetFilePath(string name)
        {
            return Application.persistentDataPath + Path.DirectorySeparatorChar + name + ".json";
        }
    }
}