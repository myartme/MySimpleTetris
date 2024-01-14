using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Save.Data;
using Service;
using Application = UnityEngine.Device.Application;

namespace Save
{
    public class BinarySaveSystem<T> : IStorable<T> where T : AbstractSaveData
    {
        public bool IsExists => File.Exists(_filePath);
        
        private BinaryFormatter _binaryFormatter = new ();
        private readonly string _filePath;
        
        public BinarySaveSystem(SaveNames fileSaveName)
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
            using (var file = File.Create(_filePath))
            {
                _binaryFormatter.Serialize(file, data);
            }
        }
        
        public T Load()
        {
            if (!File.Exists(_filePath)) return default;
            
            using var file = File.Open(_filePath, FileMode.Open);
            return (T)_binaryFormatter.Deserialize(file);
        }

        private string GetFilePath(string name)
        {
            return Application.persistentDataPath + Path.DirectorySeparatorChar + name + ".dat";
        }
    }
}